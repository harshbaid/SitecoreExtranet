using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace SCExtranet.Lib.Utility
{
	public static class ExtranetSession
	{

		public static readonly string SessionKey = "ExtranetSession";
		public static readonly string CountKey = "count";
		public static readonly string ExpiryKey = "expires";
		public static readonly int SessionLength = 15;

		public static DateTime ExpiryDate() {
			return DateTime.Parse(Data()[ExpiryKey].ToString());
		}

		public static int Count() {
			Dictionary<string, string> d = Data();
			return int.Parse(d[CountKey].ToString());
		}

		public static void IncreaseCounter() {
			Dictionary<string, string> d = Data();
			int count = int.Parse(d[CountKey].ToString());
			count++;
			d[CountKey] = count.ToString();
			UpdateData(d);
		}

		public static void UpdateData(Dictionary<string, string> data) {
			Session()[SessionKey] = data;
		}

		public static void Reset() {
			HttpSessionState s = Session();
			//create default session values
			Dictionary<string, string> sesData = new Dictionary<string, string>();
			sesData.Add(CountKey, "0");
			sesData.Add(ExpiryKey, DateTime.Now.AddMinutes(SessionLength).ToString());
			s[SessionKey] = sesData;
		}

		public static HttpSessionState Session() {
			return HttpContext.Current.Session;
		}

		public static Dictionary<string, string> Data() {
			HttpSessionState s = Session();
			//start storing how many times a person has tried to login into session 

			if (s[SessionKey] == null) {
				Reset();
			}
			//pull it from session and modify it
			Dictionary<string, string> sesData = (Dictionary<string, string>)s[SessionKey];

			return sesData;
		}
	}
}
