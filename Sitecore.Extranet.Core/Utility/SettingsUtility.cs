using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace Sitecore.Extranet.Core.Utility {
	public class SettingsUtility {

		private static Item Settings {
			get {
				return Sitecore.Context.Database.GetItem("/sitecore/System/Modules/Extranet/Settings");
			}
		}

		public static string FromEmailAddress {
			get {
				return (Settings != null) ? Settings["Email From Address"] : string.Empty;
			}
		}
	}
}
