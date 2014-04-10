using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Data.Items;

namespace SCExtranet.Core.Utility.FormText {
	
	/// <summary>
	/// DefaultFormTextProvider pulls form text from the SCExtranet.config file
	/// </summary>
	public class DefaultFormTextProvider : IFormTextProvider {

		public string GetTextByKey(string TextKey) {
			Item i = Sitecore.Context.Database.GetItem(string.Format("/sitecore/System/Modules/Extranet/FormText{0}", TextKey));
			return (i != null) ? i["Value"] : string.Empty;
		}
	}
}
