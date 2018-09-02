using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

public static class ModelBuilderExtensions
{
    public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.Relational().TableName = entity.DisplayName();
        }
    }
}

namespace NetCoreBase.Models
{
    public class NetCoreDbContext : IdentityDbContext<NetCoreUser>
    {

        public NetCoreDbContext(DbContextOptions<NetCoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Make it so EF Core does not pluralize Table Names
            // I prefer singular table names (Can remove if you want plural table names).
            // https://stackoverflow.com/questions/37493095/entity-framework-core-rc2-table-name-pluralization
            builder.RemovePluralizingTableNameConvention();
            // Make Identity tables signular as well.
            builder.Entity<NetCoreUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            // Run Seed defined in NetCoreDBSeed.cs as a ModelBuilder extension.
            builder.Seed();
        }
    }
}