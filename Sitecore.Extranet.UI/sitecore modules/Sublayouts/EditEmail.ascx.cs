using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Extranet.Core.Sublayouts.Extranet;

namespace Sitecore.Extranet.UI {
	public partial class EditEmail : BaseEditEmail {

		protected override TextBox OldEmail { get { return txtEmailAddressOld; } }
		protected override TextBox NewEmail { get { return txtEmailAddressNew; } }
		protected override TextBox NewEmailConfirm { get { return txtEmailAddressConfirm; } }
		protected override Panel EditEmailPL { get { return pnlEditEmail; } }
		protected override Literal EmailMessageText { get { return ltlMessageEmail; } }
		protected override HyperLink BackLink { get { return lnkBack; } }
		protected override Button SubmitButton { get { return btnSubmit; } }

		protected void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);
		}
	}
}