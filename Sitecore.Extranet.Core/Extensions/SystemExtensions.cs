using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Linq;

namespace Sitecore.Extranet.Core.Extensions {
	public static class NameValueCollectionExtensions {
		public static bool HasKey(this NameValueCollection QString, string Key) {

			foreach (string key in QString.Keys) {
				if (key.Equals(Key)) {
					return true;
				}
			}

			return false;
		}

		public static string ToQueryString(this NameValueCollection QString) {
			return ToQueryString(QString, new NameValueCollection());
		}

		public static string ToQueryString(this NameValueCollection QString, NameValueCollection OverrideKeys) {
			StringBuilder sb = new StringBuilder();

			string append = "?";
			foreach (string key in OverrideKeys.Keys) {
				if (!OverrideKeys[key].ToString().Equals("")) {
					sb.Append(append + key + "=" + HttpUtility.UrlEncode(OverrideKeys[key].ToString()));
					append = "&";
				}
			}

			foreach (string key in QString.Keys) {
				if (!OverrideKeys.HasKey(key) && !QString[key].ToString().Equals("")) {
					sb.Append(append + key + "=" + HttpUtility.UrlEncode(QString[key].ToString()));
				}
				append = "&";
			}

			return sb.ToString();
		}

		public static Dictionary<K, V> Merge<K, V>(this Dictionary<K, V> target, IEnumerable<KeyValuePair<K, V>> source, bool overwrite) {
			source.Aggregate(target, (acc, kvp) => {
				if (!acc.ContainsKey(kvp.Key))
					acc.Add(kvp.Key, kvp.Value);
				else if (overwrite) 
					acc[kvp.Key] = kvp.Value;
				return acc;
			});
			return target;
		}
	}
}
