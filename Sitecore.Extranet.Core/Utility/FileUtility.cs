using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sitecore.Extranet.Core.Utility {
	public class FileUtility {

		public static void RemoveFile(string file) {

		}

		public static void MakeFile(string file, string contents) {
			string filePath = string.Format(@"{0}{1}", HttpContext.Current.Request.PhysicalApplicationPath, file);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(contents);
			}
		}
	}
}
