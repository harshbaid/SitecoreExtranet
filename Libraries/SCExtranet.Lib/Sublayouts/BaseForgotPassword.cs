using System;
using System.Web;
using System.Web.UI.WebControls;
using SCExtranet.Lib.Utility;
using Sitecore.Security.Accounts;

namespace SCExtranet.Lib.Sublayouts.Extranet {
	public abstract class BaseForgotPassword : BaseSublayout {

		protected abstract PlaceHolder FormPH { get; }
		protected abstract HyperLink RegisterLink { get; }
		protected abstract TextBox Username { get; }
		protected abstract Literal MessageText { get; }

		protected string returnURL;
		public string LoginURL = Sitecore.Context.Site.LoginPage;
		public string RegisterURL = SitecoreUtility.GetStringContent("/Strings/Extranet/PageURLs/RegisterPage", LanguageFallback.English);

		protected virtual void Page_Load(object sender, EventArgs e) {
			//set the return url
			if (Request.QueryString.HasKey("returnUrl") && !Request.QueryString.HasKey("returnUrl").Equals("")) {
				returnURL = Request.QueryString["returnUrl"];
			}

			//add the return url to the form button
			RegisterLink.NavigateUrl = (!string.IsNullOrEmpty(returnURL)) ? RegisterURL + "?returnUrl=" + returnURL : RegisterURL;
		}

		protected virtual void ProcessSubmit(object sender, EventArgs e) {
			if (Page.IsValid) {
				string message = "";
				if (ResetPassAndSendUserAnEmail(Username.Text, ref message)) {
					Username.Text = "";
					FormPH.Visible = false;
				}
				MessageText.Text = message;
			}
		}

		protected static bool ResetPassAndSendUserAnEmail(string username, ref string message) {
			try {
				if (ExtranetSecurity.HasExtranetUserPrefix()) {
					string domainUser = Sitecore.Context.Domain.GetFullName(ExtranetSecurity.ExtranetUserPrefix() + username);
					User u = (User)User.FromName(domainUser, AccountType.User);
					if (!Sitecore.Security.Accounts.User.Exists(domainUser)) {
						//throw new System.Security.Authentication.AuthenticationException(domainUser + " does not exist.");
						message = username + SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/UserDoesntExist", LanguageFallback.English);
					} else if (u != null) {
						System.Web.Security.MembershipUser user = System.Web.Security.Membership.GetUser(domainUser);
						string newPass = user.ResetPassword();

						EmailUtility.SendMail(EmailUtility.GetFromAddress(), u.Profile.Email, SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/EmailResetPasswordSubject", LanguageFallback.English) + " " + HttpContext.Current.Request.Url.Host, SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/EmailHello", LanguageFallback.English) + " " + u.Profile.FullName + ",\r\n" + SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/EmailYourNewPasswordIs", LanguageFallback.English) + ": " + newPass);
						message = SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/NewPasswordWasSent", LanguageFallback.English);

						return true;
					} else {
						message = username + SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/UserDoesntExist", LanguageFallback.English);
					}
				} else {
					message = "." + SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/ConfigurationError", LanguageFallback.English);
				}
			} catch (System.Security.Authentication.AuthenticationException) {
				message = SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/AuthenticationError", LanguageFallback.English);
			} catch (System.Configuration.ConfigurationErrorsException) {
				message = SitecoreUtility.GetStringContent("/Strings/Extranet/ForgotPassword/ConfigurationError", LanguageFallback.English);
			}
			return false;
		}

	}
}
