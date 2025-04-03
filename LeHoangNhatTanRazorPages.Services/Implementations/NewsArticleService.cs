using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Hubs;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.News;

using Microsoft.AspNetCore.SignalR;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IHubContext<NewsHub> _newsHubContext;

        public NewsArticleService(IRepositoryManager repositoryManager, IMapper mapper, IHubContext<NewsHub> newsHubContext)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _newsHubContext = newsHubContext;
        }

        public async Task CreateNewsArticleAsync(NewsArticleViewModel newsArticleViewModel)
        {
            var newsArticle = _mapper.Map<NewsArticle>(newsArticleViewModel);

            if (!string.IsNullOrWhiteSpace(newsArticleViewModel.TagNames))
            {
                var tagNames = newsArticleViewModel.TagNames.Split(',').Select(t => t.Trim()).ToList();
                var (existTags, nonExistTags) = await _repositoryManager.Tag.GetTagByNamesAsync(tagNames, true);
                var tags = existTags.ToList();

                //tạo những tag chưa tồn tại
                foreach (var nonExistTag in nonExistTags)
                {
                    var newTag = new Tag
                    {
                        TagName = nonExistTag
                    };
                    await _repositoryManager.Tag.Create(newTag);
                    tags.Add(newTag);
                }

                newsArticle.Tags = tags;
            }

            await _repositoryManager.NewsArticle.Create(newsArticle);
            await _repositoryManager.SaveAsync();
            await _newsHubContext.Clients.All.SendAsync("ReceiveNewsUpdate", $"New article published: {newsArticleViewModel.NewsTitle}");
        }

        public async Task DeleteNewsArticleAsyncAsync(string newsArticleId)
        {
            var newsArticle = await _repositoryManager.NewsArticle.GetNewsArticleAsync(newsArticleId, true);
            if (newsArticle == null) return;

            newsArticle.Tags.Clear();

            _repositoryManager.NewsArticle.Delete(newsArticle);
            await _repositoryManager.SaveAsync();
            await _newsHubContext.Clients.All.SendAsync("ReceiveNewsUpdate", "An article was deleted!");
        }

        public async Task<NewsArticleViewModel?> GetNewsArticleAsync(string newsArticleId, bool trackChanges)
        {
            var newsArticle = await _repositoryManager.NewsArticle.GetNewsArticleAsync(newsArticleId, trackChanges);

            var newsArticleViewModel = _mapper.Map<NewsArticleViewModel>(newsArticle);

            return newsArticleViewModel;
        }

        public async Task<PagedList<NewsArticleViewModel>> GetNewsArticlesAsync(NewsArticleParameters parameters, bool trackChanges)
        {
            var newsArticles = await _repositoryManager.NewsArticle.GetNewsArticlesAsync(parameters, trackChanges);

            var newsArticlesViewModel = _mapper.Map<IEnumerable<NewsArticleViewModel>>(newsArticles);

            return PagedList<NewsArticleViewModel>.ToPagedList(newsArticlesViewModel, newsArticles.MetaData);
        }

        public async Task UpdateNewsArticleAsync(NewsArticleViewModel newsArticleViewModel)
        {
            var newsArticle = await _repositoryManager.NewsArticle.GetNewsArticleAsync(newsArticleViewModel.NewsArticleId!, true);
            _mapper.Map(newsArticleViewModel, newsArticle);
            if (newsArticle == null) return;

            // Lấy danh sách tên tag từ view model
            var tagNames = newsArticleViewModel.TagNames?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (tagNames != null && tagNames.Any())
            {
                // Lấy tag đã có trong DB và tên chưa có
                var (existTags, nonExistTagNames) = await _repositoryManager.Tag.GetTagByNamesAsync(tagNames, trackChanges: true);
                var updatedTags = existTags.ToList();

                // Tạo tag mới cho những tag chưa tồn tại
                foreach (var nonExistTag in nonExistTagNames)
                {
                    var newTag = new Tag
                    {
                        TagName = nonExistTag
                    };
                    await _repositoryManager.Tag.Create(newTag);
                    updatedTags.Add(newTag);
                }

                // Xóa tag không còn được chọn
                foreach (var tag in newsArticle.Tags.ToList())
                {
                    if (!tagNames.Contains(tag.TagName, StringComparer.OrdinalIgnoreCase))
                    {
                        newsArticle.Tags.Remove(tag);
                    }
                }

                // Thêm tag mới (tránh trùng)
                foreach (var tag in updatedTags)
                {
                    if (!newsArticle.Tags.Any(t => t.TagName.Equals(tag.TagName, StringComparison.OrdinalIgnoreCase)))
                    {
                        newsArticle.Tags.Add(tag);
                    }
                }
            }
            else
            {
                // Nếu không có tag nào => xóa hết
                newsArticle.Tags.Clear();
            }

            // Cập nhật các field khác

            await _repositoryManager.SaveAsync();
            await _newsHubContext.Clients.All.SendAsync("ReceiveNewsEdit", $"Article updated: {newsArticle.NewsTitle}");
        }
    }
}
