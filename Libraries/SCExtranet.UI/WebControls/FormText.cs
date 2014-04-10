using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using SCExtranet.Lib.Utility;
using System.IO;
using Sitecore.Data.Items;
using SCExtranet.Lib.Utility.FormText;

namespace SCExtranet.UI.WebControls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:FormText runat=server></{0}:FormText>")]
	public class FormText : CompositeControl {
		[Category(""), DefaultValue(string.Empty), Localizable(true)]
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
			IFormTextProvider p = FormTextUtility.GetProvider();
			if (p == null)
				return;

			writer.Write(p.GetTextByKey(TextKey));
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
