using System.Collections.Specialized;
using Sitecore.Web.UI.WebControls;

namespace SCExtranet.Core.Extensions {
	public static class SublayoutExtensions {

		public static NameValueCollection ParameterList(this Sublayout s) {
			return Sitecore.Web.WebUtil.ParseUrlParameters(s.Parameters);
		}
	}
}
