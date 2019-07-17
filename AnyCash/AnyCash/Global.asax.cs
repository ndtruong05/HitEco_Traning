using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnyCash
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404 || Context.Response.StatusCode == 403)
            {
                // redirect 301 to Homepage
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            string userIp = HttpContext.Current.Request.UserHostAddress;
            string userBrowser = HttpContext.Current.Request.Browser.Type;
            string requestUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            //Server.ClearError();
            //if (exception.GetType() == typeof(HttpException))
            //{
            //    Response.RedirectToRoute("Page404Error");
            //    Response.End();
            //}
            //else
            //{
            //    Response.RedirectToRoute("SystemError");
            //    Response.End();
            //}
        }
    }
}
