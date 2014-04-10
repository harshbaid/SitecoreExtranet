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
using SCExtranet.Lib.Utility;
using System.Collections.Specialized;
using SCExtranet.Lib.Sublayouts.Extranet;

namespace SCExtranet.UI
{
	public partial class AccountNavigation : BaseAccountNavigation {
		
		#region Implement abstract members

		protected override Literal UsernameText { get { return ltlUsername; } }
		protected override PlaceHolder AccountNavPH { get { return phAccountNav; } }

		#endregion

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}