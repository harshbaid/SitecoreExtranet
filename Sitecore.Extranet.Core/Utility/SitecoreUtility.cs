using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Publishing;
using Sitecore.Workflows;

namespace Sitecore.Extranet.Core.Utility {
	public class SitecoreUtility {

		/// <summary>
		/// Retrieves the start item (or homepage item) of the current site.
		/// </summary>
		/// <returns></returns>
		public static Item GetSiteStartItem() {
			return GetSiteStartItem(Sitecore.Context.Language);
		}
		/// <summary>
		/// Retrieves the start item (or homepage item) of the current site.
		/// </summary>
		/// <returns></returns>
		public static Item GetSiteStartItem(Language language) {
			return Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath, language);
		}

		public static string GetItemURL(Item i) {
			return LinkManager.GetItemUrl(i);
		}

		#region Publishing

		public static void SetWorkflowToPublishable(Item i) {
			Item wfs = i.Database.GetItem(i[FieldIDs.WorkflowState]);
			if (wfs == null)
				return;

			if (IsFinalState(wfs))
				return;

			IWorkflow workflow = i.Database.WorkflowProvider.GetWorkflow(i);
			if (workflow == null)
				return;

			foreach (WorkflowState state in workflow.GetStates()) {
				if (!state.FinalState)
					continue;

				using (new EditContext(i, true, false)) {
					i[FieldIDs.WorkflowState] = state.StateID;
				}
			}
		}

		public static bool IsFinalState(Item workflowItem) {
			return ((CheckboxField)workflowItem.Fields[WorkflowFieldIDs.FinalState]).Checked;
		}

		public static void PublishContent(Item i, bool publishChildren) {
			Database webDB = Sitecore.Configuration.Factory.GetDatabase("web");
			PublishContent(i, publishChildren, webDB);
		}

		public static void PublishContent(Item i, bool publishChildren, Database db) {

			//make sure content can be published if it will be
			SetWorkflowToPublishable(i);

			PublishOptions po = new PublishOptions(i.Database, db, Sitecore.Publishing.PublishMode.SingleItem, i.Language, DateTime.Now);
			Publisher publisher = new Publisher(po);
			publisher.Options.RootItem = i;
			publisher.Options.Deep = publishChildren;
			publisher.Publish();
		}

		#endregion Publishing

		#region Sites

		public static IEnumerable<string> SystemSites {
			get {
				return new List<string> { "shell", "login", "admin", "service", "modules_shell", "modules_website", "scheduler", "system", "publisher" };
			}
		}

		#endregion Sites
	}
}
