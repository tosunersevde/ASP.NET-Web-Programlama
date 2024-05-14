using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Moqups.Areas.Admin.Data.Configurations
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole() { Name = "User", NormalizedName = "USER" },
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" }
            );
        }
    }
}
