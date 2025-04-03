namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public class NewsArticleParameters : RequestParameters
    {
        public NewsArticleParameters() => OrderBy = "CreatedDate";

        public short? CategoryId { get; set; }

        public string? Tag { get; set; }

        public bool IncludeTags { get; set; } = false;

        public bool IncludeCategory { get; set; } = false;

        public bool IncludeCreatedBy { get; set; } = false;

        public short? CreatedById { get; set; }

        public bool? NewsStatus { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? ExcludeNewsArticleId { get; set; }
    }

}
