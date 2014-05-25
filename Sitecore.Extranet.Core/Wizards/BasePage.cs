using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Web.UI.XmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Data;

namespace Sitecore.Extranet.Core.Wizards {
	/// <summary>
	/// Represents a wizard page in the sitebuilder appliction. 
	/// Defines the interface used by the sitebuilder to manipulate pages.
	/// </summary>
	public class BasePage : WizardDialogBaseXmlControl {

		private Database _mdb;
		public Database MasterDB {
			get {
				if (_mdb == null)
					_mdb = Sitecore.Configuration.Factory.GetDatabase("master");
				return _mdb;
			}
		}

		public string PageName { get; set; }

		public virtual bool IsValid { get { return true; } }

		protected override void OnLoad(EventArgs e) {

			// similar to is not PostBack.
			if (!Sitecore.Context.ClientPage.IsEvent) {
				InitializeControl();
			}
			base.OnLoad(e);
		}

		protected virtual void InitializeControl(){ }
		
		protected string SummaryStr(string name, string value) {
			return string.Format("{0}: <span class='value'>{1}</span>", name, value);
		}

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