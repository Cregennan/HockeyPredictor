using MegaHockey;
using MegaHockey.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



var s = ClassifierSingletone.Instance();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HistoryRecordContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("HistoryRecordContext") ?? throw new InvalidOperationException("Connection string 'HistoryRecordContext' not found.")));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddControllersWithViews();



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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Search}/{id?}"
);
//app.MapControllerRoute(
//    name: "searchresult",
//    pattern: "Home/SearchResult/{first}/{second}"
//    );
app.MapRazorPages();

app.Run();


