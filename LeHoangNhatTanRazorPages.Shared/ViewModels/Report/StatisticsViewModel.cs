using System.ComponentModel.DataAnnotations;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Report
{
    public class StatisticsViewModel
    {
        public int TotalNews { get; set; }
        public int ActiveNews { get; set; }
        public int InactiveNews { get; set; }
        public int Categories { get; set; }
        public List<CategoryStatItem> CategoryStats { get; set; } = new List<CategoryStatItem>();
        public List<DailyStatItem> DailyStats { get; set; } = new List<DailyStatItem>();
    }

    public class CategoryStatItem
    {
        public short CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int Count { get; set; }
    }

    public class DailyStatItem
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }

        // Ngày định dạng
        public string FormattedDate => Date.ToString("dd/MM/yyyy");
    }

    public class DateRangeViewModel
    {
        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        [Display(Name = "Từ ngày")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        [Display(Name = "Đến ngày")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
