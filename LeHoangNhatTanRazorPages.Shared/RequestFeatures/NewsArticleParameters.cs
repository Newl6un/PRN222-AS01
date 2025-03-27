namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public class NewsArticleParameters : RequestParameters
    {
        public NewsArticleParameters() => OrderBy = "CreatedDate";

        public string? Title { get; set; }
        public string? Content { get; set; }
    }

}
