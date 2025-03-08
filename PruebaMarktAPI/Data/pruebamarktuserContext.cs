using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PruebaMarktAPI.Data
{
    public class pruebamarktuserContext : IdentityDbContext
    {

        public pruebamarktuserContext(DbContextOptions<pruebamarktuserContext> options)
        :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>() {
                new IdentityRole
                {
                    Name="Basic",
                    NormalizedName="BASIC"
                },
                new IdentityRole
                {
                    Name="Premium",
                    NormalizedName="PREMIUM"
                },
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            List<IdentityUser> users = new List<IdentityUser>()
            {
                new IdentityUser
                {
                    UserName="basic@prueba.com",
                    NormalizedUserName="BASIC@PRUEBA.COM",
                    Email="basic@prueba.com",
                    NormalizedEmail="BASIC@PRUEBA.COM",
                    EmailConfirmed=true
                },
                new IdentityUser
                {
                    UserName="premium@prueba.com",
                    NormalizedUserName="PREMIUM@PRUEBA.COM",
                    Email="premium@prueba.com",
                    NormalizedEmail="PREMIUM@PRUEBA.COM",
                    EmailConfirmed=true
                },
                new IdentityUser
                {
                    UserName="admin@prueba.com",
                    NormalizedUserName="ADMIN@PRUEBA.COM",
                    Email="admin@prueba.com",
                    NormalizedEmail="ADMIN@PRUEBA.COM",
                    EmailConfirmed=true
                }
            };
            builder.Entity<IdentityUser>().HasData(users);
            var passwordHasher = new PasswordHasher<IdentityUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Asdf1234!");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Asdf1234!");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "Asdf1234!");
            List<IdentityUserRole<string>> roluser = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
            {
                UserId=users[0].Id,
                RoleId=roles[0].Id
            },
            new IdentityUserRole<string>()
            {
                UserId=users[1].Id,
                RoleId=roles[1].Id
            },
            new IdentityUserRole<string>()
            {
                UserId=users[2].Id,
                RoleId=roles[2].Id
            }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(roluser);
        }
    }
}
