using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Jobs;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Security.Accounts;
using System.Web.Security;
using Sitecore.Extranet.Core.Utility;

namespace Sitecore.Extranet.Core.Wizards.ExtranetRemoverWizard {
	public class ExtranetRemover : BaseLongRunningJob {

		public ExtranetRemover(Job job) : base(job) { }

		#region Start Build

		public override void CoreExecute(Dictionary<string, object> data) {
			
			string siteID = InputData.Get<string>(Constants.Keys.Site);
			Item SiteItem = MasterDB.GetItem(siteID);
			string siteName = SiteItem["name"];

			//status
			SetStatus(1, "Removing extranet pages.");

			//remove extranet pages from branch
			RemoveExtranetPages(SiteItem);

			//status
			SetStatus(2, "Removing Security.");

			//remove role and users
			RemoveRoleAndUsers(SiteItem);

			//remove site login settings and attributes
			UpdateSite(SiteItem);
		}

		#endregion Start Build

		#region Action Chunks

		protected void RemoveExtranetPages(Item siteItem){
			Item HomeItem = MasterDB.GetItem(siteItem["startItem"]);
			IEnumerable<Item> pages = HomeItem.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.ExtranetFolder) || a.Template.Is(Constants.TempateName.ExtranetLogin));
			if (pages == null || !pages.Any())
				return;
			foreach (Item p in pages) {
				Item par = p.Parent;
				p.Recycle();

				SitecoreUtility.PublishContent(par, true);
			}
		}

		protected void RemoveRoleAndUsers(Item siteItem){

			//remove roles
			string roleName = string.Format("extranet\\{0} Extranet", siteItem["name"]);
			if (Role.Exists(roleName)) {
				Role r = Role.FromName(roleName);
				
				IEnumerable<User> users = UserManager.GetUsers().Where(a => a.IsInRole(r));
				if (users != null && users.Any()) {
					foreach (User u in users)
						u.Delete();
				}
				
				Roles.DeleteRole(r.Name);
			}
		}

		protected void UpdateSite(Item siteItem) {

			//set login url on the site node
			using (new EditContext(siteItem)) {
				siteItem["loginPage"] = string.Empty;
			}

			IEnumerable<Item> atts = siteItem.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.SiteAttribute));
			if (atts != null && atts.Any()) {
				foreach (Item a in atts)
					a.Recycle();
			}

			SitecoreUtility.PublishContent(siteItem, true);
		}

		#endregion Action Chunks
	}
}
