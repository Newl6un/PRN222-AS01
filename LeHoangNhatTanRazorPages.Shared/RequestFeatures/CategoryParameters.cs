namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public class CategoryParameters : RequestParameters
    {
        public CategoryParameters() => OrderBy = "CategoryId";

        public bool IsActive { get; set; }

        public bool HasMostPost { get; set; } = false;

        public short? ParentCategoryId { get; set; }

        public bool IncludeNewsArticles { get; set; } = false;

        public bool IncludeSubCategories { get; set; } = false;

        public bool IncludeParentCategory { get; set; } = false;
    }

}
