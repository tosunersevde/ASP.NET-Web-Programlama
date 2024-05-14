using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moqups.Controllers;
using Moqups.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moqups.Infrastructure.Extensions;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.ConfigureDbContext(builder.Configuration);

// Services
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
//});

//builder.Services.AddScoped<RepositoryContext>(serviceProvider =>
//{
//    var options = serviceProvider.GetRequiredService<DbContextOptions<RepositoryContext>>();
//    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
//    return new RepositoryContext(options);
//});



builder.Services.ConfigureIdentity();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false; //Giris onayi ister
//    options.User.RequireUniqueEmail = true; //Benzersiz email ister
//    options.Password.RequireUppercase = false; //Sifre icin buyuk harf gerekli degil
//    options.Password.RequireLowercase = false; //Sifre icin kucuk harf gerekli degil
//    options.Password.RequireDigit = false; //Sifre icin rakam gerekli degil
//    options.Password.RequiredLength = 6; //Sifre uzunlugu 6
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();


builder.Services.ConfigureSession();

//builder.Services.AddDistributedMemoryCache(); //Sunucu tarafinda bir onbellek saglar.

//builder.Services.AddSession(options =>
//{
//    options.Cookie.Name = "Moqups.Session";
//    options.IdleTimeout = TimeSpan.FromMinutes(10); //10 dk sonra oturum duser.

//});


builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();

builder.Services.ConfigureRouting();

builder.Services.ConfigureApplicationCookie();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = new PathString("/Account/Login");
//    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
//    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
//});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.Cookie.HttpOnly = true;
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//        options.LoginPath = "/Account/Login"; // Set your login path
//        options.AccessDeniedPath = "/Account/AccessDenied"; // Set your access denied path
//        options.SlidingExpiration = true;
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//});

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}


app.UseStaticFiles();

app.UseSession();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        //name: "default",
        //pattern: "{controller=Home}/{action=Index}/{id?}" seklinde de yazilabilir.
        "default",
        "{controller=FirstPage}/{action=Index}/{id?}");

    endpoints.MapRazorPages();

    endpoints.MapControllers(); //API'lar icin yapilan tanim

});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=FirstPage}/{action=Index}");

//app.ConfigureDefaultAdminUser();

// Create roles and admin user
//CreateRolesAndAdminUser(app).Wait();

app.ConfigureAndCheckMigration();

app.ConfigureLocalization();

app.ConfigureDefaultAdminUser();


app.Run();
//async Task CreateRolesAndAdminUser(WebApplication app)
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//        string[] roleNames = { "Admin", "User" };
//        foreach (var roleName in roleNames)
//        {
//            var roleExist = await roleManager.RoleExistsAsync(roleName);
//            if (!roleExist)
//            {
//                await roleManager.CreateAsync(new IdentityRole(roleName));
//            }
//        }

//        var adminUser = new IdentityUser
//        {
//            UserName = "admin",
//            Email = "admin@gmail.com",
//        };

//        string adminPassword = "admin123456";

//        var user = await userManager.FindByEmailAsync(adminUser.Email);
//        if (user == null)
//        {
//            var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
//            if (createAdminUser.Succeeded)
//            {
//                await userManager.AddToRoleAsync(adminUser, "Admin");
//            }
//        }
//    }
//}





//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Moqups.Controllers;
//using Moqups.Models;

//var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
//});
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}

//app.UseStaticFiles();

//app.UseRouting();

////Login-yetkilendirme
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=FirstPage}/{action=Index}");

//app.Run();
