using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using com.portfolio.website.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace com.portfolio.website.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string controllerName = context.HttpContext.Request.RouteValues["controller"].ToString();
            string actionName = context.HttpContext.Request.RouteValues["action"].ToString();
            string areaName = (context.HttpContext.Request.RouteValues["area"] ?? "").ToString();

            var userId = context.HttpContext.GetValue("userId");
            
            if(controllerName.ToLower() == "home" || controllerName.ToLower() == "user")
            {
                return;
            }

            else
            {
                if(string.IsNullOrEmpty(userId))
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "Area", ""},
                            { "Controller", "User"},
                            { "Action", "Login"}
                        }
                        
                       );
                }
            }
        }
    }
}
