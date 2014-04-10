using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SCExtranet.Core.Wizards {
	public static class WizardExtensions {

		public static Item TryGetItem(this Database db, string path) {
			Item ret = db.GetItem(path);
			if (null == ret)
				throw new InvalidOperationException(string.Format("Item: {0} was not found in Database: {1}", path, db.Name));
			return ret;
		}

		public static BranchItem TryGetBranch(this BranchRecords branches, string name) {
			var ret = branches.GetMaster(name);
			if (null == ret)
				throw new InvalidOperationException(string.Format("Branch: {0} was not found in Database: {1}", name, branches.Database.Name));
			return ret;
		}

		public static T Get<T>(this Dictionary<string, object> data, string parameter) {
			object ret = null;
			if (data.TryGetValue(parameter, out ret) && ret is T)
				return (T)ret;
			else
				return default(T);
		}
	}
}
