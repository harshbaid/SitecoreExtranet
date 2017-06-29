using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;
using Sitecore.Publishing;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using System;
using System.Web;

namespace Sitecore.Extranet.Core.Pipelines.RequestBegin
{
    public class ExtranetSecurityCheck : RequestBeginProcessor
    {
        public override void Process(RequestBeginArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Profiler.StartOperation("Check security access to page.");

            if (!HasAccess())
            {
                args.AbortPipeline();
                var loginPage = GetLoginPage(Context.Site);
                if (loginPage.Length > 0)
                {
                    var urlString = new UrlString(loginPage);
                    if (Settings.Authentication.SaveRawUrl)
                    {
                        urlString.Append("url", HttpUtility.UrlEncode(Context.RawUrl));
                    }

                    var absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                    if (!string.IsNullOrEmpty(absolutePath))
                    {
                        urlString["returnUrl"] = absolutePath;
                    }

                    Tracer.Info("Redirecting to login page \"" + loginPage + "\".");

                    WebUtil.Redirect(urlString.ToString(), false);
                }
                else
                {
                    Tracer.Info("Redirecting to error page as no login page was found.");
                    WebUtil.RedirectToErrorPage("Login is required, but no valid login page has been specified for the site (" + Context.Site.Name + ").", false);
                }
            }

            Profiler.EndOperation();
        }

        /// <summary>
        /// Check credentials.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance has access; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool HasAccess()
        {
            Tracer.Info(string.Concat("Checking security for current user \"", Context.User.Name, "\"."));
            SiteContext site = Context.Site;
            if (site != null && site.RequireLogin && !Context.User.IsAuthenticated && !this.IsLoginPageRequest())
            {
                Tracer.Warning(string.Concat("Site \"", site.Name, "\" requires login and no user is logged in."));
                return false;
            }
            if (site != null && site.DisplayMode != DisplayMode.Normal && !Context.User.IsAuthenticated && string.IsNullOrEmpty(PreviewManager.GetShellUser()) && !this.IsLoginPageRequest())
            {
                Tracer.Warning(string.Concat("Current display mode is \"", site.DisplayMode, "\" and no user is logged in."));
                return false;
            }
            if (Context.Item == null)
            {
                Tracer.Info("Access is granted as there is no current item.");
                return true;
            }
            if (Context.Item.Access.CanRead())
            {
                Tracer.Info(string.Concat("Access granted as the current user \"", Context.GetUserName(), "\" has read access to current item."));
                return true;
            }
            string[] userName = new string[] { "The current user \"", Context.GetUserName(), "\" does not have read access to the current item \"", Context.Item.Paths.Path, "\"." };
            Tracer.Warning(string.Concat(userName));
            return false;
        }

        /// <summary>
        /// Determines whether current request addresses the login page of the site.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if current request addresses the login page of the site; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsLoginPageRequest()
        {
            string loginPage = this.GetLoginPage(Context.Site);
            if (string.IsNullOrEmpty(loginPage))
            {
                return false;
            }
            return HttpContext.Current.Request.RawUrl.StartsWith(loginPage, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets the login page.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        protected virtual string GetLoginPage(SiteContext site)
        {
            if (site == null)
            {
                return string.Empty;
            }
            if (site.DisplayMode == DisplayMode.Normal)
            {
                return site.LoginPage;
            }
            SiteContext siteContext = SiteContext.GetSite("shell");
            if (siteContext == null)
            {
                return string.Empty;
            }
            return siteContext.LoginPage;
        }
    }
}
