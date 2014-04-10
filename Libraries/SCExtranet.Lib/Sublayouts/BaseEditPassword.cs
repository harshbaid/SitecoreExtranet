using System;
using System.Web.UI.WebControls;
using SCExtranet.Lib.Utility;
using Sitecore.Links;
using Sitecore.Security.Authentication;

namespace SCExtranet.Lib.Sublayouts.Extranet {
	public abstract class BaseEditPassword : BaseSecurePage {

		protected abstract TextBox OldPassword { get; }
		protected abstract TextBox NewPassword { get; }
		protected abstract TextBox NewPasswordConfirm { get; }
		protected abstract Panel EditPassPL { get; }
		protected abstract Literal PasswordMessageText { get; }
		protected abstract HyperLink BackLink { get; }

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);

			BackLink.NavigateUrl = SitecoreUtility.ProcessLink(LinkManager.GetDynamicUrl(PreferredDataSource.Parent));
		}

		protected virtual void ProcessEditPass(object sender, EventArgs e) {
			if (Page.IsValid) {
				if (NewPassword.Text.Equals(NewPasswordConfirm.Text)) {
					string message = "";
					if (UpdatePassword(OldPassword.Text, NewPassword.Text, ref message)) {
						EditPassPL.Visible = false;
					}
					StateUtility.Redirect.To(string.Format("{0}?{1}={2}", ExtranetSecurity.EditAccountURL, ExtranetSecurity.QSMessKey, message), true);
				} else {
					PasswordMessageText.Text = SitecoreUtility.GetStringContent("/Strings/Extranet/EditAccount/PasswordDoesntMatch", LanguageFallback.English);
				}
			} else {
				//form failed possibly captcha
				PasswordMessageText.Text = string.Empty;
			}
		}

		protected static bool UpdatePassword(string oldPassword, string newPassword, ref string message) {
			//get auth helper
			AuthenticationHelper authHelper = new AuthenticationHelper(Sitecore.Security.Authentication.AuthenticationManager.Provider);
			try {
				//check to see if the existing password is correct
				if (!authHelper.ValidateUser(Sitecore.Context.User.Name, oldPassword)) {
					//throw new System.Security.Authentication.AuthenticationException("Incorrect password.");
					message = SitecoreUtility.GetStringContent("/Strings/Extranet/EditAccount/OldPasswordIsIncorrect", LanguageFallback.English);
				} else {
					//get the current user
					System.Web.Security.MembershipUser user = System.Web.Security.Membership.GetUser(Sitecore.Context.User.Name);
					if (user.ChangePassword(oldPassword, newPassword)) {
						message = SitecoreUtility.GetStringContent("/Strings/Extranet/EditAccount/PasswordHasBeenChanged", LanguageFallback.English);
						return true;
					} else {
						//throw new System.Security.Authentication.AuthenticationException("Unable to change password");
						message = SitecoreUtility.GetStringContent("/Strings/Extranet/EditAccount/UnableToChangePassword", LanguageFallback.English);
					}
				}
			} catch (System.Security.Authentication.AuthenticationException) {
				message = SitecoreUtility.GetStringContent("/Strings/Extranet/EditAccount/AuthenticationError", LanguageFallback.English);
			}
			return false;
		}
	}
}
