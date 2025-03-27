using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LeHoangNhatTanRazorPages.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeRolesAttribute : Attribute, IAuthorizationFilter
    {
        public bool RequireAuthentication { get; set; } = true;
        public bool RequireAdmin { get; set; } = false;
        public bool RequireStaff { get; set; } = false;
        public bool RequireLecturer { get; set; } = false;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = (bool)context.HttpContext.Items["IsAuthenticated"]!;
            var isAdmin = (bool)context.HttpContext.Items["IsAdmin"]!;
            var isStaff = (bool)context.HttpContext.Items["IsStaff"]!;
            var isLecturer = (bool)context.HttpContext.Items["IsLecturer"]!;

            // Authentication check
            if (RequireAuthentication && !isAuthenticated)
            {
                context.Result = new RedirectToPageResult("/Authentication/Login",
                    new { message = "You must login first!" });
                return;
            }

            // Role checks
            if (RequireAdmin && !isAdmin)
            {
                context.Result = new RedirectToPageResult("/Authentication/Login",
                    new { message = "Only administrators can access this page." });
                return;
            }

            if (RequireStaff && !(isStaff || isAdmin))
            {
                context.Result = new RedirectToPageResult("/Authentication/Login",
                    new { message = "Only staff members can access this page." });
                return;
            }

            if (RequireLecturer && !(isLecturer || isStaff || isAdmin))
            {
                context.Result = new RedirectToPageResult("/Authentication/Login",
                    new { message = "You do not have permission to access this page." });
                return;
            }
        }
    }
}
