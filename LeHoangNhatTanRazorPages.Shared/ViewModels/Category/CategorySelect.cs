namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Category
{
    public class CategorySelect
    {
        public short CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public short? ParentCategoryId { get; set; }
        public int Level { get; set; } // Mức độ phân cấp
        public bool Selected { get; set; } = false;// Đánh dấu đã chọn

    }
}
