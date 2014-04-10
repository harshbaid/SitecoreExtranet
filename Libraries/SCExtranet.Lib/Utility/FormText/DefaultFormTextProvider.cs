using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCExtranet.Lib.Utility.FormText {
	public class DefaultFormTextProvider : IFormatProvider {

		public string GetTextByKey(string TextKey){
			return TextKey;//return Sitecore.Configuration.Factory
		}
	}
}
