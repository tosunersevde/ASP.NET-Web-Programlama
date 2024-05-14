using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Moqups.Models;
using Repositories.Config;
using System.Reflection;

namespace Repositories;
//public class RepositoryContext : DbContext //Veritabanı
//public class RepositoryContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
public class RepositoryContext : IdentityDbContext<IdentityUser>
{
    //Default, parametresiz olan RepositoryContext yapısı bozulmuş olur.

    public DbSet<Message> Messages { get; set; }
    public DbSet<CallbackMessage> CallbackMessages { get; set; }
    public DbSet<TakipNo> TakipNumaralari { get; set; }

    //private readonly IApplicationDbContext _dbContext;

    //private readonly IServiceProvider _serviceProvider;
    //public RepositoryContext(DbContextOptions<RepositoryContext> options, IServiceProvider serviceProvider)
    //public RepositoryContext(DbContextOptions<RepositoryContext> options, IApplicationDbContext dbContext)
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
    : base(options) //base'i dbcontext'tir.
    { 
        //_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        //_dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        //_serviceProvider = serviceProvider;
        //_dbContext = dbContext;

        //public IApplicationDbContext GetDbContext()
        //{
        //    return _dbContext;
        //}
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //Sınıfla iliski kurma-cekirdek datalari context uzerinde tanımlama
    {
        modelBuilder.Entity<TakipNo>()
            .HasIndex(u => u.TakipNumarası)
            .IsUnique();

        base.OnModelCreating(modelBuilder);

        //Configuration islemi ile config dosyalrinin dogrudan tanimlanabilmesi
        //modelBuilder.ApplyConfiguration(new ProductConfig());
        //modelBuilder.ApplyConfiguration(new CategoryConfig());

        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //Yeni bir tip kaydı yapildiginda otomatik tanimlanir.
    }
}