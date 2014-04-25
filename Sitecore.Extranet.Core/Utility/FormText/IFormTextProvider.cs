using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Extranet.Core.Utility.FormText {
	public interface IFormTextProvider {

		string GetTextByKey(string TextKey);
	}
}
