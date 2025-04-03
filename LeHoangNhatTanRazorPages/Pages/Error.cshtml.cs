using LeHoangNhatTanRazorPages.Shared.ViewModels.Error;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Diagnostics;

namespace LeHoangNhatTanRazorPages.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly IWebHostEnvironment _environment;

        public ErrorViewModel ErrorData { get; set; } = new ErrorViewModel();

        public ErrorModel(ILogger<ErrorModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnGet(int? statusCode)
        {
            ErrorData.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorData.StatusCode = statusCode;
            ErrorData.ShowErrorDetails = _environment.IsDevelopment();

            // Log lỗi
            _logger.LogError("Error {StatusCode} occurred. RequestId: {RequestId}",
                statusCode, ErrorData.RequestId);
        }
    }
}
