using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;
using SCExtranet.Core.Extensions;
using Sitecore.Security.Accounts;

namespace SCExtranet.Core.Wizards.ExtranetRemoverWizard.Pages {
	public class SelectSiteRemovePage : BasePage {

		#region Controls
		protected Combobox SiteItem;
		#endregion Controls

		#region Properties

		private Database _db;
		public Database db {
			get {
				if (_db == null)
					_db = Sitecore.Configuration.Factory.GetDatabase("master");
				return _db;
			}
		}

		public override IEnumerable<string> DataSummary {
			get {
				Item si = db.GetItem(SiteItem.Value);
				string siteName = si["name"];
				
				yield return SummaryStr(Constants.Keys.Site, siteName);

				//find the extranet roles
				string roleName = string.Format("extranet\\{0} Extranet", siteName);
				if (Role.Exists(roleName)) {
					yield return SummaryStr("Role", roleName);
				}

				//find the extranet pages 
				Item HomeItem = db.GetItem(si["startItem"]);
				if (HomeItem != null) {
					IEnumerable<Item> pages = HomeItem.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.ExtranetFolder) || a.Template.Is(Constants.TempateName.ExtranetLogin));
					if (pages != null && pages.Any()) {
						foreach (Item p in pages)
							yield return SummaryStr("Content", p.Paths.ContentPath);
					}
				}

				//find the attributes
				IEnumerable<Item> atts = si.Axes.GetDescendants().Where(a => a.Template.IsID(Constants.TemplateIDs.SiteAttribute));
				if (atts != null && atts.Any()) {
					foreach (Item a in atts)
						yield return SummaryStr("Attribute", a.Paths.Path);
				}
			}
		}

		protected string SummaryStr(string name, string value) {
			return string.Format("{0}: <span class='value'>{1}</span>", name, value);
		}

		public override IEnumerable<KeyValuePair<string, object>> DataDictionary {
			get {
				yield return new KeyValuePair<string, object>(Constants.Keys.Site, SiteItem.Value);
			}
		}

		#endregion Properties

		#region Page Load

		protected override void OnLoad(EventArgs e) {

			// similar to is not PostBack.
			if (!Sitecore.Context.ClientPage.IsEvent) {
				InitializeControl();
			}
			base.OnLoad(e);
		}

		private void InitializeControl() {

			//setup site drop downs
			Item sFolder = this.db.GetItem(Constants.Paths.Sites);
			if (sFolder == null)
				return;
			IEnumerable<ListItem> sites =
				from val in sFolder.Axes.GetDescendants().Where(el => el.Template.IsID(Constants.TemplateIDs.Site))
				orderby val.Name
				select new ListItem() { ID = Control.GetUniqueID("I"), Header = val.DisplayName, Value = val.ID.ToString(), Selected = false };

			foreach (ListItem s in sites) {
				Sitecore.Context.ClientPage.AddControl(SiteItem, s);
			}
		}

		#endregion Page Load
	}
}
