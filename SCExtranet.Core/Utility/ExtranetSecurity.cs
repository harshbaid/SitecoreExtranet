using System.Collections.Specialized;
using System.Linq;
using SCExtranet.Core.Extensions;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace SCExtranet.Core.Utility
{
	public class ExtranetSecurity
	{

		#region Extranet Info

		public static bool HasExtranetUserPrefix() {
			return (!string.IsNullOrEmpty(ExtranetUserPrefix()));
		}

		public static string ExtranetUserPrefix() {
			NameValueCollection n = Sitecore.Context.Site.Properties;
			return (n.HasKey("ExtranetUserPrefix")) ? n["ExtranetUserPrefix"] : "";
		}

		public static bool HasExtranetRole() {
			return (!string.IsNullOrEmpty(ExtranetRole()));
		}

		public static string ExtranetRole() {
			NameValueCollection n = Sitecore.Context.Site.Properties;
			return (n.HasKey("ExtranetRole")) ? n["ExtranetRole"] : "";
		}

		public static string QSMessKey = "message";

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
