using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using SCExtranet.Lib.Utility;
using Sitecore.Security.Accounts;

namespace SCExtranet.Lib.Sublayouts.Extranet {
	public abstract class BaseRegistration : BaseSublayout {

		protected abstract TextBox Username { get; }
		protected abstract TextBox Password { get; }
		protected abstract TextBox PasswordConfirm { get; }
		protected abstract TextBox Email { get; }
		protected abstract TextBox EmailConfirm { get; }
		protected abstract PlaceHolder FormPH { get; }
		protected abstract PlaceHolder MessagePH { get; }
		protected abstract Literal MessageText { get; }

		protected string LoginURL = Sitecore.Context.Site.LoginPage;

		protected virtual void Page_Load(object sender, EventArgs e) {

			//handle case where the id exists from a registration link
			if (Request.QueryString.HasKey("code")) {
				Guid userKey = new Guid(Request.QueryString.Get("code"));
				if (RegisterUser(userKey)) {
					//redirect with the same querystring. sometimes it will include the return url
					NameValueCollection nvc = new NameValueCollection();
					nvc.Set("activated", "true");
					nvc.Set("code", "");
					Sitecore.Web.WebUtil.Redirect(LoginURL + Request.QueryString.ToQueryString(nvc));
				}
			}

			//if you're logged in and you've got permissions to this site then redirect to home
			if (Sitecore.Context.IsLoggedIn && Sitecore.Context.User.Roles.Where(a => a.Name.Contains(Sitecore.Context.Domain.Name)).Any()) {
				Sitecore.Web.WebUtil.Redirect("/");
			}
		}

		protected virtual void ProcessRegistration(object sender, EventArgs e) {

			//check if form is valid
			if (Page.IsValid) {
				string message = "";
				if (SetupAccountAndSendEmail(Username.Text, Email.Text, EmailConfirm.Text, Password.Text, PasswordConfirm.Text, string.Empty, string.Empty, ref message)) {
					//show them the thank you message
					FormPH.Visible = false;
					MessagePH.Visible = true;
				}
				MessageText.Text = message;
			} else {
				//error
				MessageText.Text = SitecoreUtility.GetStringContent("/Strings/Extranet/Register/UnknownError", LanguageFallback.English);
			}
		}

		protected static bool SetupAccountAndSendEmail(string username, string email, string confirmEmail, string password, string confirmPassword, string fullName, string comment, ref string message) {

			bool returnVal = false;

			//if the system isn't storing user prefix then fail
			if (ExtranetSecurity.HasExtranetUserPrefix()) {
				//check if passwords match
				if (password.Equals(confirmPassword)) {
					//check it emails match
					if (email.Equals(confirmEmail, StringComparison.OrdinalIgnoreCase)) {
						//see if user exists
						string domainUser = Sitecore.Context.Domain.GetFullName(ExtranetSecurity.ExtranetUserPrefix() + username);
						if (System.Web.Security.Membership.GetUser(domainUser) == null && !Sitecore.Security.Accounts.User.Exists(domainUser)) {
							try {
								//create user
								User u = Sitecore.Security.Accounts.User.Create(domainUser, password);
								MembershipUser mu = Membership.GetUser(domainUser);
								if (u == null) {
									message = SitecoreUtility.GetStringContent("/Strings/Extranet/Register/UserWasntCreatedProperly", LanguageFallback.English);
								} else {
									u.Profile.Email = email;
									u.Profile.FullName = fullName;
									u.Profile.Comment = comment;
									u.Profile.Save();

									HttpRequest req = HttpContext.Current.Request;
									StringBuilder body = new StringBuilder();
									body.AppendLine(SitecoreUtility.GetStringContent("/Strings/Extranet/Register/EmailHello", LanguageFallback.English) + " " + fullName + ",\r\n" + SitecoreUtility.GetStringContent("/Strings/Extranet/Register/EmailThanksForRegistering", LanguageFallback.English) + " " + req.Url.Host + "\r\n" + SitecoreUtility.GetStringContent("/Strings/Extranet/Register/EmailYourNewPasswordIs", LanguageFallback.English) + ": " + password);
									NameValueCollection qString = new NameValueCollection();
									qString.Set("code", ((Guid)mu.ProviderUserKey).ToString());
									//if there's a querystring value and it's in the raw path then remove it.
									string path = (string.IsNullOrEmpty(req.Url.Query) == false && req.RawUrl.Contains(req.Url.Query)) ? req.RawUrl.Replace(req.Url.Query, "") : req.RawUrl;
									body.AppendLine().AppendLine(SitecoreUtility.GetStringContent("/Strings/Extranet/Register/EmailMessage", LanguageFallback.English) + ": http://" + req.Url.Host + path + req.QueryString.ToQueryString(qString) + ".");

									EmailUtility.SendMail(EmailUtility.GetFromAddress(), email, SitecoreUtility.GetStringContent("/Strings/Extranet/Register/EmailNewUserSubject", LanguageFallback.English), body.ToString());

									returnVal = true;
								}
							} catch (System.Web.Security.MembershipCreateUserException ex) {
								message = ex.ToString() + "<br/>" + SitecoreUtility.GetStringContent("/Strings/Extranet/Register/ErrorCreatingUser", LanguageFallback.English);
							}
						} else {
							message = username + " " + SitecoreUtility.GetStringContent("/Strings/Extranet/Register/UserAlreadyRegisteredOnThisSite", LanguageFallback.English);
						}
					} else {
						message = SitecoreUtility.GetStringContent("/Strings/Extranet/Register/EmailsDontMatch", LanguageFallback.English);
					}
				} else {
					message = SitecoreUtility.GetStringContent("/Strings/Extranet/Register/PasswordsDontMatch", LanguageFallback.English);
				}
			} else {
				//it's really because the extranet user prefix wasn't setup
				message = ": " + SitecoreUtility.GetStringContent("/Strings/Extranet/Register/UnknownError", LanguageFallback.English);
			}

			return returnVal;
		}

		protected static bool RegisterUser(Guid userKey) {

			MembershipUser newUser = Membership.GetUser(userKey);
			if (newUser != null) {
				User u = (User)User.FromName(newUser.UserName, AccountType.User);
				using (new Sitecore.SecurityModel.SecurityStateSwitcher(Sitecore.SecurityModel.SecurityState.Disabled)) {
					//add this user to the site role
					//also check if the role contains "extranet" to make sure they don't get added to the reader/editor/manager roles
					if (ExtranetSecurity.HasExtranetRole()) {
						List<Role> roles = Sitecore.Context.Domain.GetRoles().Where(a => a.Name.Equals("extranet\\" + ExtranetSecurity.ExtranetRole())).ToList();
						if (roles.Any()) {
							//could also loop through them all if there are multiple
							//need to make sure there is a convention for knowing which to add.
							u.Roles.Add(roles.First());
							return true;
						}
					}
				}
			}
			return false;
		}

	}
}
