using System;
using System.Linq;
using System.Web.UI.WebControls;
using SCExtranet.Lib.Utility;

namespace SCExtranet.Lib.Sublayouts.Extranet {
	public abstract class BaseAccountNavigation : BaseSublayout {

		protected abstract Literal UsernameText { get; }
		protected abstract PlaceHolder AccountNavPH { get; }

		private string _EditAccountURL;
		protected string EditAccountURL { 
			get {
				if(string.IsNullOrEmpty(_EditAccountURL))
					_EditAccountURL = ExtranetSecurity.EditAccountURL;
				return _EditAccountURL;
			}
		}

		protected virtual void Page_Load(object sender, EventArgs e) {

			//if you're logged in show the section
			if (Sitecore.Context.IsLoggedIn && Sitecore.Context.User.Roles.Where(a => a.Name.Contains(Sitecore.Context.Domain.Name)).Any()) {
				AccountNavPH.Visible = true;
				string name = Sitecore.Context.User.Profile.FullName;
				if(UsernameText != null)
					UsernameText.Text = (name.Equals("")) ? "Extranet User" : name;
			}
		}

		protected virtual void ProcessLogout(object sender, EventArgs args) {
			Sitecore.Security.Authentication.AuthenticationManager.Logout();
			Response.Redirect(Sitecore.Context.Site.LoginPage);
		}
	}
}
