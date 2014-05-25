using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;

namespace Sitecore.Extranet.Core.Utility.FormText {
	public interface IFormTextProvider {

		string GetTextByKey(string TextKey);
		string GetTextByKey(string TextKey, Database db);
	}
}
