using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Extranet.Core.Utility;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Jobs;
using Sitecore.SecurityModel;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;

namespace Sitecore.Extranet.Core.Wizards.ExtranetSetupWizard {
	public class ExtranetBuilder : BaseLongRunningJob {
		
		public ExtranetBuilder(Job job) : base(job) { }

		#region Start Build

		/// <summary>
		/// Builds a country site in the Sitecore Master database. Performs clean up if the creation fails.
		/// </summary>
		/// <param name="data">Contains parameters to configure the construction. See Lookup.Parameters for detailed information.</param>
		/// <returns>A message indicating the status of the action</returns>
		public override void CoreExecute() {

			string siteName = InputData.Get<string>(Constants.Keys.Site);
			SiteInfo SiteItem = Factory.GetSiteInfo(siteName);
			
			IEnumerable<ListItem> langs = InputData.Get<IEnumerable<ListItem>>(Constants.Keys.Languages);
			IEnumerable<Language> selectedLangs = (from val in MasterDB.Languages
													where langs.Any(a => a.Value.Equals(val.Name))
													select val);
					
			string pageID = InputData.Get<string>(Constants.Keys.Page);
			Item PageItem = MasterDB.GetItem(pageID);
					
			//status
			SetStatus(1, "Building extranet pages.");
			
			//create extranet pages from branch
			Item extranetFolder = BuildExtranetPages(SiteItem, selectedLangs);

			//status
			SetStatus(2, "Configuring Security.");

			//create role
			CreateRole(SiteItem);

			//move login page to child of secure page
			Item LoginPage = ConfigureLoginPage(extranetFolder, PageItem, siteName);

			//update site login settings and attributes
			UpdateSite(SiteItem, LoginPage);
		}

		#endregion Start Build

		#region Build Chunks

		protected Item BuildExtranetPages(SiteInfo siteItem, IEnumerable<Language> selectedLangs) {
			LangCur = 1;
			LangTotal = selectedLangs.Count();
			Item extranetFolder = null;
			foreach (Language targetLang in selectedLangs) {
				using (new LanguageSwitcher(targetLang)) {
					
					//get content and media branch and branch ancestor info
					BranchItem extranetBranch = MasterDB.GetItem(InputData.Get<string>(Constants.Keys.ExtranetBranch));
			
					//if adding multiple languages, determines if this is the first language added or not
					bool firstRun = (LangCur == 1);

					if (firstRun) { // if adding multiple languages you only want to do this once
						//status
						BuildJob.Status.Messages.Add(string.Format("Building {0} content from branch.", targetLang.CultureInfo.DisplayName));

						Item HomeItem = MasterDB.GetItem(siteItem.StartItem);
						extranetFolder = HomeItem.Add("extranet", extranetBranch);

						PublishContent(extranetFolder, true);

						CleanupList.Add(extranetFolder); // Register website for cleanup if creation fails. 
					} else {
						// All content items including website item
						IEnumerable<Item> contentItems = new Item[] { extranetFolder }.Concat(extranetFolder.Axes.GetDescendants());

						//add new version for language and update referential links
						ItemCur = 1;
						ItemTotal = contentItems.Count();
						foreach (Item newItem in contentItems) {
							BuildJob.Status.Messages.Add(string.Format("Language {0} of {1}<br/>Item {2} of {3}", LangCur, LangTotal, ItemCur, ItemTotal));

							//create a version of the current language for all the content under website
							Item langVersion = MasterDB.GetItem(newItem.ID, targetLang);
							langVersion = langVersion.Versions.AddVersion();

							//publish content item
							PublishContent(langVersion, false);

							ItemCur++;
						}
					}
				}
				LangCur++;
			}
			return extranetFolder;
		}

		protected void CreateRole(SiteInfo siteItem) {
			//setup roles
			string roleName = string.Format("extranet\\{0} Extranet", siteItem.Name);
			CreateRole(roleName);
		}

		protected Item ConfigureLoginPage(Item extranetFolder, Item newParent, string sitename) {

			Item loginPage = extranetFolder.Paths.GetSubItem("login");

			loginPage.MoveTo(newParent);
				
			//update permissions 			
			string s = loginPage[FieldIDs.Security];
			if (!string.IsNullOrEmpty(s))
				ReplaceSecurity(loginPage, "BranchTemplate", sitename);
			
			PublishContent(loginPage, false);

			CleanupList.Add(loginPage);
			
			return loginPage;
		}

		protected void UpdateSite(SiteInfo siteInfo, Item loginPage) {

			string siteName = siteInfo.Name;
			string loginUrl = string.Format("{0}.aspx", loginPage.Paths.ContentPath.Replace(string.Format("/{0}/Home", siteName), ""));

			//if you've got the multisite manager installed then you'll handle this differently
			Item sFolder = MasterDB.GetItem(Constants.Paths.Sites);
			if (sFolder != null) {
				//setup site drop downs
				IEnumerable<Item> siteRes = sFolder.GetChildren().Where(a => a.DisplayName.Equals(siteName));
				if(!siteRes.Any())
					return;
				Item siteItem = siteRes.First();
				
				using (new EditContext(siteItem)) {
					//set login url on the site node
					siteItem["loginPage"] = loginUrl;
				}

				//set extranet user prefix attributes on site node: ExtranetUserPrefix and ExtranetRole 
				string saIDstr = Constants.TemplateIDs.SiteAttribute;
				if (Sitecore.Data.ID.IsID(saIDstr)) {
					ID saID = null;
					if (ID.TryParse(saIDstr, out saID)) {
						TemplateItem sa = MasterDB.GetItem(saID);
						if (sa != null) {
							Item uPrefix = siteItem.Add("ExtranetUserPrefix", sa);
							if (uPrefix != null) {
								using (new EditContext(uPrefix)) {
									uPrefix["Value"] = string.Format("{0}_", siteName);
								}
								CleanupList.Add(uPrefix);
							}
							Item roleName = siteItem.Add("ExtranetRole", sa);
							if (roleName != null) {
								using (new EditContext(roleName)) {
									roleName["Value"] = string.Format("{0} Extranet", siteName);
								}
								CleanupList.Add(roleName);
							}
							Item providerName = siteItem.Add("ExtranetProvider", sa);
							if (providerName != null) {
								using (new EditContext(providerName)) {
									providerName["Value"] = InputData.Get<string>(Constants.Keys.SecProvider);
								}
								CleanupList.Add(providerName);
							}
						}
					}
				}

				PublishContent(siteItem, true);
			} else {
				StringBuilder fc = new StringBuilder();
				fc.AppendLine("<configuration xmlns:patch=\"http://www.sitecore.net/xmlconfig/\">");
				fc.AppendLine("	<sitecore>");
				fc.AppendLine("		<sites>");
				fc.AppendFormat("			<site name=\"{0}\">", siteName).AppendLine();
				fc.AppendFormat("				<patch:attribute name=\"loginPage\">{0}</patch:attribute>", loginUrl).AppendLine();
				fc.AppendFormat("				<patch:attribute name=\"ExtranetUserPrefix\">{0}_</patch:attribute>", siteName).AppendLine();
				fc.AppendFormat("				<patch:attribute name=\"ExtranetRole\">{0} Extranet</patch:attribute>", siteName).AppendLine();
				fc.AppendFormat("				<patch:attribute name=\"ExtranetProvider\">{0}</patch:attribute>", InputData.Get<string>(Constants.Keys.SecProvider)).AppendLine();
				fc.AppendLine("			</site>");
				fc.AppendLine("		</sites>");
				fc.AppendLine("	</sitecore>");
				fc.AppendLine("</configuration>");
				
				FileUtility.MakeFile(string.Format(Constants.FilePatterns.ExtranetSiteConfig, siteName), fc.ToString());
			}
		}

		#endregion Build Chunks

		#region Publish Helper Methods

		protected void PublishContent(Item i, bool publishChildren) {
			//get the value from the settings page of the InputData
			bool publishContent = InputData.Get<bool>(Constants.Keys.PublishContent);
			if (publishContent) {
				SitecoreUtility.PublishContent(i, publishChildren);
			}
		}

		#endregion Publish Helper Methods
	}
}
