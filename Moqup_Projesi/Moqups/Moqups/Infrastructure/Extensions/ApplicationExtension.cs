using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Moqups.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app) //WebApplication da genisletilebilir.
        {
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>(); //Ihtiyac duyulan servisin uygulama uzerinden temin edilmesi saglanir.

            if (context.Database.GetPendingMigrations().Any()) //Herhangi bir bekleyen migration ifadesi varsa
            { 
                context.Database.Migrate(); //Migrate islemi otomatik olarak yapilir.
            }

        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            //Kultur bilgilerini set eden middleware - klasik bir middleware options ifadelerini destekler.
            app.UseRequestLocalization(options =>
            {
                //("tr-TR", "") - birden fazla ulke kodu girilebilir.
                options.AddSupportedCultures("tr-TR")
                .AddSupportedUICultures("tr-TR") //arayuz
                .SetDefaultCulture("tr-TR"); //default
            });
        }

        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app) //Controller olmadigindan task kullanilmaz.
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin+123456";

            //UserManager - tanim yapildi, ilgili servis IoC uzerinden alinacak.
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            //RoleManager
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateAsyncScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser); 
            if (user is null) //Boyle bir kullanici yoksa
            {
                user = new IdentityUser() //parametre olarak adminUser da verilebilirdi.
                {
                    Email = "admin@gmail.com",
                    //PhoneNumber = "5051112233",
                    UserName = adminUser,
                };

                var result = await userManager.CreateAsync(user, adminPassword); //veritabanina kaydeder.

                if (!result.Succeeded)
                    throw new Exception("Amin user could not created!");

                var roleResult = await userManager.AddToRolesAsync(user,
                    roleManager
                        .Roles
                        .Select (r => r.Name)
                        .ToList() //rollerin listesini alir.

                    //new List<string>() //Bu sekilde tanimlanirsa congig dosyasinda degisiklik elle duzeltilmelidir.
                    //{
                    //    "Admin",
                    //    "Editor",
                    //    "User"
                    //}
                );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role defination for admin!");
            }
        }
    }
    
}
