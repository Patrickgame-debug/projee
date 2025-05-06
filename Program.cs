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

// EMA�L KISMI
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CompareService>();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".E_Ticaret.Session"; // Session cookie ad�
    options.Cookie.HttpOnly = true; // HttpOnly �zelli�i
    options.Cookie.IsEssential = true; // Gerekli oldu�unu belirtir
    options.IdleTimeout = TimeSpan.FromDays(1); // Session s�resi
    options.IOTimeout = TimeSpan.FromMinutes(30); // IO timeout s�resi
}); // Session servisi eklenir  sessionlar sadece string olarak kullan�l�r bunun icinde json kullan�caz
 
builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(i =>
{
    // kullan�c�  url k�sm�nda KullaniciGiris http://localhost:5175/KullaniciGiris/Index yapm�aya cal�s�rsa engelliyor onu  KullaniciGiris/GirisYap sayfas�na at�yor
    i.LoginPath = "/KullaniciGiris/GirisYap"; // Giri� yap sayfas� /Account/Sing�n normalde 
    i.AccessDeniedPath = "/AccessDenied";  // Eri�im engellendi sayfas�  kullan�c� yetkisi yoksa y�nlendirilir
    i.Cookie.Name = "KullaniciGiris"; // Cookie ad� Account yerine 
    i.Cookie.MaxAge = TimeSpan.FromDays(30); // Cookie'nin ge�erlilik s�resi
    i.Cookie.IsEssential = true; // Cookie'nin gerekli oldu�unu belirtir

});
builder.Services.AddAuthorization( x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role,"Admin")); // Admin rol� i�in yetkilendirme politikas�
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User","Customer","Admin")); // User rol� i�in yetkilendirme politikas�
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

app.UseAuthentication(); // Kimlik do�rulama middleware'i eklenir �nce gelir  �nce oturum acma
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
