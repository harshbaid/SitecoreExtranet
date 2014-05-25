using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Configuration;
using Sitecore.Web;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Extranet.Core.Utility.FormText;
using Sitecore.Security.Accounts;

namespace Sitecore.Extranet.UI {
	public partial class Settings : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			IEnumerable<SiteInfo> infos = Factory.GetSiteInfoList().Where(a => a.Properties.HasKey("ExtranetRole"));
			pnlMessage.Visible = !infos.Any();
			if (!infos.Any())
				return;
			rptSites.DataSource = infos;
			rptSites.DataBind();
		}

		protected string GetUserCount(SiteInfo si) {
			string roleName = string.Format("extranet\\{0} Extranet", si.Name);
			if (Role.Exists(roleName)) {
				Role r = Role.FromName(roleName);
				IEnumerable<User> users = UserManager.GetUsers().Where(a => a.IsInRole(r));
				return (users != null && users.Any()) ? users.Count().ToString() : "0";
			}
			return "0";
		}
	}
}