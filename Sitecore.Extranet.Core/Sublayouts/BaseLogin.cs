using System;
using System.Web.UI.WebControls;
using Sitecore.Extranet.Core.Utility;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Extranet.Core.Utility.FormText;
using System.Web.Security;

namespace Sitecore.Extranet.Core.Sublayouts.Extranet {
	public abstract class BaseLogin : BaseSublayout {

		protected abstract Panel LoginPanel { get; }
		protected abstract Panel LoggedInPanel { get; }
		protected abstract HyperLink RegisterLink { get; }
		protected abstract Literal MessageText { get; }
		protected abstract TextBox Username { get; }
		protected abstract TextBox Password { get; }
		protected abstract Button SubmitButton { get; }

		private string _returnURL;
		protected string returnURL {
			get {
				if (string.IsNullOrEmpty(_returnURL)) {
					if (Request.QueryString.HasKey("returnUrl") && !Request.QueryString.HasKey("returnUrl").Equals(""))
						_returnURL = Request.QueryString["returnUrl"];
					else
						_returnURL = string.Empty;
				}
				return _returnURL;
			}
		}

		private string _RegisterURL;
		public virtual string RegisterURL {
			get {
				if (string.IsNullOrEmpty(_RegisterURL))
					_RegisterURL = ExtranetSecurity.RegisterURL;
				return _RegisterURL;
			}
		}
		private string _ForgotPasswordURL;
		public virtual string ForgotPasswordURL {
			get {
				if (string.IsNullOrEmpty(_ForgotPasswordURL))
					_ForgotPasswordURL = ExtranetSecurity.ForgotPasswordURL;
				return _ForgotPasswordURL;
			}
		}

		protected virtual void Page_Load(object sender, EventArgs e) {

			SubmitButton.Text = FormTextUtility.Provider.GetTextByKey("/Login/Login");

			//if you're logged in and you've got permissions to this site then redirect to home
			if (ExtranetSecurity.IsLoggedIn()) {
				if (!string.IsNullOrEmpty(returnURL)) {
					Sitecore.Web.WebUtil.Redirect(returnURL);
				}

				//hide login if you didn't redirect
				LoginPanel.Visible = false;
				LoggedInPanel.Visible = true;
			} else {
				//show login and hide logged in content
				LoginPanel.Visible = true;
				LoggedInPanel.Visible = false;
			}

			//add the return url to the form button
			RegisterLink.NavigateUrl = (!string.IsNullOrEmpty(returnURL)) ? RegisterURL + "?returnUrl=" + returnURL : RegisterURL;

			//if you've been redirected from an activation then show messaging
			if (Request.QueryString.HasKey("activated") && !Request.QueryString.HasKey("activated").Equals("true")) {
				//show a message explaining the user what happened.
				MessageText.Text = FormTextUtility.Provider.GetTextByKey("/Login/AccountActivated");
			}
		}

		protected virtual void ProcessLogin(object sender, EventArgs e) {

			if (Page.IsValid) {
				string message = "";
				if (Login(Username.Text, Password.Text, ref message)) {
					if (!string.IsNullOrEmpty(returnURL)) {
						Sitecore.Web.WebUtil.Redirect(returnURL);
					} else {
						Sitecore.Web.WebUtil.Redirect(Sitecore.Context.Site.LoginPage);
					}
				} else {
					MessageText.Text = message;
				}
			}
		}

		protected virtual bool Login(string username, string password, ref string message) {

			//if the session is old reset it 
			if (ExtranetSession.ExpiryDate().CompareTo(DateTime.Now) < 1) {
				ExtranetSession.Reset();
			}
			//increase the counter
			ExtranetSession.IncreaseCounter();
			//only try to login a limited amount of times
			if (ExtranetSession.Count() < ExtranetSecurity.LoginCount()) {
				if (ExtranetSecurity.HasExtranetUserPrefix()) {
					try {
						Sitecore.Security.Domains.Domain domain = Sitecore.Context.Domain;
						string domainUser = domain.Name + @"\" + ExtranetSecurity.ExtranetUserPrefix() + username;
						if (Sitecore.Security.Authentication.AuthenticationManager.Login(domainUser, password, false)) {
							//if you pass the login attempt but you're not logged in, that means there's no security attached to your user.
							if (ExtranetSecurity.IsLoggedIn()) {
								ExtranetSession.Reset();
								return true;
							} else {
								//users with no roles never activated their accounts
								message = FormTextUtility.Provider.GetTextByKey("/Login/UserRegisteredNotActivated");
							}
						} else {
							//throw new System.Security.Authentication.AuthenticationException("Invalid username or password.");
							message = FormTextUtility.Provider.GetTextByKey("/Login/InvalidUsernameOrPassword");
						}
					} catch (System.Security.Authentication.AuthenticationException) {
						//generic error
						message = FormTextUtility.Provider.GetTextByKey("/Login/AuthenticationError");
					}
				} else {
					//actually an error because the extranet user prefix wasn't setup properly
					message = ": " + FormTextUtility.Provider.GetTextByKey("/Login/AuthenticationError");
				}
			} else {
				//too many attempts to login.
				message = FormTextUtility.Provider.GetTextByKey("/Login/TooManyAttempts");
			}
			return false;
		}

	}
}
