namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public class TagParameters : RequestParameters
    {
        public TagParameters() => OrderBy = "TagName";

        public string? TagName { get; set; }
    }

}
