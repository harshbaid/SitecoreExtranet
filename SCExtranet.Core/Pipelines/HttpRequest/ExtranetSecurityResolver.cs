using System;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.IO;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;
using Sitecore.SecurityModel;
using Sitecore.Sites;
using Sitecore.Web;

namespace SCExtranet.Core.Pipelines.HttpRequest {
	public class ExtranetSecurityResolver : HttpRequestProcessor {
		public override void Process(HttpRequestArgs args) {
			//make sure you're on the right domain and page mode
			if (!Sitecore.Context.Domain.Name.ToLower().Contains("sitecore") && Sitecore.Context.PageMode.IsNormal) {
				// Get the site context 
				SiteContext site = Sitecore.Context.Site;

				// Check if the current user has sufficient rights to enter this page 
				if (SiteManager.CanEnter(site.Name, Sitecore.Context.User)) {
					string prefix = args.StartPath;

					if (args.LocalPath.Contains(Sitecore.Context.Site.StartPath))
						prefix = String.Empty;

					if (Sitecore.Context.Database == null)
						return;

					// Get the item using securityDisabler for restricted items such as permission denied items
					Item contextItem = null;
					using (new SecurityDisabler()) {
						if (Context.Database != null && args.Url.ItemPath.Length != 0) {
							string path = MainUtil.DecodeName(args.Url.ItemPath);
							Item item = args.GetItem(path);
							if (item == null) {
								path = args.LocalPath;
								item = args.GetItem(path);
							}
							if (item == null) {
								path = MainUtil.DecodeName(args.LocalPath);
								item = args.GetItem(path);
							}
							string str2 = (site != null) ? site.RootPath : string.Empty;
							if (item == null) {
								path = FileUtil.MakePath(str2, args.LocalPath, '/');
								item = args.GetItem(path);
							}
							if (item == null) {
								path = MainUtil.DecodeName(FileUtil.MakePath(str2, args.LocalPath, '/'));
								item = args.GetItem(path);
							}
							if (item == null) {
								Item root = ItemManager.GetItem(site.RootPath, Language.Current, Sitecore.Data.Version.Latest, Context.Database, SecurityCheck.Disable);
								if (root != null) {
									string path2 = MainUtil.DecodeName(args.LocalPath);
									item = this.GetSubItem(path2, root);
								}
							}
							if (item == null) {
								int index = args.Url.ItemPath.IndexOf('/', 1);
								if (index >= 0) {
									Item root = ItemManager.GetItem(args.Url.ItemPath.Substring(0, index), Language.Current, Sitecore.Data.Version.Latest, Context.Database, SecurityCheck.Disable);
									if (root != null) {
										string path3 = MainUtil.DecodeName(args.Url.ItemPath.Substring(index));
										item = this.GetSubItem(path3, root);
									}
								}
							}
							if (((item == null) && args.UseSiteStartPath) && (site != null)) {
								item = args.GetItem(site.StartPath);
							}
							contextItem = item;
						}
					}

					//Item contextItem = Sitecore.Context.Item;
					if (contextItem != null) {
						User u = Sitecore.Context.User;
						bool isAllowed = AuthorizationManager.IsAllowed(contextItem, AccessRight.ItemRead, u);

						if (!isAllowed && (site.LoginPage.Length > 0)) {
							// Redirect the user 
							WebUtil.Redirect(String.Format("{0}?returnUrl={1}", site.LoginPage, HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.RawUrl)));
						}
					}
				}
			}
		}

		private Item GetSubItem(string path, Item root) {
			Item child = root;
			foreach (string str in path.Split(new char[] { '/' })) {
				if (str.Length != 0) {
					child = this.GetChild(child, str);
					if (child == null) {
						return null;
					}
				}
			}
			return child;
		}

		private Item GetChild(Item item, string itemName) {
			foreach (Item item2 in item.Children) {
				if (item2.DisplayName.Equals(itemName, StringComparison.OrdinalIgnoreCase)) {
					return item2;
				}
				if (item2.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)) {
					return item2;
				}
			}
			return null;
		}
	}
}
