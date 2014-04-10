using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCExtranet.Core.Wizards.ExtranetSetupWizard {
	public static class Constants {
		/// <summary>
		/// keys to store field values in the data array
		/// </summary>
		public static class Keys {
			public static readonly string ExtranetBranch = "ExtranetBranch";
			public static readonly string Languages = "Languages";
			public static readonly string Page = "Page";
			public static readonly string PublishContent = "PublishContent";
			public static readonly string Site = "Site";
		}

		public static class Paths {
			public static readonly string Branches = "/sitecore/templates/Branches";
			public static readonly string Sites = "/sitecore/system/Sites";
		}

		public static class ItemIDs {
			public static readonly string Branches = "{BAD98E0E-C1B5-4598-AC13-21B06218B30C}";
		}

		public static class TemplateIDs {
			public static readonly string SiteAttribute = "{3ED5E8A1-C091-4EFE-8000-69E753399786}";
			public static readonly string Site = "{E37DA970-1EE9-495B-8B34-911773ADEA82}";
		}
	}
}
