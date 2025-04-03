using LeHoangNhatTanRazorPages.Shared.RequestFeatures;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Shared
{
    public class PaginationViewModel
    {
        public MetaData? MetaData { get; set; }
        private readonly Dictionary<string, string?> _queryParams;
        private readonly string _baseUrl;

        public PaginationViewModel()
        {
            _queryParams = new Dictionary<string, string?>();
            _baseUrl = "/";
        }

        public PaginationViewModel(MetaData metaData, HttpRequest request)
        {
            MetaData = metaData;
            _baseUrl = request.Path;

            // Parse query parameters và loại bỏ CurrentPage nếu có
            _queryParams = new Dictionary<string, string?>();

            foreach (var query in request.Query)
            {
                if (!query.Key.Equals("PageNumber", StringComparison.OrdinalIgnoreCase))
                {
                    _queryParams[query.Key] = query.Value.ToString();
                }
            }
        }

        public string GetPageUrl(int pageNumber)
        {
            var queryParams = new Dictionary<string, string?>(_queryParams);

            // Cập nhật hoặc thêm mới CurrentPage
            if (queryParams.ContainsKey("PageNumber"))
            {
                queryParams["PageNumber"] = pageNumber.ToString();
            }
            else
            {
                queryParams.Add("PageNumber", pageNumber.ToString());
            }

            return QueryHelpers.AddQueryString(_baseUrl, queryParams);
        }
    }
}
