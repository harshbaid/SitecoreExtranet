using System;
using Sitecore.Extranet.Core.Utility;

namespace Sitecore.Extranet.Core.Sublayouts.Extranet {
	public class BaseSecurePage : BaseSublayout {

		protected virtual void Page_Load(object sender, EventArgs e) {
			//if you're not logged in you shouldn't be on this page.
			if (!ExtranetSecurity.IsLoggedIn())
				Response.Redirect(Sitecore.Context.Site.LoginPage);
		}
	}
}
