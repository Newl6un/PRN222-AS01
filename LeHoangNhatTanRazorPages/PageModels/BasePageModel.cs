using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LeHoangNhatTanRazorPages.PageModels
{
    public class BasePageModel : PageModel
    {
        // Authentication status
        public bool IsAuthenticated => HttpContext.Items.ContainsKey("IsAuthenticated")
            ? (bool)HttpContext.Items["IsAuthenticated"]!
            : false;

        // Role-specific properties
        public bool IsAdmin => HttpContext.Items.ContainsKey("IsAdmin")
            ? (bool)HttpContext.Items["IsAdmin"]!
            : false;

        public bool IsStaff => HttpContext.Items.ContainsKey("IsStaff")
            ? (bool)HttpContext.Items["IsStaff"]!
            : false;

        public bool IsLecturer => HttpContext.Items.ContainsKey("IsLecturer")
            ? (bool)HttpContext.Items["IsLecturer"]!
            : false;

        // User information
        public string EmailAddress => HttpContext.Items.ContainsKey("EmailAddress")
            ? (string)HttpContext.Items["EmailAddress"]!
            : string.Empty;

        public int UserId => HttpContext.Items.ContainsKey("UserId")
            ? (int)HttpContext.Items["UserId"]!
            : 0;

        [TempData]
        public string? StatusMessage { get; set; }

        // Helper method to check if current user is the creator of content
        protected bool IsCreatorOf(int creatorId) => UserId == creatorId;
    }
}
