using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.News
{
    public class NewsArticleViewModel
    {
        public string? NewsArticleId { get; set; }

        [Required(ErrorMessage = "Tiêu đề tin tức là bắt buộc")]
        [StringLength(400, ErrorMessage = "Tiêu đề tin tức không được vượt quá 400 ký tự")]
        [Display(Name = "Tiêu đề tin tức")]
        public string? NewsTitle { get; set; }

        [Required(ErrorMessage = "Tiêu đề phụ là bắt buộc")]
        [StringLength(150, ErrorMessage = "Tiêu đề phụ không được vượt quá 150 ký tự")]
        [Display(Name = "Tiêu đề phụ")]
        public string? Headline { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Nội dung tin tức là bắt buộc")]
        [StringLength(4000, ErrorMessage = "Nội dung tin tức không được vượt quá 4000 ký tự")]
        [Display(Name = "Nội dung tin tức")]
        public string? NewsContent { get; set; }

        // Hiển thị nội dung ngắn gọn (cho danh sách tin tức)
        public string SnippetContent =>
            NewsContent?.Length > 400
                ? NewsContent.Substring(0, 400) + "..."
                : NewsContent ?? string.Empty;

        [StringLength(400, ErrorMessage = "Nguồn tin tức không được vượt quá 400 ký tự")]
        [Display(Name = "Nguồn tin tức")]
        public string? NewsSource { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [Display(Name = "Danh mục")]
        public short? CategoryId { get; set; }

        [Display(Name = "Tên danh mục")]
        public string? CategoryName { get; set; }

        [Display(Name = "Trạng thái")]
        public bool NewsStatus { get; set; } = true;

        [Display(Name = "Trạng thái")]
        public string StatusText => NewsStatus ? "Hoạt động" : "Không hoạt động";

        public short? CreatedById { get; set; }

        [Display(Name = "Người tạo")]
        public string? CreatorName { get; set; }

        public short? UpdatedById { get; set; }
        public string? UpdaterName { get; set; }

        [Display(Name = "Ngày chỉnh sửa")]
        public DateTime? ModifiedDate { get; set; }

        // Định dạng ngày tháng hiển thị
        public string FormattedCreatedDate => CreatedDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
        public string FormattedModifiedDate => ModifiedDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

        // Cho form chọn danh mục
        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }

        private string _tagNames = string.Empty;
        public string TagNames
        {
            get
            {
                var tagList = new List<string>();

                // Nếu _tagNames có giá trị thủ công (ví dụ được set trước), tách và thêm từng tag
                if (!string.IsNullOrWhiteSpace(_tagNames))
                {
                    tagList.AddRange(
                        _tagNames
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => t.Trim())
                    );
                }

                // Thêm các TagName từ danh sách Tags
                if (Tags != null && Tags.Any())
                {
                    tagList.AddRange(Tags.Select(t => t.TagName).Where(t => !string.IsNullOrWhiteSpace(t)));
                }

                return string.Join(", ", tagList.Distinct());
            }
            set
            {
                _tagNames = value;
            }
        }

        // Tags
        [Display(Name = "Tags")]
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
