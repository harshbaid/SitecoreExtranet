using System;
using System.Web;
using System.Web.UI.WebControls;
using Sitecore.Extranet.Core.Utility;
using Sitecore.Security.Accounts;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Extranet.Core.Utility.FormText;
using System.Net.Mail;

namespace Sitecore.Extranet.Core.Sublayouts.Extranet {
	public abstract class BaseForgotPassword : BaseSublayout {

		protected abstract PlaceHolder FormPH { get; }
		protected abstract HyperLink RegisterLink { get; }
		protected abstract TextBox Username { get; }
		protected abstract Literal MessageText { get; }
		protected abstract Button SubmitButton { get; }

		protected string returnURL;
		public string LoginURL = Sitecore.Context.Site.LoginPage;
		public string RegisterURL = ExtranetSecurity.RegisterURL;

		protected virtual void Page_Load(object sender, EventArgs e) {

			SubmitButton.Text = FormTextUtility.Provider.GetTextByKey("/ForgotPassword/ResetPassword");

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

		protected virtual bool ResetPassAndSendUserAnEmail(string username, ref string message) {
			try {
				if (ExtranetSecurity.HasExtranetUserPrefix()) {
					string domainUser = Sitecore.Context.Domain.GetFullName(ExtranetSecurity.ExtranetUserPrefix() + username);
					User u = (User)User.FromName(domainUser, AccountType.User);
					if (!Sitecore.Security.Accounts.User.Exists(domainUser)) {
						//throw new System.Security.Authentication.AuthenticationException(domainUser + " does not exist.");
						message = username + FormTextUtility.Provider.GetTextByKey("/ForgotPassword/UserDoesntExist");
					} else if (u != null) {
						System.Web.Security.MembershipUser user = System.Web.Security.Membership.GetUser(domainUser);
						string newPass = user.ResetPassword();

						MailMessage m = new MailMessage();
						m.From = new MailAddress(ExtranetSecurity.FromEmailAddress());
						m.To.Add(new MailAddress(u.Profile.Email));
						m.Subject = string.Format("{0} {1}",
							FormTextUtility.Provider.GetTextByKey("/ForgotPassword/EmailResetPasswordSubject"),
							HttpContext.Current.Request.Url.Host);
						m.Body = string.Format("{0} {1},\r\n{2}: {3}",
							FormTextUtility.Provider.GetTextByKey("/ForgotPassword/EmailHello"),
							u.Profile.FullName,
							FormTextUtility.Provider.GetTextByKey("/ForgotPassword/EmailYourNewPasswordIs"),
							newPass);
						Sitecore.MainUtil.SendMail(m);
						message = FormTextUtility.Provider.GetTextByKey("/ForgotPassword/NewPasswordWasSent");

						return true;
					} else {
						message = username + FormTextUtility.Provider.GetTextByKey("/ForgotPassword/UserDoesntExist");
					}
				} else {
					message = "." + FormTextUtility.Provider.GetTextByKey("/ForgotPassword/ConfigurationError");
				}
			} catch (System.Security.Authentication.AuthenticationException) {
				message = FormTextUtility.Provider.GetTextByKey("/ForgotPassword/AuthenticationError");
			} catch (System.Configuration.ConfigurationException) {
				message = FormTextUtility.Provider.GetTextByKey("/ForgotPassword/ConfigurationError");
			}
			return false;
		}

	}
}
