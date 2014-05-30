using System.Collections.Specialized;
using System.Linq;
using Sitecore.Extranet.Core.Extensions;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Extranet.Core.Utility
{
	public class ExtranetSecurity
	{

		#region Extranet Info

		public static bool HasExtranetUserPrefix() {
			return PropExists(Constants.ExtranetAttributes.UserPrefix);
		}

		public static string ExtranetUserPrefix() {
			return GetProp(Constants.ExtranetAttributes.UserPrefix);
		}

		public static bool HasExtranetRole() {
			return PropExists(Constants.ExtranetAttributes.Role);
		}

		public static string ExtranetRole() {
			return GetProp(Constants.ExtranetAttributes.Role);
		}

		public static bool HasExtranetProvider() {
			return PropExists(Constants.ExtranetAttributes.Provider);
		}

		public static string ExtranetProvider() {
			return GetProp(Constants.ExtranetAttributes.Provider);
		}

		public static bool HasFromEmailAddress() {
			return PropExists(Constants.ExtranetAttributes.FromEmail);
		}

		public static string FromEmailAddress() {
			return GetProp(Constants.ExtranetAttributes.FromEmail);
		}

		public static bool HasLoginCount() {
			return PropExists(Constants.ExtranetAttributes.LoginCount);
		}

		public static int LoginCount() {
			return int.Parse(GetProp(Constants.ExtranetAttributes.LoginCount));
		}

		private static bool PropExists(string propName) {
			return (!string.IsNullOrEmpty(GetProp(propName)));
		}

		private static string GetProp(string propName) {
			NameValueCollection n = Sitecore.Context.Site.Properties;
			return (n.HasKey(propName)) ? n[propName] : "";
		}

		#endregion Extranet Info

		#region Page URL

		protected static string GetPageURL(Item page) {
			return (page != null) ? SitecoreUtility.GetItemURL(page) : string.Empty;
		}

		protected static Item ExtranetFolder {
			get {
				Item home = SitecoreUtility.GetSiteStartItem();
				if (home == null)
					return null;
				return home.ChildByTemplateID(Constants.ExtranetPageIDs.ExtranetFolder);
			}
		}

		protected static Item EditAccountPage {
			get {
				Item ExFolder = ExtranetFolder;
				if (ExFolder == null)
					return null;
				return ExFolder.ChildByTemplateID(Constants.ExtranetPageIDs.EditAccountPage);
			}
		}

		public static string EditAccountURL {
			get {
				return (EditAccountPage == null) ? string.Empty : GetPageURL(EditAccountPage);
			}
		}

		public static string RegisterURL {
			get {
				Item ExFolder = ExtranetFolder;
				if (ExFolder == null)
					return null;
				Item RP = ExFolder.ChildByTemplateID(Constants.ExtranetPageIDs.RegisterPage);
				return (RP == null) ? string.Empty : GetPageURL(RP);
			}
		}

		public static string ForgotPasswordURL {
			get {
				Item ExFolder = ExtranetFolder;
				if (ExFolder == null)
					return null;
				Item FP = ExFolder.ChildByTemplateID(Constants.ExtranetPageIDs.ForgotPasswordPage);
				return (FP == null) ? string.Empty : GetPageURL(FP);
			}
		}
		
		#endregion Page URL

		#region User Info

		public static string GetCurrentUserName(){
			return Sitecore.Context.User.LocalName.Replace(ExtranetUserPrefix(), "");
		}

		#endregion User Info

		#region Login

		public static bool IsLoggedIn(){
			//if you're logged in and you've got permissions to this site then redirect to home
			return (Sitecore.Context.IsLoggedIn && Sitecore.Context.User.Roles.Where(a => a.Name.Contains(Sitecore.Context.Domain.Name)).Any());
		}

		#endregion Login
	}
}
