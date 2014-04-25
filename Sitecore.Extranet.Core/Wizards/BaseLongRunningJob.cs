using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.SecurityModel;
using Sitecore.Globalization;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Data.Items;
using Sitecore.Data;
using System.IO;
using System.Web;
using Sitecore.Extranet.Core.Utility;
using System.Text.RegularExpressions;
using Sitecore.Data.Fields;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Domains;
using Sitecore.Security.Accounts;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Jobs;
using Sitecore.Data.Managers;
using Sitecore.Publishing;
using Sitecore;

namespace Sitecore.Extranet.Core.Wizards {
	public abstract class BaseLongRunningJob {

		protected List<object> CleanupList;
		protected Database MasterDB;
		protected Database WebDB;
		protected Job BuildJob;
		protected Dictionary<string, object> InputData;
		
		public BaseLongRunningJob(Job job) {
			CleanupList = new List<object>();
			BuildJob = job;
			MasterDB = Sitecore.Configuration.Factory.GetDatabase("master");
			WebDB = Sitecore.Configuration.Factory.GetDatabase("web");
		}
		
		#region Messaging
		
		protected int LangCur;
		protected int LangTotal;
		protected int ItemCur;
		protected int ItemTotal;
		
		protected void SetStatus(int processed) {
			SetStatus(processed, string.Empty);
		}
		protected void SetStatus(string message) {
			SetStatus(-1, message);
		}
		protected void SetStatus(int processed, string message) {
			if(processed > -1)
				BuildJob.Status.Processed = processed;
			if(!string.IsNullOrEmpty(message))
				BuildJob.Status.Messages.Add(message);
		}

		#endregion Messaging

		#region Execute

		/// <summary>
		/// Builds a country site in the Sitecore Master database. Performs clean up if the creation fails.
		/// </summary>
		/// <param name="data">Contains parameters to configure the construction. See Lookup.Parameters for detailed information.</param>
		/// <returns>A message indicating the status of the action</returns>
		public void Execute(Dictionary<string, object> data) {

			BuildJob = Sitecore.Context.Job;
			SetStatus(0, "Starting Build.");
			InputData = data;
			CleanupList.Clear();

			try {
				using (new SecurityDisabler()) {
					CoreExecute(data);
				}
				BuildJob.Status.Messages.Add("Finished Successfully");
			} catch (Exception ex) {
				StringBuilder sb = new StringBuilder(ex.ToString());
				CleanupOnFail(ref sb);
				BuildJob.Status.Failed = true;
				BuildJob.Status.Messages.Add(string.Format("The wizard was unable to complete because of the following error(s): <br/>{0}", sb.ToString()));
			}
		}

		public abstract void CoreExecute(Dictionary<string, object> data);

		#endregion 

		#region Security Helper Methods

		/// <summary>
		/// Any settings added to the branches using the "BranchTemplate" roles will be replaced with ones created for this site
		/// </summary>
		/// <param name="i"></param>
		/// <param name="sitename"></param>
		protected void ReplaceSecurity(Item i, string oldRoleName, string newRoleName) {
			string s = i[FieldIDs.Security];
			if (!string.IsNullOrEmpty(s)) {
				using (new EditContext(i)) {
					i[FieldIDs.Security] = s.Replace(oldRoleName, newRoleName);
				}
			}
		}

		/// <summary>
		/// This will create a role from the string name provided
		/// </summary>
		/// <param name="resources">the resource set that keeps track of items to remove</param>
		/// <param name="fullyQualifiedRoleName">name of the role</param>
		/// <returns>The role created or null if it isn't created</returns>
		protected Role CreateRole(string fullyQualifiedRoleName) {
			if (!Role.Exists(fullyQualifiedRoleName)) {
				System.Web.Security.Roles.CreateRole(fullyQualifiedRoleName);
			}
			Role role = Role.FromName(fullyQualifiedRoleName);
			if (role != null) {
				//add to resources for removal on fail
				CleanupList.Add(role);
			}
			return role;
		}

		#endregion Security Helper Methods
		
		#region Helper Methods

		protected void CleanupOnFail(ref StringBuilder message) {
			using (new SecurityDisabler()) {
				foreach (var val in CleanupList) {
					try {
						if (val is Item) {
							(val as Item).Delete();
						} else if (val is DirectoryInfo) {
							(val as DirectoryInfo).Delete(true);
						} else if (val is Role) {
							System.Web.Security.Roles.DeleteRole((val as Role).Name);
						}
					} catch (System.Exception ex) {
						message.AppendLine();
						message.AppendFormat("Failed to cleanup [{0}] because of --> {1}", val.ToString(), ex.Message);
					}
				}
			}
		}

		#endregion Helper Methods	
	}
}
