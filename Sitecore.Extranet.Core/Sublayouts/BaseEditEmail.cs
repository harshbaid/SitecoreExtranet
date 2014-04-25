using System;
using System.Web.UI.WebControls;
using Sitecore.Extranet.Core.Utility;
using Sitecore.Extranet.Core.Utility.FormText;
using Sitecore.Links;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;

namespace Sitecore.Extranet.Core.Sublayouts.Extranet {
	public abstract class BaseEditEmail : BaseSecurePage {

		protected abstract TextBox OldEmail { get; }
		protected abstract TextBox NewEmail { get; }
		protected abstract TextBox NewEmailConfirm { get; }
		protected abstract Panel EditEmailPL { get; }
		protected abstract Literal EmailMessageText { get; }
		protected abstract HyperLink BackLink { get; }

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);

			//set the current email to the form
			OldEmail.Text = Sitecore.Context.User.Profile.Email;

			BackLink.NavigateUrl = SitecoreUtility.GetItemURL(PreferredDataSource.Parent);
		}

		protected virtual void ProcessEditEmail(object sender, EventArgs e) {
			string newEmail = NewEmail.Text;
			if (Page.IsValid) {
				if (newEmail.Equals(NewEmailConfirm.Text)) {
					string message = "";
					if (UpdateEmail(newEmail, ref message)) {
						OldEmail.Text = newEmail;
						NewEmail.Text = "";
						NewEmailConfirm.Text = "";
						EditEmailPL.Visible = false;
					}
					Response.Redirect(string.Format("{0}?{1}={2}", ExtranetSecurity.EditAccountURL, ExtranetSecurity.QSMessKey, message), true);
				} else {
					EmailMessageText.Text = FormTextUtility.Provider.GetTextByKey("/EditAccount/EmailDoesntMatch");
				}
			} else {
				//form failed possibly captcha
				EmailMessageText.Text = string.Empty;
			}
		}

		public static bool UpdateEmail(string newEmail, ref string message) {
			//get auth helper
			AuthenticationHelper authHelper = new AuthenticationHelper(Sitecore.Security.Authentication.AuthenticationManager.Provider);
			try {
				//get the current user
				User u = Sitecore.Context.User;
				u.Profile.Email = newEmail;
				u.Profile.Save();

				message = FormTextUtility.Provider.GetTextByKey("/EditAccount/EmailHasChanged");

				return true;
			} catch (System.Security.Authentication.AuthenticationException) {
				message = FormTextUtility.Provider.GetTextByKey("/EditAccount/EmailWasntChanged");
			}
			return false;
		}
	}
}
