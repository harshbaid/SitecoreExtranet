using System;
using System.Web.UI.WebControls;
using SCExtranet.Core.Sublayouts.Extranet;

namespace SCExtranet.UI
{
	public partial class EditAccount : BaseEditAccount {

		protected override Literal UsernameText { get { return ltlUsername; } }
		protected override Literal EmailText { get { return ltlEmail; } }
		protected override Literal MessageText { get { return ltlMessage; } }
		protected override HyperLink EmailLink { get { return lnkEmail; } }
		protected override HyperLink PassLink { get { return lnkPass; } }

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}