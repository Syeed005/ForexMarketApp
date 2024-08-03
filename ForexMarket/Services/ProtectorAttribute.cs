using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ForexMarket.Services {
    public class ProtectorAttribute : Attribute, IActionFilter {
        private readonly IAuthorizer _authorizer;

        public ProtectorAttribute(IAuthorizer authorizer)
        {
            _authorizer = authorizer;
        }
        public void OnActionExecuted(ActionExecutedContext context) {
            if (!_authorizer.IsAdmin()) {
                context.Result = new RedirectToActionResult("Error", "Home", null);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            if (!_authorizer.IsAdmin()) {
                context.Result = new RedirectToActionResult("Error", "Home", null);
            }
        }
    }
}
