using LeHoangNhatTanRazorPages.DataAccess;
using LeHoangNhatTanRazorPages.DataAccess.Implementations;
using LeHoangNhatTanRazorPages.DataAccess.Interfaces;
using LeHoangNhatTanRazorPages.Repository.Implementation;
using LeHoangNhatTanRazorPages.Repository.Interfaces;
using LeHoangNhatTanRazorPages.Services.Implementations;
using LeHoangNhatTanRazorPages.Services.Interfaces;
using LeHoangNhatTanRazorPages.Shared.Configurations;
using LeHoangNhatTanRazorPages.Utilities;

using Microsoft.EntityFrameworkCore;

namespace LeHoangNhatTanRazorPages.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<FUNewsManagementContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opts =>
            {
                opts.EnableRetryOnFailure();
            }));

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();

            services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<ISystemAccountService, SystemAccountService>();
        }

        public static void ConfigureAdminAccount(this IServiceCollection services)
        {
            services.AddSingleton<AppConfiguration>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var section = config.GetSection("AdminConfig");
                return section.Get<AppConfiguration>()!;
            });
        }

        public static void ConfigureLazy(this IServiceCollection services)
        {
            services.AddScoped(typeof(Lazy<>), typeof(Lazier<>));
        }
    }
}
