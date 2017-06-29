using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;
using Sitecore.Pipelines.RenderLayout;
using Sitecore.Text;
using Sitecore.Web;
using System.Web;

namespace Sitecore.Extranet.Core.Pipelines.RenderLayout
{
    public class ExtranetSecurityCheck : SecurityCheck
    {
        public virtual void Process(RequestBeginArgs args)
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
    }
}