﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Items;
using SCExtranet.Core.Extensions;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Globalization;
using Sitecore.Web;

namespace SCExtranet.Core.Wizards.ExtranetSetupWizard.Pages {
	public class SelectSitePage : BasePage {

		#region Controls
		protected Combobox SiteItem;
		protected Literal LangErrorMessage;
		protected DataContext ExtranetBranchDC;
		protected Literal BranchErrorMessage;
		protected TreePicker ExtranetBranch;
		protected Listbox LanguageList; 
		protected Checkbox PublishContent;
		#endregion Controls

		#region Properties

		private Database _db;
		public Database db {
			get {
				if(_db == null)
					_db = Sitecore.Configuration.Factory.GetDatabase("master");
				return _db;
			}
		}

		public override bool IsValid {
			get {
				bool valid = true;

				BranchErrorMessage.Visible = false;
				LangErrorMessage.Visible = false;
				
				if (!ValidateBranch()) {
					valid = false;
					BranchErrorMessage.Visible = true;
				} 
				
				if (!LanguageList.Selected.Where(a => a.Selected).Any()) { // languages
					valid = false;
					LangErrorMessage.Text = "Please select at least one language.";
					LangErrorMessage.Visible = true;
				}

				if (!valid)
					Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml(LangErrorMessage.ID, LangErrorMessage);

				return valid;
			}
		}

		private bool ValidateBranch() {

			bool valid = true;
			StringBuilder sb = new StringBuilder();

			string eID = ExtranetBranch.Value;
			if (!string.IsNullOrEmpty(eID) && Sitecore.Data.ID.IsID(eID)) { // Content Branch
				Item ci = db.GetItem(eID);
				if (ci == null) {
					valid = false;
					sb.Append("The extranet branch item you chose is null.").Append("<br/>");
				} else if (!ci.TemplateName.Equals("Branch")) {
					valid = false;
					sb.Append("Please select a extranet branch item whose template type is a branch.").Append("<br/>");
				}
			} else {
				valid = false;
				sb.Append("The extranet branch selected is producing an empty string or bad ID.").Append("<br/>");
			}

			if (!valid)
				BranchErrorMessage.Text = sb.ToString();

			return valid;
		}

		public override IEnumerable<string> DataSummary {
			get {
				//get langs
				IEnumerable<string> langTitle = new List<string> { "Languages:" };
				IEnumerable<string> langs = from li in LanguageList.Selected.Where(a => a.Selected)
											select string.Format("<span class='value'>{0}</span>", li.Header);
				//get the rest
				IEnumerable<string> others = new List<string> { 
					SummaryStr(Constants.Keys.ExtranetBranch, db.GetItem(ExtranetBranch.Value).Name),							 
					SummaryStr(Constants.Keys.PublishContent, PublishContent.Checked.ToString()),
					SummaryStr(Constants.Keys.Site, SiteItem.Value)
				};
				
				return langTitle.Concat(langs).Concat(others);		
			}
		}

		protected string SummaryStr(string name, string value) {
			return string.Format("{0}: <span class='value'>{1}</span>", name, value);
		}

		public override IEnumerable<KeyValuePair<string, object>> DataDictionary {
			get {
				yield return new KeyValuePair<string, object>(Constants.Keys.Site, SiteItem.Value);
				yield return new KeyValuePair<string, object>(Constants.Keys.ExtranetBranch, ExtranetBranch.Value);
				yield return new KeyValuePair<string, object>(Constants.Keys.Languages, LanguageList.Selected.Where(a => a.Selected));
				yield return new KeyValuePair<string, object>(Constants.Keys.PublishContent, PublishContent.Checked);
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

			ExtranetBranchDC.GetFromQueryString();
			ExtranetBranchDC.Root = Constants.Paths.Branches;
			ExtranetBranchDC.Folder = Constants.ItemIDs.Branches;

			//setup site drop downs
			IEnumerable<ListItem> sites = 
				from val in Sitecore.Configuration.Factory.GetSiteInfoList()
				orderby val.Name
				select new ListItem() { ID = Control.GetUniqueID("I"), Header = val.Name, Value = val.Name, Selected = false };

			foreach (ListItem s in sites) {
				Sitecore.Context.ClientPage.AddControl(SiteItem, s);
			}

			Sitecore.Data.Database db = Sitecore.Configuration.Factory.GetDatabase("master");
			IEnumerable<Language> langs = from val in db.Languages orderby val.Name select val;

			foreach (Language l in langs) {
				ListItem li1 = new ListItem() { ID = Control.GetUniqueID("I"), Header = l.CultureInfo.DisplayName, Value = l.Name, Selected = (l.Name == Sitecore.Context.Language.Name) };
				Sitecore.Context.ClientPage.AddControl(LanguageList, li1);
			}
		}

		#endregion Page Load
	}
}
