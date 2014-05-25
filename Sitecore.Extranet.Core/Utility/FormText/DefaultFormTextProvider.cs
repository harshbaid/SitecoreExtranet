using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Extranet.Core.Utility.FormText {
	
	/// <summary>
	/// DefaultFormTextProvider pulls form text from the Sitecore.Extranet.config file
	/// </summary>
	public class DefaultFormTextProvider : IFormTextProvider {

		public string GetTextByKey(string TextKey) {
			return GetTextByKey(TextKey, Sitecore.Context.Database);
		}

		public string GetTextByKey(string TextKey, Database db) {
			Item i = db.GetItem(string.Format("/sitecore/System/Modules/Extranet/FormText{0}", TextKey));
			return (i != null) ? i["Value"] : string.Empty;
		}
	}
}
