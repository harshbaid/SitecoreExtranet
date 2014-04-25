using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Sitecore.Security.Authentication;
using Sitecore.Security.Accounts;
using Sitecore.Security;
using System.Collections.Specialized;
using Sitecore.Extranet.Core.Utility;
using System.Collections.Generic;
using System.Web.SessionState;
using Sitecore.Extranet.Core.Sublayouts.Extranet;

namespace Sitecore.Extranet.UI
{
	public partial class Login : BaseLogin
	{
		#region implement abstract members
		
		protected override Panel LoginPanel { get { return pnlLogin; } }
		protected override Panel LoggedInPanel { get { return pnlLoggedIn; } }
		protected override HyperLink RegisterLink { get { return lnkRegister; } }
		protected override Literal MessageText { get { return ltlMessage; } }
		protected override TextBox Username { get { return txtUser; } }
		protected override TextBox Password { get { return txtPass; } }

		#endregion

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}