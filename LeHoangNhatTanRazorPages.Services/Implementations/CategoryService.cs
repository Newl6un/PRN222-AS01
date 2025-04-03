using AutoMapper;

using LeHoangNhatTanRazorPages.BO.Models;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.RequestFeatures;
using LeHoangNhatTanRazorPages.Shared.ViewModels.Category;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeHoangNhatTanRazorPages.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public Task CreateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _repositoryManager.Category.Create(category);
            return _repositoryManager.SaveAsync();
        }

        public async Task DeleteCategoryAsync(short categoryId)
        {
            var category = await _repositoryManager.Category.GetByIdAsync(true, categoryId);
            if (category == null)
            {
                throw new Exception("Danh mục không tồn tại");
            }
            _repositoryManager.Category.Delete(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task<PagedList<CategoryViewModel>> GetCategoriesAsync(CategoryParameters parameters, bool trackChanges)
        {
            var categories = await _repositoryManager.Category.GetCategoriesAsync(parameters, trackChanges);

            var categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            return PagedList<CategoryViewModel>.ToPagedList(categoryViewModels, categories.MetaData);
        }

        public async Task<CategoryViewModel> GetCategoryAsync(short categoryId, bool trackChanges)
        {
            var category = await _repositoryManager.Category.GetByIdAsync(trackChanges, categoryId);
            if (category == null)
            {
                throw new Exception("Danh mục không tồn tại");
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return categoryViewModel;
        }

        public async Task<IEnumerable<SelectListItem>> GetParentCategorySelectListAsync(short? parentId = null)
        {
            var categories = await _repositoryManager.Category.GetAllAsync(false);
            // Tạo danh sách các CategorySelectDto để trả về
            var result = new List<CategorySelect>();
            // Xây dựng cây danh mục

            foreach (var rootCategory in categories)
            {
                var categorySelect = new CategorySelect
                {
                    CategoryId = rootCategory.CategoryId,
                    CategoryName = rootCategory.CategoryName,
                    ParentCategoryId = rootCategory.ParentCategoryId,
                    Level = 1
                };
                if (parentId != null && parentId == rootCategory.CategoryId)
                {
                    categorySelect.Selected = true;
                }
                result.Add(categorySelect);

                // Đệ quy để thêm các danh mục con
                AddChildCategories(categories, result, rootCategory.CategoryId, 2);
            }

            var selectList = _mapper.Map<IEnumerable<SelectListItem>>(result);
            return selectList;
        }

        public async Task UpdateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            var category = await _repositoryManager.Category.GetByIdAsync(true, categoryViewModel.CategoryId);

            if (category == null)
            {
                throw new Exception("Danh mục không tồn tại");
            }

            _mapper.Map(categoryViewModel, category);
            _repositoryManager.Category.Update(category);
            await _repositoryManager.SaveAsync();
        }

        private void AddChildCategories(IEnumerable<Category> allCategories, List<CategorySelect> result,
                                int parentId, int level)
        {
            var children = allCategories
                .Where(c => c.ParentCategoryId == parentId && c.CategoryId != c.ParentCategoryId); // tránh tự đệ quy chính mình

            foreach (var child in children)
            {
                result.Add(new CategorySelect
                {
                    CategoryId = child.CategoryId,
                    CategoryName = child.CategoryName,
                    ParentCategoryId = child.ParentCategoryId,
                    Level = level
                });

                // Đệ quy tiếp nếu không tự trỏ chính mình
                AddChildCategories(allCategories, result, child.CategoryId, level + 1);
            }
        }
    }
}
