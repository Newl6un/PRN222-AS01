using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Staff.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<IndexModel> _logger;

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        [BindProperty(SupportsGet = true)]
        public CategoryParameters Parameters { get; set; } = new CategoryParameters();
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();

        public IndexModel(IServiceManager serviceManager, ILogger<IndexModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Kiểm tra quyền truy cập (Staff hoặc Admin)
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }

            try
            {
                Parameters.IncludeParentCategory = true;
                Parameters.IncludeNewsArticles = true;


                var result = await _serviceManager.Category.GetCategoriesAsync(Parameters, false);
                Categories = result;

                // Khởi tạo thông tin phân trang
                Pagination = new PaginationViewModel(
                    result.MetaData,
                Request
                );

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting categories");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải danh sách danh mục";
                return Page();
            }
        }

        public async Task<IActionResult> OnGetCreateAsync()
        {
            var parentCategorySelectList = await _serviceManager.Category.GetParentCategorySelectListAsync();

            return Partial("Partials/_CreateCategory", new CategoryViewModel
            {
                ParentCategorySelectList = parentCategorySelectList
            });
        }

        public async Task<IActionResult> OnPostCreateAsync([FromForm] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ParentCategorySelectList = await _serviceManager.Category.GetParentCategorySelectListAsync();
                return Partial("Partials/_CreateCategory", model);
            }

            try
            {
                await _serviceManager.Category.CreateCategoryAsync(model);
                TempData["SuccessMessage"] = "Tạo danh mục thành công!";

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo danh mục");
            }
        }

        public async Task<IActionResult> OnGetEditAsync(short id)
        {
            var category = await _serviceManager.Category.GetCategoryAsync(id, false);
            if (category == null)
            {
                return NotFound();
            }
            var parentCategorySelectList = await _serviceManager.Category.GetParentCategorySelectListAsync(category.ParentCategoryId);

            category.ParentCategorySelectList = parentCategorySelectList;
            return Partial("Partials/_EditCategory", category);
        }

        public async Task<IActionResult> OnPostEditAsync([FromForm] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ParentCategorySelectList = await _serviceManager.Category.GetParentCategorySelectListAsync(model.ParentCategoryId);
                return Partial("Partials/_EditCategory", model);
            }
            try
            {
                await _serviceManager.Category.UpdateCategoryAsync(model);
                TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật danh mục");
            }
        }

        public async Task<IActionResult> OnGetDeleteAsync(short id)
        {
            var category = await _serviceManager.Category.GetCategoryAsync(id, false);
            if (category == null)
            {
                return NotFound();
            }
            return Partial("Partials/_DeleteCategory", category);
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            try
            {
                await _serviceManager.Category.DeleteCategoryAsync(id);
                TempData["SuccessMessage"] = "Xóa danh mục thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                return StatusCode(500, "Đã xảy ra lỗi khi xóa danh mục");
            }
        }
    }
}
