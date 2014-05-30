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
using Sitecore.Web;
using Sitecore.Configuration;

namespace Sitecore.Extranet.Core.Wizards.ExtranetRemoverWizard {
	public class ExtranetRemover : BaseLongRunningJob {

		public ExtranetRemover(Job job) : base(job) { }

		#region Start Build

		public override void CoreExecute() {

			string siteName = InputData.Get<string>(Constants.Keys.Site);
			SiteInfo SiteInfo = Factory.GetSiteInfo(siteName);
			
			//status
			SetStatus(1, "Removing extranet pages.");

			//remove extranet pages from branch
			RemoveExtranetPages(SiteInfo);

			//status
			SetStatus(2, "Removing Security.");

			//remove role and users
			RemoveRoleAndUsers(siteName);

			//remove site login settings and attributes
			RemoveSiteAttributes(SiteInfo);
		}

		#endregion Start Build

		#region Action Chunks

		protected void RemoveExtranetPages(SiteInfo siteItem){
			Item HomeItem = MasterDB.GetItem(string.Format("{0}{1}", siteItem.RootPath, siteItem.StartItem));
			IEnumerable<Item> pages = HomeItem.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.ExtranetFolder) || a.Template.Is(Constants.TempateName.ExtranetLogin));
			if (pages == null || !pages.Any())
				return;
			foreach (Item p in pages) {
				Item par = p.Parent;
				p.Recycle();

				SitecoreUtility.PublishContent(par, true);
			}
		}

		protected void RemoveRoleAndUsers(string siteName) {

			//remove roles
			string roleName = string.Format("extranet\\{0} Extranet", siteName);
			if (Role.Exists(roleName)) {
				Role r = Role.FromName(roleName);

				bool removeRole = InputData.Get<bool>(Constants.Keys.RemoveRole);
				bool removeUsers = InputData.Get<bool>(Constants.Keys.RemoveUsers);

				if (removeUsers) {
					IEnumerable<User> users = UserManager.GetUsers().Where(a => a.IsInRole(r));
					if (users != null && users.Any()) {
						foreach (User u in users)
							u.Delete();
					}
				}

				if(removeUsers)
					Roles.DeleteRole(r.Name);
			}
		}

		protected void RemoveSiteAttributes(SiteInfo siteInfo) {

			
			Item sFolder = MasterDB.GetItem(Constants.Paths.Sites);
			if (sFolder != null) {
				Item siteItem = sFolder.Axes.GetChild(siteInfo.Name);
				//set login url on the site node
				using (new EditContext(siteItem)) {
					siteItem[Sitecore.Extranet.Core.Constants.ExtranetAttributes.LoginPage] = string.Empty;
				}

				IEnumerable<Item> atts = siteItem.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.SiteAttribute) && Sitecore.Extranet.Core.Constants.ExtranetAttributes.Keys.Contains(a.Key));
				if (atts != null && atts.Any()) {
					foreach (Item a in atts)
						a.Recycle();
				}

				SitecoreUtility.PublishContent(siteItem, true);
			} else {
				FileUtility.RemoveFile(string.Format(Sitecore.Extranet.Core.Wizards.ExtranetSetupWizard.Constants.FilePatterns.ExtranetSiteConfig, siteInfo.Name));
			}
		}

		#endregion Action Chunks
	}
}
