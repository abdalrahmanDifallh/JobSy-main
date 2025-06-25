using JopSy.Data;
using JopSy.Interface;
using JopSy.Models; // أضفت هذا عشان كلاس User
using JopSy.Repository;
using Microsoft.AspNetCore.Identity; // أضفت هذا لـ Identity
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// تسجيل الـ Repositories
builder.Services.AddScoped<IJobRepository, JobRepository>();
//builder.Services.AddScoped<IAddressRepository, AddressRepository>(); // لو هتستخدمه، شيل التعليق

// إعداد DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// إضافة خدمات Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // إعدادات اختيارية لكلمات المرور
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // استبدلت MapStaticAssets بـ UseStaticFiles
app.UseRouting();

app.UseAuthentication(); // أضفت هذا (مهم جدًا لـ Identity)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();