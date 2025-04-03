using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeHoangNhatTanRazorPages.Pages.Staff.News
{
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<IndexModel> _logger;

        public List<NewsArticleViewModel> NewsArticles { get; set; } = new List<NewsArticleViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        [BindProperty(SupportsGet = true)]
        public NewsArticleParameters Parameters { get; set; } = new NewsArticleParameters();
        public PaginationViewModel Pagination { get; set; } = new PaginationViewModel();

        public IndexModel(
            IServiceManager serviceManager,
            ILogger<IndexModel> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Kiểm tra quyền truy cập (Staff hoặc Admin)
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }
            try
            {

                Parameters.IncludeTags = true;
                Parameters.IncludeCategory = true;
                Parameters.IncludeCreatedBy = true;

                var result = await _serviceManager.NewsArticle.GetNewsArticlesAsync(Parameters, false);

                NewsArticles = result;

                var categoryParameters = new CategoryParameters
                {
                    TakeAll = true
                };

                var categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);

                Categories = categories;



                // Khởi tạo thông tin phân trang
                Pagination = new PaginationViewModel(
                    result.MetaData,
                    Request
                );

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting news articles");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải danh sách tin tức";
                return Page();
            }
        }

        public async Task<IActionResult> OnGetCreateAsync()
        {
            var categoryParameters = new CategoryParameters
            {
                TakeAll = true
            };



            Categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);
            return Partial("Partials/_CreateNews", (new NewsArticleViewModel(), Categories));
        }

        public async Task<IActionResult> OnPostCreateAsync([Bind(Prefix = "item1")] NewsArticleViewModel newsArticle)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }

            var categoryParameters = new CategoryParameters
            {
                TakeAll = true
            };

            Categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);


            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceManager.NewsArticle.CreateNewsArticleAsync(newsArticle);

                    TempData["SuccessMessage"] = "Tạo tin tức thành công!";

                    return new JsonResult(new { success = true });
                }
                catch (Exception)
                {
                    return StatusCode(500, "Đã xảy ra lỗi khi cập nhật tin tức");
                }
            }

            return Partial("Partials/_CreateNews", (newsArticle, Categories));
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var result = await _serviceManager.NewsArticle.GetNewsArticleAsync(id, false);
            if (result == null)
            {
                return NotFound();
            }

            var categoryParameters = new CategoryParameters
            {
                TakeAll = true
            };

            var categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);

            Categories = categories;



            return Partial("Partials/_EditNews", (result, Categories));
        }

        public async Task<IActionResult> OnPostEditAsync([Bind(Prefix = "item1")] NewsArticleViewModel newsArticle)
        {
            var ht = HttpContext.Request;
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }

            var categoryParameters = new CategoryParameters
            {
                TakeAll = true
            };

            var categories = await _serviceManager.Category.GetCategoriesAsync(categoryParameters, false);

            Categories = categories;

            var tagParameters = new TagParameters
            {
                TakeAll = true
            };


            if (!ModelState.IsValid)
            {
                // Nếu dữ liệu không hợp lệ, có thể trả về lại trang hiện tại kèm theo thông báo lỗi
                return Partial("Partials/_EditNews", (newsArticle, Categories));
            }

            try
            {
                await _serviceManager.NewsArticle.UpdateNewsArticleAsync(newsArticle);

                TempData["SuccessMessage"] = "Tạo tin tức thành công!";

                return new JsonResult(new { success = true });
            }
            catch (Exception)
            {
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật tin tức");
            }
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var result = await _serviceManager.NewsArticle.GetNewsArticleAsync(id, false);
            if (result == null)
            {
                return NotFound();
            }

            return Partial("Partials/_DeleteNews", result);
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != 0 && userRole != 1)
            {
                return RedirectToPage("/Auth/AccessDenied");
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            try
            {
                await _serviceManager.NewsArticle.DeleteNewsArticleAsyncAsync(id);
                TempData["SuccessMessage"] = "Xóa tin tức thành công!";
                return new JsonResult(new { success = true });
            }
            catch (Exception)
            {
                return StatusCode(500, "Đã xảy ra lỗi khi xóa tin tức");
            }
        }

    }
}
