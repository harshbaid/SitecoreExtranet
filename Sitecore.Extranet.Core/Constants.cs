using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Extranet.Core {
	public static class Constants {

		public class ExtranetAttributes {
			public static readonly string UserPrefix = "ExtranetUserPrefix";
			public static readonly string Role = "ExtranetRole";
			public static readonly string Provider = "ExtranetProvider";
			public static readonly string FromEmail = "EmailFromAddress";
			public static readonly string LoginCount = "LoginCount";
			public static readonly string LoginPage = "loginPage";
			
			public static List<string> Keys {
				get {
					return new List<string> { UserPrefix, Role, FromEmail, LoginCount };
				}
			}
		}

		public class ExtranetParams {
			public static string QSMessKey = "message";
		}

		public class ExtranetPageIDs {
			public static readonly string EditAccountPage = "{65BB2602-A3D0-4E5F-870B-BAB5D2ED9698}";
			public static readonly string EditEmailPage = "{738AE870-3132-4ABE-90DF-F055158E52B7}";
			public static readonly string EditPasswordPage = "{2A68C021-FD67-442B-A6AB-FB8A69E152FA}";
			public static readonly string ExtranetFolder = "{E8A70FE0-666F-427E-9591-4E076CDBB254}";
			public static readonly string ForgotPasswordPage = "{E9B20A1C-9AEA-42AE-9382-3C7123C4FB0C}";
			public static readonly string LoginPage = "{8FAE1C52-C62E-4F1C-84CF-CD6203A025F7}";
			public static readonly string RegisterPage = "{EA7ECF02-BA82-4FDC-9DF3-936589327CEC}";
		}

		public class FormTextIDs {
			public static readonly string FormTextEntry = "{D2A1C483-B1D5-4EC2-98FC-51B8339FB0E2}";
			public static readonly string FormTextFolder = "{31223863-3566-4CE3-899B-051C47345B98}";
		}
	}
}
