using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
namespace WebAppMVC.Filter
{
    public class OurUserFilter
    {
        public class OurAuthFilter : ActionFilterAttribute, IAuthenticationFilter
        {
            public void OnAuthentication(AuthenticationContext filterContext)
            {
                if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserEmail"])))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }

            public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
            {
                if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Error!!!!"
                    };
                }

            }
        }
    }
}