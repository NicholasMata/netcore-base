using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NetCoreBase.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole("User"),
                new IdentityRole("Admin")
            );
        }
    }
}