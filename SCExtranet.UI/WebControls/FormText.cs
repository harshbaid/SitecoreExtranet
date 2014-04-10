using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using SCExtranet.Core.Utility;
using System.IO;
using Sitecore.Data.Items;
using SCExtranet.Core.Utility.FormText;

namespace SCExtranet.UI.WebControls
{
	/// <summary>
	/// FormText is a web control used to get Form Text values specified by the TextKey property from the FormTextProvider specified in the config 
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:FormText runat=server></{0}:FormText>")]
	public class FormText : CompositeControl {
		[Category(""), DefaultValue(""), Localizable(true)]
		public string TextKey {
			[DebuggerStepThrough()]
			get {
				return (ViewState["TextKey"] == null) ? string.Empty : (string)ViewState["TextKey"];
			}
			[DebuggerStepThrough()]
			set {
				ViewState["TextKey"] = value;
			}
		}

		protected override void Render(HtmlTextWriter writer) {
			writer.Write(FormTextUtility.Provider.GetTextByKey(TextKey));
		}

		/// <summary>
		/// prevents the wrapping span tag
		/// </summary>
		/// <param name="writer"></param>
		public override void RenderBeginTag(HtmlTextWriter writer) {
			writer.Write("");
		}

		public override void RenderEndTag(HtmlTextWriter writer) {
			writer.Write("");
		}
	}
}
