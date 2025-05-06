using E_Ticaret.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using E_Ticaret.Models.Entities;

using E_Ticaret.OrtakKullanim;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// EMAÝL KISMI
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CompareService>();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".E_Ticaret.Session"; // Session cookie adý
    options.Cookie.HttpOnly = true; // HttpOnly özelliði
    options.Cookie.IsEssential = true; // Gerekli olduðunu belirtir
    options.IdleTimeout = TimeSpan.FromDays(1); // Session süresi
    options.IOTimeout = TimeSpan.FromMinutes(30); // IO timeout süresi
}); // Session servisi eklenir  sessionlar sadece string olarak kullanýlýr bunun icinde json kullanýcaz
 
builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(i =>
{
    // kullanýcý  url kýsmýnda KullaniciGiris http://localhost:5175/KullaniciGiris/Index yapmöaya calýsýrsa engelliyor onu  KullaniciGiris/GirisYap sayfasýna atýyor
    i.LoginPath = "/KullaniciGiris/GirisYap"; // Giriþ yap sayfasý /Account/SingÝn normalde 
    i.AccessDeniedPath = "/AccessDenied";  // Eriþim engellendi sayfasý  kullanýcý yetkisi yoksa yönlendirilir
    i.Cookie.Name = "KullaniciGiris"; // Cookie adý Account yerine 
    i.Cookie.MaxAge = TimeSpan.FromDays(30); // Cookie'nin geçerlilik süresi
    i.Cookie.IsEssential = true; // Cookie'nin gerekli olduðunu belirtir

});
builder.Services.AddAuthorization( x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role,"Admin")); // Admin rolü için yetkilendirme politikasý
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User","Customer","Admin")); // User rolü için yetkilendirme politikasý
}); // Yetkilendirme servisi eklenir

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); // Session middleware'i eklenir
app.UseRouting();

app.UseAuthentication(); // Kimlik doðrulama middleware'i eklenir önce gelir  önce oturum acma
app.UseAuthorization(); //  sonra Yetkilendirme middleware'i eklenir

app.MapStaticAssets();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
