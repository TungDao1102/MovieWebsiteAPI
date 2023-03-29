using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BTLonWebMovie.Models.Authentication
{
	public class Authentication : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.Session.GetString("UserName") == null)
			{
				filterContext.Result = new RedirectToRouteResult(
					new Microsoft.AspNetCore.Routing.RouteValueDictionary
					{
						{ "controller", "Access" },
						{ "action", "Login" }
					});
			}
		}
	}
}
