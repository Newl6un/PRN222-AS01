using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Category
{
    public class CategoryViewModel
    {
        public short CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Mô tả danh mục là bắt buộc")]
        [StringLength(250, ErrorMessage = "Mô tả danh mục không được vượt quá 250 ký tự")]
        [Display(Name = "Mô tả")]
        public string? CategoryDesciption { get; set; }

        [Display(Name = "Danh mục cha")]
        public short? ParentCategoryId { get; set; }

        [Display(Name = "Tên danh mục cha")]
        public string? ParentCategoryName { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }

        [Display(Name = "Trạng thái")]
        public string StatusText => IsActive == true ? "Hoạt động" : "Không hoạt động";

        // Số lượng tin tức thuộc danh mục
        public int NewsCount { get; set; }

        // Danh sách danh mục con
        public List<CategoryViewModel> SubCategories { get; set; } = new List<CategoryViewModel>();

        // Cho form chọn danh mục cha
        public IEnumerable<SelectListItem>? ParentCategorySelectList { get; set; }
    }
}
