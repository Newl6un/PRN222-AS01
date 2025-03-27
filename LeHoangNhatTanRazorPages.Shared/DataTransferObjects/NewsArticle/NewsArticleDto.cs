using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.Category;
using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.SystemAccount;
using LeHoangNhatTanRazorPages.Shared.DataTransferObjects.Tag;

namespace LeHoangNhatTanRazorPages.Shared.DataTransferObjects.NewsArticle
{
    public class NewsArticleDto
    {
        public string? NewsArticleId { get; set; }

        public string? NewsTitle { get; set; }

        public string? Headline { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public short? CategoryId { get; set; }

        public bool NewsStatus { get; set; }

        public short? CreatedById { get; set; }

        public short? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public IEnumerable<TagDto>? Tags { get; set; }

        public SystemAccountDto? CreatedBy { get; set; }

        public SystemAccountDto? UpdatedBy { get; set; }

        public CategoryDto? Category { get; set; }
    }
}
