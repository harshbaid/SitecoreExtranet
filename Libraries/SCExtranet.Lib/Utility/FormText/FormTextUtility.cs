using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCExtranet.Lib.Utility.FormText {
	public class FormTextUtility {

		public static IFormTextProvider GetProvider() {
			throw new NotImplementedException();
			//go to the config and instantiate an obj from the extranet providerType. 
			//make sure it's the right type and return it or throw an exception
		}
	}
}
