using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Extranet.Core.Sublayouts.Extranet;

namespace Sitecore.Extranet.UI {
	public partial class EditPassword : BaseEditPassword {

		protected override TextBox OldPassword { get { return txtPassOld; } }
		protected override TextBox NewPassword { get { return txtPassNew; } }
		protected override TextBox NewPasswordConfirm { get { return txtPassConfirm; } }
		protected override Panel EditPassPL { get { return pnlEditPass; } }
		protected override Literal PasswordMessageText { get { return ltlMessagePass; } }
		protected override HyperLink BackLink { get { return lnkBack; } }
		protected override Button SubmitButton { get { return btnSubmit; } }

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}