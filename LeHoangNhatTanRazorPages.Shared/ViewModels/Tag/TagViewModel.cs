using System.ComponentModel.DataAnnotations;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Tag
{
    public class TagViewModel
    {
        public int TagId { get; set; }

        [Required(ErrorMessage = "Tên tag là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên tag không được vượt quá 50 ký tự")]
        [Display(Name = "Tên tag")]
        public string? TagName { get; set; }

        [StringLength(400, ErrorMessage = "Ghi chú không được vượt quá 400 ký tự")]
        [Display(Name = "Ghi chú")]
        public string? Note { get; set; }
    }
}
