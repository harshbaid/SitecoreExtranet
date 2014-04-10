using System;
using SCExtranet.Core.Utility;

namespace SCExtranet.Core.Sublayouts.Extranet {
	public class BaseSecurePage : BaseSublayout {

		protected virtual void Page_Load(object sender, EventArgs e) {
			//if you're not logged in you shouldn't be on this page.
			if (!ExtranetSecurity.IsLoggedIn())
				Response.Redirect(Sitecore.Context.Site.LoginPage);
		}
	}
}
