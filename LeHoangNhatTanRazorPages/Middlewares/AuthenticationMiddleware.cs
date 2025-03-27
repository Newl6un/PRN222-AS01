namespace LeHoangNhatTanRazorPages.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get user information from session
            var userEmail = context.Session.GetString("EmailAddress");
            var userRole = context.Session.GetInt32("Role");
            var userId = context.Session.GetInt32("UserId") ?? 0;

            // Set user information in context items for access in PageModel
            context.Items["IsAuthenticated"] = !string.IsNullOrEmpty(userEmail);
            context.Items["IsAdmin"] = userRole == 0;
            context.Items["IsStaff"] = userRole == 1;
            context.Items["IsLecturer"] = userRole == 2;
            context.Items["EmailAddress"] = userEmail ?? string.Empty;
            context.Items["UserId"] = userId;

            await _next(context);
        }
    }
}
