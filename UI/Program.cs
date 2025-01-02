using Microsoft.AspNetCore.Authentication.Cookies;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
