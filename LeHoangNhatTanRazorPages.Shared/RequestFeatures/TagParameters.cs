namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public class TagParameters : RequestParameters
    {
        public TagParameters() => OrderBy = "TagName";

        public bool IsIncludeNews { get; set; } = false;

        public bool HasMostNews { get; set; } = false;
    }

}
