using System;
using System.Web.UI.WebControls;
using SCExtranet.Lib.Utility;
using SCExtranet.Lib.Extensions;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web;

namespace SCExtranet.Lib.Sublayouts.Extranet {
	public abstract class BaseEditAccount : BaseSecurePage {

		protected abstract Literal UsernameText { get; }
		protected abstract Literal EmailText { get; }
		protected abstract Literal MessageText { get; }
		protected abstract HyperLink EmailLink { get; }
		protected abstract HyperLink PassLink { get; }

		protected string GetURL(string template) {
			Item p = PreferredDataSource.ChildByTemplate(template);
			return (p != null) ? SitecoreUtility.ProcessLink(LinkManager.GetDynamicUrl(p)) : string.Empty;
		}

		protected override void Page_Load(object sender, EventArgs e) {
			base.Page_Load(sender, e);

			UsernameText.Text = ExtranetSecurity.GetCurrentUserName();
			EmailText.Text = Sitecore.Context.User.Profile.Email;

			string message = WebUtil.GetQueryString(ExtranetSecurity.QSMessKey, string.Empty);
			if (!string.IsNullOrEmpty(message))
				MessageText.Text = message;

			EmailLink.NavigateUrl = GetURL(Constants.TemplateNames.Page.Extranet.EditEmailPage);
			PassLink.NavigateUrl = GetURL(Constants.TemplateNames.Page.Extranet.EditPasswordPage);
		}
	}
}
