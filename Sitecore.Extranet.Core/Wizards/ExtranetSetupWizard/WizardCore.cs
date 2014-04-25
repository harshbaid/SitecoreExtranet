using System.Collections.Generic;
using System.Linq;
using Sitecore.Extranet.Core.Wizards.ExtranetSetupWizard.Pages;
using Sitecore.Jobs;
using HtmlLiteral = Sitecore.Web.UI.HtmlControls.Literal;

namespace Sitecore.Extranet.Core.Wizards.ExtranetSetupWizard {
	public class WizardCore : BaseWizardCore {

		#region Pages
		public static readonly string SelectSitePage = "SitePage";
		public static readonly string SetSecurityPage = "PagePage";
		#endregion Pages

		#region Controls
		//process steps
		protected HtmlLiteral step1Message;
		protected HtmlLiteral step1Status;
		protected HtmlLiteral step2Message;
		protected HtmlLiteral step2Status;
		#endregion Controls

		#region Settings

		protected override int TotalSteps { get { return 2; } }

		protected override string ExecuteBtnText { get { return "Build Extranet >"; } }

		protected override string JobName { get { return "Extranet Builder"; } }

		#endregion Settings

		#region Control Groupings

		protected override List<HtmlLiteral> MessageFields {
			get {
				return new List<HtmlLiteral>() { step1Message, step2Message };
			}
		}

		protected override List<HtmlLiteral> StatusImages {
			get {
				return new List<HtmlLiteral>() { step1Status, step2Status };
			}
		}

		#endregion Control Groupings

		#region Page Changing

		protected override bool HasCustomPageChangingEvent(string page, string newpage) {
			
			if (!newpage.Equals(SetSecurityPage))
				return false;
			
			SetSecurityPage np = (SetSecurityPage)SiteBuilderPages.Where(a => a.ID.Equals(SetSecurityPage)).First();
			BasePage op = SiteBuilderPages.Where(a => a.ID.Equals(SelectSitePage)).First();
			np.PreviousPage = op;
			np.SetDataContext();

			return true;
		}

		#endregion Page Changing

		#region Building
			
		protected override BaseLongRunningJob GetJobObject(Job j) {
			return new ExtranetBuilder(j);
		}

		#endregion Building
	}
}
