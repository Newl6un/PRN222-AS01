using LeHoangNhatTanRazorPages.Extensions;
using LeHoangNhatTanRazorPages.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureAdminAccount();
builder.Services.ConfigureLazy();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMvc().AddRazorPagesOptions(opts => opts.Conventions.AddPageRoute("/NewsArticles/Index", ""));
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Add session before authentication middleware
app.UseSession();

// Add our custom authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();
app.MapRazorPages();

app.Run();
