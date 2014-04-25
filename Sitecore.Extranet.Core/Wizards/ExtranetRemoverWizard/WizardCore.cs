using System.Collections.Generic;
using Sitecore.Jobs;
using HtmlLiteral = Sitecore.Web.UI.HtmlControls.Literal;

namespace Sitecore.Extranet.Core.Wizards.ExtranetRemoverWizard {
	public class WizardCore : BaseWizardCore {

		#region Pages
		public static readonly string SelectSitePage = "SitePage";
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

		protected override string ExecuteBtnText { get { return "Remove Extranet >"; } }

		protected override string JobName { get { return "Extranet Remover"; } }

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

		#region Building
		
		protected override BaseLongRunningJob GetJobObject(Job j) {
			return new ExtranetRemover(j);
		}

		#endregion Building
	}
}

