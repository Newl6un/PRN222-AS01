using LeHoangNhatTanRazorPages.Shared.ViewModels.Report;

namespace LeHoangNhatTanRazorPages.Services.Interfaces
{
    public interface IReportService
    {
        Task<StatisticsViewModel> GetStatisticsAsync(DateTime startDate, DateTime endDate);
    }
}
