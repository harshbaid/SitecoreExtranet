using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Security.Accounts;
using Sitecore.Extranet.Core.Utility;
using Sitecore.Web;
using Sitecore.Configuration;

namespace Sitecore.Extranet.Core.Wizards.ExtranetRemoverWizard.Pages {
	public class SelectSiteRemovePage : BasePage {

		#region Controls
		protected Combobox SiteItem;
		protected Checkbox RemoveRole;
		protected Checkbox RemoveUsers;
		#endregion Controls

		#region Properties

		protected List<string> Attributes {
			get {
				return new List<string>() { "ExtranetUserPrefix", "ExtranetRole", "ExtranetProvider" };
			}
		}

		public override IEnumerable<string> DataSummary {
			get {
				SiteInfo si= Factory.GetSiteInfo(SiteItem.Value);
				string siteName = SiteItem.Value;
				
				yield return SummaryStr(Constants.Keys.Site, siteName);

				//find the extranet roles
				string roleName = string.Format("extranet\\{0} Extranet", siteName);
				if (Role.Exists(roleName)) {
					yield return SummaryStr("Role", roleName);
				}

				//find the extranet pages 
				Item HomeItem = MasterDB.GetItem(string.Format("{0}{1}", si.RootPath, si.StartItem));
				if (HomeItem != null) {
					IEnumerable<Item> pages = HomeItem.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.ExtranetFolder) || a.Template.Is(Constants.TempateName.ExtranetLogin));
					if (pages != null && pages.Any()) {
						foreach (Item p in pages)
							yield return SummaryStr("Content", p.Paths.ContentPath);
					}
				}

				//find the attributes
				IEnumerable<KeyValuePair<string,string>> atts = si.Properties.ToDictionary().Where(a => Attributes.Contains(a.Key));
				if (atts != null && atts.Any()) {
					foreach (KeyValuePair<string, string> a in atts)
						yield return SummaryStr("Attribute", string.Format("{0} - {1}", a.Key, a.Value));
				}

				yield return SummaryStr(Constants.Keys.RemoveRole, RemoveRole.Checked.ToString());
				yield return SummaryStr(Constants.Keys.RemoveUsers, RemoveUsers.Checked.ToString());
			}
		}

		public override IEnumerable<KeyValuePair<string, object>> DataDictionary {
			get {
				yield return new KeyValuePair<string, object>(Constants.Keys.Site, SiteItem.Value);
				yield return new KeyValuePair<string, object>(Constants.Keys.RemoveRole, RemoveRole.Checked);
				yield return new KeyValuePair<string, object>(Constants.Keys.RemoveUsers, RemoveUsers.Checked);
			}
		}

		#endregion Properties

		#region Initialize

		protected override void InitializeControl() {

			//setup site drop downs
			IEnumerable<ListItem> sites =
				from val in Sitecore.Configuration.Factory.GetSiteInfoList()
				where !SitecoreUtility.SystemSites.Contains(val.Name)
				orderby val.Name
				select new ListItem() { ID = Control.GetUniqueID("I"), Header = val.Name, Value = val.Name, Selected = false };

			foreach (ListItem s in sites) 
				Sitecore.Context.ClientPage.AddControl(SiteItem, s);
		}

		#endregion Initialize
	}
}
