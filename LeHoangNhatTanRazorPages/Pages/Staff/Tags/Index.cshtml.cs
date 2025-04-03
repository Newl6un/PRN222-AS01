using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Shared;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Tag;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Staff.Tags
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<IndexModel> _logger;

        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();

        [BindProperty(SupportsGet = true)]
        public TagParameters Parameters { get; set; } = new TagParameters();
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



                var result = await _serviceManager.Tag.GetTagsAsync(Parameters, false);
                Tags = result;

                // Khởi tạo thông tin phân trang
                Pagination = new PaginationViewModel(
                    result.MetaData,
                Request
                );

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting tags");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải danh sách thẻ";
                return Page();
            }
        }

        public async Task<IActionResult> OnGetCreateAsync()
        {

            return Partial("Partials/_CreateTag", new TagViewModel());
        }

        public async Task<IActionResult> OnPostCreateAsync([FromForm] TagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Partial("Partials/_CreateTag", model);
            }

            try
            {
                await _serviceManager.Tag.CreateTagAsync(model);
                TempData["SuccessMessage"] = "Tạo thẻ thành công!";

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tag");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo thẻ");
            }
        }

        public async Task<IActionResult> OnGetEditAsync(short id)
        {
            var tag = await _serviceManager.Tag.GetTagAsync(id, false);
            if (tag == null)
            {
                return NotFound();
            }

            return Partial("Partials/_EditTag", tag);
        }

        public async Task<IActionResult> OnPostEditAsync([FromForm] TagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Partial("Partials/_EditTag", model);
            }
            try
            {
                await _serviceManager.Tag.UpdateTagAsync(model);
                TempData["SuccessMessage"] = "Cập nhật thẻ thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag");
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật thẻ ");
            }
        }

        public async Task<IActionResult> OnGetDeleteAsync(short id)
        {
            var tag = await _serviceManager.Tag.GetTagAsync(id, false);
            if (tag == null)
            {
                return NotFound();
            }
            return Partial("Partials/_DeleteTag", tag);
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            try
            {
                await _serviceManager.Tag.DeleteTagAsync(id);
                TempData["SuccessMessage"] = "Xóa thẻ thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tag");
                return StatusCode(500, "Đã xảy ra lỗi khi xóa thẻ");
            }
        }
    }
}
