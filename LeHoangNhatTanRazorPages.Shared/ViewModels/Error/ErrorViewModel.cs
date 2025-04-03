namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Error
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int? StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public ErrorDetails? ErrorDetails { get; set; }
        public bool ShowErrorDetails { get; set; }
    }
}
