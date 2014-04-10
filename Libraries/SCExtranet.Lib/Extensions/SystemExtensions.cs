using System.Collections.Specialized;

namespace SCExtranet.Lib.Extensions {
	public static class NameValueCollectionExtensions {
		public static bool HasKey(this NameValueCollection QString, string Key) {

			foreach (string key in QString.Keys) {
				if (key.Equals(Key)) {
					return true;
				}
			}

			return false;
		}
	}
}
