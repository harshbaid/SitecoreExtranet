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
using Sitecore.Security.Accounts;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Security.Authentication;
using System.Text;
using System.Collections.Specialized;
using Sitecore;
using System.Net.Mail;
using SCExtranet.Lib.Utility;
using SCExtranet.Lib.Sublayouts.Extranet;

namespace SCExtranet.UI
{
	public partial class Registration : BaseRegistration
	{
		#region implement abstract members

		protected override TextBox Username { get { return txtUser; } }
		protected override TextBox Password { get { return txtPass; } }
		protected override TextBox PasswordConfirm { get { return txtConfirmPass; } }
		protected override TextBox Email { get { return txtEmailAddress; } }
		protected override TextBox EmailConfirm { get { return txtConfirmEmailAddress; } }
		protected override PlaceHolder FormPH { get { return phForm; } }
		protected override PlaceHolder MessagePH { get { return phMessage; } }
		protected override Literal MessageText { get { return ltlMessage; } }

		#endregion

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}