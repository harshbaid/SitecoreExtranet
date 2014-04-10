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
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Configuration;
using Sitecore.Web;
using Sitecore.SecurityModel.Cryptography;
using Sitecore.Security.Accounts;
using System.Collections.Specialized;
using SCExtranet.Lib.Utility;
using SCExtranet.Lib.Sublayouts.Extranet;

namespace SCExtranet.UI
{
	public partial class ForgotPassword : BaseForgotPassword
	{
		#region implement abstract members

		protected override PlaceHolder FormPH { get { return phFPForm; } }
		protected override HyperLink RegisterLink { get { return lnkRegister; } }
		protected override TextBox Username { get { return txtUser; } }
		protected override Literal MessageText { get { return ltlMessage; } }

		#endregion

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}