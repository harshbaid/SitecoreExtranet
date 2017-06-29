using Sitecore.Extranet.Core.Extensions;
using Sitecore.Extranet.Core.Utility;
using Sitecore.Extranet.Core.Utility.FormText;
using Sitecore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Extranet.UI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        // GET: Default
        public ActionResult Login(string returnURL = "")
        {
            UserModel model = new UserModel();
            PageLoad(model);
            return View();
        }

        private void PageLoad(UserModel model, string returnURL = "")
        {
            //if you're logged in and you've got permissions to this site then redirect to home
            if (ExtranetSecurity.IsLoggedIn())
            {
                if (!string.IsNullOrEmpty(returnURL))
                {
                    Sitecore.Web.WebUtil.Redirect(returnURL);
                }
                //else
                //{
                //    Sitecore.Web.WebUtil.Redirect("\\");
                //}

                //hide login if you didn't redirect
                model.LoginPanelVisible = false;
                model.LoggedInPanelVisible = true;
            }
            else
            {
                //show login and hide logged in content
                model.LoginPanelVisible = true;
                model.LoggedInPanelVisible = false;
            }

            //if you've been redirected from an activation then show messaging
            if (Request.QueryString.HasKey("activated") && !Request.QueryString.HasKey("activated").Equals("true"))
            {
                //show a message explaining the user what happened.
                model.Message = FormTextUtility.Provider.GetTextByKey("/Login/AccountActivated");
            }
        }

        protected virtual bool Login(string username, string password, ref string message)
        {

            //if the session is old reset it 
            if (ExtranetSession.ExpiryDate().CompareTo(DateTime.Now) < 1)
            {
                ExtranetSession.Reset();
            }
            //increase the counter
            ExtranetSession.IncreaseCounter();
            //only try to login a limited amount of times
            if (ExtranetSession.Count() < ExtranetSecurity.LoginCount())
            {
                if (ExtranetSecurity.HasExtranetUserPrefix())
                {
                    try
                    {
                        Sitecore.Security.Domains.Domain domain = Sitecore.Context.Domain;
                        string domainUser = domain.Name + @"\" + ExtranetSecurity.ExtranetUserPrefix() + username;
                        if (AuthenticationManager.Login(domainUser, password, false))
                        {
                            //if you pass the login attempt but you're not logged in, that means there's no security attached to your user.
                            if (ExtranetSecurity.IsLoggedIn())
                            {
                                ExtranetSession.Reset();
                                return true;
                            }
                            else
                            {
                                //users with no roles never activated their accounts
                                message = FormTextUtility.Provider.GetTextByKey("/Login/UserRegisteredNotActivated");
                            }
                        }
                        else
                        {
                            //throw new System.Security.Authentication.AuthenticationException("Invalid username or password.");
                            message = FormTextUtility.Provider.GetTextByKey("/Login/InvalidUsernameOrPassword");
                        }
                    }
                    catch (System.Security.Authentication.AuthenticationException)
                    {
                        //generic error
                        message = FormTextUtility.Provider.GetTextByKey("/Login/AuthenticationError");
                    }
                }
                else
                {
                    //actually an error because the extranet user prefix wasn't setup properly
                    message = ": " + FormTextUtility.Provider.GetTextByKey("/Login/AuthenticationError");
                }
            }
            else
            {
                //too many attempts to login.
                message = FormTextUtility.Provider.GetTextByKey("/Login/TooManyAttempts");
            }
            return false;
        }

        [HttpPost]
        public ActionResult Login(UserModel usermodel, string returnURL = "")
        {
            string message = "";
            if (Login(usermodel.Username, usermodel.Password, ref message))
            {
                if (!string.IsNullOrEmpty(returnURL))
                {
                    Sitecore.Web.WebUtil.Redirect(returnURL);
                }
                else
                {
                    Sitecore.Web.WebUtil.Redirect("\\");
                }
            }
            else
            {
                usermodel.Message = message;
            }

            return View();
        }

        [HttpGet]
        // GET: Default
        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return View();
        }
    }
}