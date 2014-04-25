using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace Sitecore.Extranet.Core.Extensions {
	
	public static class TemplateExtentions {

		public static bool Is(this TemplateItem template, string name) {
			if (template == null)
				return false;

			return (template.Name == name) ? true : (null != template.BaseTemplates.FirstOrDefault(t => Is(t, name)));
		}

		public static bool IsID(this TemplateItem template, string targetTemplateId) {
			if (template == null)
				return false;

			return (template.ID.ToString() == targetTemplateId) ? true : (null != template.BaseTemplates.FirstOrDefault(t => IsID(t, targetTemplateId)));
		}
	}
}
