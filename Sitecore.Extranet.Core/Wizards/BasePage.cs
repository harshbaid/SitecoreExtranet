using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Web.UI.XmlControls;
using Sitecore.Web.UI.Pages;

namespace Sitecore.Extranet.Core.Wizards {
	/// <summary>
	/// Represents a wizard page in the sitebuilder appliction. 
	/// Defines the interface used by the sitebuilder to manipulate pages.
	/// </summary>
	public class BasePage : WizardDialogBaseXmlControl {

		public string PageName { get; set; }

		public virtual bool IsValid { get { return true; } }

		public virtual IEnumerable<string> DataSummary {
			get {
				return from val in DataDictionary select string.Format(@"{0}: <span class='value'>{1}</span>", val.Key, val.Value);
			}
		}

		public virtual IEnumerable<KeyValuePair<string, object>> DataDictionary {
			get {
				yield break;
			}
		}
	}
}