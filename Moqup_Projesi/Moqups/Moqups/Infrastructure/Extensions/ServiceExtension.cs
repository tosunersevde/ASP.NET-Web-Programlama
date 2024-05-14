using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using Moqups.Models;

namespace Moqups.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration) //IServiceCollection-bir inteface yapisi genisletilir.
        {
            services.AddDbContext<RepositoryContext>(options =>
            {
                //options.UseSqlite(configuration.GetConnectionString("sqlconnection"),
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                b => b.MigrationsAssembly("Moqups")); //Hangi proje uzerinde migration islemleri yapilacak?

                options.EnableSensitiveDataLogging(true); //Hassas bilgiler dusen log'lara yansitilabilir, giris-cikis gosterme durumlari izlenebilir.
            });

        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options => 
            {
                options.SignIn.RequireConfirmedAccount = false; //Giris onayi ister
                options.User.RequireUniqueEmail = true; //Benzersiz email ister
                options.Password.RequireUppercase = false; //Sifre icin buyuk harf gerekli degil
                options.Password.RequireLowercase = false; //Sifre icin kucuk harf gerekli degil
                options.Password.RequireDigit = false; //Sifre icin rakam gerekli degil
                options.Password.RequiredLength = 6; //Sifre uzunlugu 6
            })
            .AddEntityFrameworkStores<RepositoryContext>();
        }

        public static void ConfigureSession(this IServiceCollection services) //Ikinci parametre varsa Program.cs'de belirtilir.
        {
            services.AddDistributedMemoryCache(); //Sunucu tarafinda bir onbellek saglar.
            services.AddSession(options =>
            {
                options.Cookie.Name = "Moqups.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10); //10 dk sonra oturum duser.

            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //session okunmasini saglayan ifade
            //services.AddScoped<Cart>(c => SessionCart.GetCart(c));
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthService, AuthManager>();
        }

        public static void ConfigureApplicationCookie(this IServiceCollection services) //Uygulama cerezleri genisletilecek
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
        }

        public static void ConfigureRouting(this IServiceCollection services) //Yonlendirme yapilirken kucuk harf kullanimi icin
        {
            services.AddRouting(options =>
            { 
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false; //Sonuna '/' eklenmez.
            });
        }

    }
}
