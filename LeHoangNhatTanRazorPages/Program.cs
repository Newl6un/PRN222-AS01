using LeHoangNhatTanRazorPages.Extensions;
using LeHoangNhatTanRazorPages.Services.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureAdminAccount();
builder.Services.ConfigureLazy();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureSignalR();

var app = builder.Build();

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Add session before authentication middleware
app.UseSession();

app.MapRazorPages();
app.MapHub<NewsHub>("/newsHub");
app.Run();
