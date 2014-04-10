using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCExtranet.Core.Wizards.ExtranetRemoverWizard {
	public static class Constants {
		/// <summary>
		/// keys to store field values in the data array
		/// </summary>
		public static class Keys {
			public static readonly string Site = "Site";
		}

		public static class Paths {
			public static readonly string Sites = "/sitecore/system/Sites";
		}

		public static class TempateName {
			public static readonly string ExtranetLogin = "LoginPage";
		}

		public static class TemplateIDs {
			public static readonly string ExtranetFolder = "{E8A70FE0-666F-427E-9591-4E076CDBB254}";
			public static readonly string SiteAttribute = "{3ED5E8A1-C091-4EFE-8000-69E753399786}";
			public static readonly string Site = "{E37DA970-1EE9-495B-8B34-911773ADEA82}";
		}
	}
}
