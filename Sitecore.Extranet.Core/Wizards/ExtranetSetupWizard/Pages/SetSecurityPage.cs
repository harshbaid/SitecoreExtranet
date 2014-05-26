using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Sites;
using Sitecore.Web.UI.HtmlControls;

namespace Sitecore.Extranet.Core.Wizards.ExtranetSetupWizard.Pages {
	public class SetSecurityPage : BasePage {

		#region Page
		protected Literal PageErrorMessage;
		protected DataContext PageDC;
		protected TreePicker PageTree;
		#endregion Page;
			
		#region Properties

		public BasePage PreviousPage { get; set; }

		public override bool IsValid {
			get {
				bool valid = ValidatePage();
				
				PageErrorMessage.Visible = false;

				if (!valid) {
					PageErrorMessage.Visible = true;
					Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml(PageErrorMessage.ID, PageErrorMessage);
				}
				return valid;
			}
		}

		private bool ValidatePage() {

			bool valid = true;
			StringBuilder sb = new StringBuilder();

			string cID = PageTree.Value;
			if (!string.IsNullOrEmpty(cID) && Sitecore.Data.ID.IsID(cID)) { // Content Branch
				Item ci = MasterDB.GetItem(cID);
				if (ci == null) {
					valid = false;
					sb.Append("The item you selected is null.").Append("<br/>");
				}
			} else {
				valid = false;
				sb.Append("The item selected is producing an empty string or bad ID.").Append("<br/>");
			}

			if (!valid)
				PageErrorMessage.Text = sb.ToString();

			return valid;
		}

		public override IEnumerable<string> DataSummary {
			get {
				yield return SummaryStr(Constants.Keys.Page, MasterDB.GetItem(PageTree.Value).Paths.ContentPath);
			}
		}

		public override IEnumerable<KeyValuePair<string, object>> DataDictionary {
			get {
				yield return new KeyValuePair<string, object>(Constants.Keys.Page, PageTree.Value);
			}
		}

		#endregion Properties

		#region Initialize

		protected override void InitializeControl() {

			SetDataContext();
		}

		public void SetDataContext() {
			
			PageDC.GetFromQueryString();
			
			if (PreviousPage == null) 
				return;

			string siteName = (string)PreviousPage.DataDictionary.Where(a => a.Key.Equals(Constants.Keys.Site)).First().Value;
			SiteContext sc = Configuration.Factory.GetSite(siteName);
			if(sc == null)
				return;

			PageDC.Root = sc.StartPath;
			PageDC.Folder = sc.StartPath;
		}

		#endregion Initialize
	}
}
