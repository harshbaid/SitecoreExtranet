using System;
using System.Web.UI.WebControls;
using SCExtranet.Core.Utility;
using SCExtranet.Core.Utility.FormText;
using Sitecore.Links;
using Sitecore.Security.Authentication;

namespace SCExtranet.Core.Sublayouts.Extranet {
	public abstract class BaseEditPassword : BaseSecurePage {

		protected abstract TextBox OldPassword { get; }
		protected abstract TextBox NewPassword { get; }
		protected abstract TextBox NewPasswordConfirm { get; }
		protected abstract Panel EditPassPL { get; }
		protected abstract Literal PasswordMessageText { get; }
		protected abstract HyperLink BackLink { get; }

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);

			BackLink.NavigateUrl = SitecoreUtility.GetItemURL(PreferredDataSource.Parent);
		}

		protected virtual void ProcessEditPass(object sender, EventArgs e) {
			if (Page.IsValid) {
				if (NewPassword.Text.Equals(NewPasswordConfirm.Text)) {
					string message = "";
					if (UpdatePassword(OldPassword.Text, NewPassword.Text, ref message)) {
						EditPassPL.Visible = false;
					}
					Response.Redirect(string.Format("{0}?{1}={2}", ExtranetSecurity.EditAccountURL, ExtranetSecurity.QSMessKey, message), true);
				} else {
					PasswordMessageText.Text = FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordDoesntMatch");
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
					message = FormTextUtility.Provider.GetTextByKey("/EditAccount/OldPasswordIsIncorrect");
				} else {
					//get the current user
					System.Web.Security.MembershipUser user = System.Web.Security.Membership.GetUser(Sitecore.Context.User.Name);
					if (user.ChangePassword(oldPassword, newPassword)) {
						message = FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordHasBeenChanged");
						return true;
					} else {
						//throw new System.Security.Authentication.AuthenticationException("Unable to change password");
						message = FormTextUtility.Provider.GetTextByKey("/EditAccount/UnableToChangePassword");
					}
				}
			} catch (System.Security.Authentication.AuthenticationException) {
				message = FormTextUtility.Provider.GetTextByKey("/EditAccount/AuthenticationError");
			}
			return false;
		}
	}
}
