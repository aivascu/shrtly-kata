using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShrtLy.BLL;
using ShrtLy.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ShrtLyContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ShrtLyContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShrtLyContext>(opt => opt.UseSqlServer("Data Source=localhost;Initial Catalog=ShrtLy;User ID=sa;Password=MyCoolP@ss2;Trust Server Certificate=true;"));
builder.Services.AddTransient<IShorteningService, ShorteningService>();
builder.Services.AddTransient<ILinksRepository, LinksRepository>();
builder.Services.AddTransient<ShrtLyContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
