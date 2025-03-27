namespace LeHoangNhatTanRazorPages.Shared.DataTransferObjects.Category
{
    public class CategoryDto
    {
        public short CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? CategoryDesciption { get; set; }

        public short? ParentCategoryId { get; set; }

        public bool? IsActive { get; set; }

        public CategoryDto? ParentCategory { get; set; }
    }
}
