using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore;
using Sitecore.Data.Managers;
using Sitecore.Jobs;
using Sitecore.Shell.Framework;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Extranet.Core.Extensions;
using HtmlLiteral = Sitecore.Web.UI.HtmlControls.Literal;

namespace Sitecore.Extranet.Core.Wizards {
	public abstract class BaseWizardCore : WizardForm {

		#region Pages
		public static readonly string ProcessPage = "ProcessPage";
		public static readonly string SummaryPage = "SummaryPage";
		public static readonly string LastPage = "LastPage";
		#endregion Pages

		#region Controls
		protected HtmlLiteral Choices;
		protected HtmlLiteral FinalMessage;
		//process build id
		protected Edit HandleId;
		#endregion Controls

		#region Settings

		protected virtual int RefreshTime { get { return 200; } }

		protected abstract int TotalSteps { get; }

		protected abstract string ExecuteBtnText { get; }

		protected abstract string JobName { get; }

		#endregion Settings

		#region Control Groupings

		protected abstract List<HtmlLiteral> MessageFields { get; }

		protected abstract List<HtmlLiteral> StatusImages { get; }

		protected Dictionary<StatusType, ImageSet> StatusTypes {
			get {
				return new Dictionary<StatusType, ImageSet>() {
					{ StatusType.progress, new ImageSet(){ Src="Images/Progress.gif", Height=17, Width=94 } },
					{ StatusType.failed, new ImageSet(){ Src="Applications/32x32/delete.png", Height=32, Width=32 } },
					{ StatusType.passed, new ImageSet(){ Src="Applications/32x32/check.png", Height=32, Width=32 } },
					{ StatusType.queued, new ImageSet(){ Src="People/32x32/stopwatch.png", Height=32, Width=32 } }
				};
			}
		}

		public enum StatusType { progress, failed, passed, queued };

		protected class ImageSet {
			public string Src;
			public int Height;
			public int Width;
		}

		#endregion Control Groupings

		#region Properties

		protected Job BuildJob {
			get {
				Handle handle = Handle.Parse(HandleId.Value);
				return JobManager.GetJob(handle);
			}
		}

		/// <summary>
		/// Returns a sequence of the <see cref="SiteBuilderPageBase"/> controls the wizard contains.
		/// </summary>
		protected IEnumerable<BasePage> SiteBuilderPages {
			get {
				// for each page id, find the control
				var q = from string val in this.Pages select Context.ClientPage.FindControl(val);
				// return only sitebuilder pages, cast to sitebuilder pages.
				return q.OfType<BasePage>().Cast<BasePage>();
			}
		}

		/// <summary>
		/// Returns the current page of wizard as a <see cref="SiteBuilderPageBase"/> or null if the current page is not of that type.
		/// </summary>
		protected BasePage CurrentSiteBuilderPage {
			get {
				var ret = Context.ClientPage.FindControl(this.Active) as BasePage;
				return ret;
			}
		}

		#endregion Properties

		#region Page Changing

		protected virtual bool HasCustomPageChangingEvent(string page, string newpage) { return false; }
		
		/// <summary>
		/// Validates each SiteBuilderPage before allowing the navigation to continue.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="formEventArgs"></param>
		protected override void OnNext(object sender, EventArgs formEventArgs) {
			if (null != CurrentSiteBuilderPage) {
				if (CurrentSiteBuilderPage.IsValid) {
					base.OnNext(sender, formEventArgs);
				}
			} else {
				base.OnNext(sender, formEventArgs);
			}
		}

		/// <summary>
		/// Provides logic keyed to navigation
		/// </summary>
		/// <param name="page"></param>
		/// <param name="newpage"></param>
		/// <returns></returns>
		protected override bool ActivePageChanging(string page, ref string newpage) {

			NextButton.Header = "Next >";
			if(HasCustomPageChangingEvent(page, newpage)){
				return true;
			} else if (newpage == SummaryPage) {
				NextButton.Header = ExecuteBtnText;
				// invokes an aggegate function on each sitebuilder page using a new string builder 
				// as the aggregate object to collect the output.
				StringBuilder sb = SiteBuilderPages.Aggregate(new StringBuilder(), (acc, aPage) => {
					acc.AppendFormat(@"<h4>{0}</h4>", aPage.PageName);
					acc.Append(@"<ul>");
					foreach (string val in aPage.DataSummary) {
						acc.AppendFormat("<li>{0}</li>", val);
					}
					acc.Append(@"</ul>");
					return acc;
				});
				Choices.Text = sb.ToString();
			} else if (newpage == ProcessPage) {

				// performs an aggregation function on each sitebuilder page using a new dictionary as the aggregate object.
				// Collects the data from all pages and builds the requested site.
				Dictionary<string, object> data = SiteBuilderPages.Aggregate(new Dictionary<string, object>(), (d, aPage) => d.Merge(aPage.DataDictionary, false));

				//disable the buttons and start the long running process
				NextButton.Visible = false;
				BackButton.Visible = false;

				for (int i = 0; i < TotalSteps; i++) {
					SetStatus((i+1), StatusType.queued, " ");
				}

				Job job = JobManager.Start(new JobOptions(
				  JobName,
				  "Wizard Tools",
				  Sitecore.Context.Site.Name,
				  this,
				  "ProcessBuild",
				  new object[] { data }));
				job.Status.Total = TotalSteps;

				HandleId.Value = job.Handle.ToString();
				SheerResponse.Timer("CheckBuildStatus", RefreshTime);
			}

			return true;
		}

		#endregion Page Changing

		#region Building

		protected abstract BaseLongRunningJob GetJobObject(Job j);

		/// <summary>
		/// long running method called from the JobManager
		/// </summary>
		/// <param name="iterations"></param>
		protected void ProcessBuild(Dictionary<string, object> data) {
			BaseLongRunningJob blrj = GetJobObject(BuildJob);
			blrj.Execute(data);
		}

		/// <summary>
		/// Updates the UI about the status of the build
		/// </summary>
		protected void CheckBuildStatus() {
			try {
				//get message info
				int last = BuildJob.Status.Messages.Count - 1;
				string message = (last > -1) ? BuildJob.Status.Messages[last] : "no messages";

				//set status message
				int step = (int)BuildJob.Status.Processed;
				if (step > 0 && step <= BuildJob.Status.Total) {
					//set last step as finished as long as there is a last step
					if (step > 1) {
						SetStatus(step - 1, StatusType.passed, "Completed.");
					}
					//set current step as in progress
					SetStatus(step, StatusType.progress, message);
				}

				if (!BuildJob.IsDone) {
					SheerResponse.Timer("CheckBuildStatus", RefreshTime);
				} else {
					//on finish the build job adds an additional message so grab the 2nd to last message if it's the last one.
					message = (last > 0) ? BuildJob.Status.Messages[last - 1] : BuildJob.Status.Messages[last];
					BuildComplete((BuildJob.Status.Failed) ? StatusType.failed : StatusType.passed, (BuildJob.Status.Failed) ? "Failed" : "Passed", (message.Length > 0) ? message : "The Site Builder Wizard has completed.");
				}
			} catch (Exception ex) {
				BuildComplete(StatusType.failed, "Check Build Status threw an exception", ex.ToString());
			}
		}

		protected void BuildComplete(StatusType t, string statusText, string message) {
			//set last status
			int step = (int)BuildJob.Status.Processed;
			SetStatus(step, t, "Completed.");

			//set the last message and button states
			ImageSet p = StatusTypes[t];
			FinalMessage.Text = string.Format("Build Completed.  {0}<br/><br/>Status: {1}<br/><br/>Message:<br/><br/>{2}", ThemeManager.GetImage(p.Src, p.Width, p.Height), statusText, message);
			
			//finished. go to the next page
			this.Next();
		}

		protected void SetStatus(int step, StatusType t, string message) {
			int pos = (step > 0) ? step - 1 : 0;
			HtmlLiteral i = StatusImages[pos];
			HtmlLiteral m = MessageFields[pos];
			ImageSet p = StatusTypes[t];
			i.Text = ThemeManager.GetImage(p.Src, p.Width, p.Height);
			m.Text = message;
		}

		#endregion Building

		#region Cancel Wizard

		protected override void OnCancel(object sender, EventArgs formEventArgs) {
			if (this.Active == LastPage) {
				//Windows.Close();
                //TODO: resolve above api call
			} else {
				Context.ClientPage.Start(this, "Confirmation");
			}
		}

		public new void Confirmation(ClientPipelineArgs args) {
			if (null == args.Result) {
				Context.ClientPage.ClientResponse.Confirm("Are you sure you want to close the wizard?");
				args.Suspend(true);
			} else if (args.Result == "yes") {
                //Windows.Close();
                //TODO: resolve above api call
            }
        }

		#endregion Cancel Wizard
	}
}

