using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		private Database _db;
		public Database db {
			get {
				if(_db == null)
					_db = Sitecore.Configuration.Factory.GetDatabase("master");
				return _db;
			}
		}

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

		public override IEnumerable<string> DataSummary {
			get {
				yield return SummaryStr(Constants.Keys.Page, db.GetItem(PageTree.Value).Paths.ContentPath);
			}
		}

		protected string SummaryStr(string name, string value) {
			return string.Format("{0}: <span class='value'>{1}</span>", name, value);
		}

		public override IEnumerable<KeyValuePair<string, object>> DataDictionary {
			get {
				yield return new KeyValuePair<string, object>(Constants.Keys.Page, PageTree.Value);
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

		#endregion Page Load
		
		private bool ValidatePage() {

			bool valid = true;
			StringBuilder sb = new StringBuilder();

			string cID = PageTree.Value;
			if (!string.IsNullOrEmpty(cID) && Sitecore.Data.ID.IsID(cID)) { // Content Branch
				Item ci = db.GetItem(cID);
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
	}
}
