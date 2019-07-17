using AnyCash.AppSession;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnyCash.Configuration
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<string> notCheck = new List<string> {
                "/Home/Login",
                "/Home/Logout",
                "/Home/_HomeHeader",
                "/Home/_HomeFooter",
                "/Home/_HomeMenuLeft",
                "/Home/_Pagination"
            };
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionUrl = "/" + controllerName + "/" + actionName;

            if (notCheck.Any(x => string.Equals(x, actionUrl, System.StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            if (HttpContext.Current.Session[AppSessionKeys.USER_INFO] == null)
            {
                string url = HttpContext.Current.Request.CurrentExecutionFilePath;
                if (!string.Equals(actionUrl, "/Home/Login", System.StringComparison.OrdinalIgnoreCase))
                {
                    string redirect = "/Login";
                    if (!string.IsNullOrEmpty(url) && url != "/")
                    {
                        redirect += "?url=" + url;
                    }
                    filterContext.Result = new RedirectResult(redirect);
                    return;
                }
            }
            else
            if (!filterContext.IsChildAction && !filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                //List<SYS_ACTIONS> actions = (List<SYS_ACTIONS>)HttpContext.Current.Session[AppSessionKeys.LIST_ACTION];
                //if (actions.Any(x => string.Equals(x.ACTION_CONTROLPATH, actionUrl, System.StringComparison.OrdinalIgnoreCase)))
                //{
                //    return;
                //}
                //else
                //{
                //    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
                //    {
                //        if (string.Equals(controllerName, "Ajax", System.StringComparison.OrdinalIgnoreCase))
                //        {
                //            Models.AjaxResultModel Result = new Models.AjaxResultModel()
                //            {
                //                Code = 2000,
                //                Result = "Bạn không có quyền truy cập chức năng này"
                //            };
                //            filterContext.Result = new JsonResult { Data = Result };
                //            return;
                //        }
                //        else
                //        {
                //            ContentResult cr = new ContentResult();
                //            cr.Content = ViewRenderer.RazorViewToString<Controllers.HomeController>("_NotPermission", null, true);
                //            filterContext.Result = cr;// ViewRenderer.CreateController<Controllers.HomeController>()._NotPermission();
                //        }
                //    }
                //    else
                //    {
                //        filterContext.Result = new RedirectResult("/Home/NotPermission");
                //    }
                //}
            }
        }
    }
}