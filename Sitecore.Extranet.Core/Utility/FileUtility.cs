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
			string filePath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, file);
			File.Delete(filePath);
		}

		public static void MakeFile(string file, string contents) {
			string filePath = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, file);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(contents);
			}
		}
	}
}
