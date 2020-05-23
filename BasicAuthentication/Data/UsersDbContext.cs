using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BasicAuthentication.Data
{
    public class UsersDbContext : IdentityDbContext<IdentityUser>
    {
        static UsersDbContext()
        {
            Database.SetInitializer(new Initializer());
        }

        private class Initializer : CreateDatabaseIfNotExists<UsersDbContext>
        {
            protected override void Seed(UsersDbContext context)
            {
                var userRole = context.Roles.Add(new IdentityRole("User"));
                var adminRole = context.Roles.Add(new IdentityRole("Admin"));

                var user = new IdentityUser("SampleUser");
                user.Roles.Add(new IdentityUserRole { Role = userRole });
                user.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = "hasRegistered",
                    ClaimValue = "true"
                });

                user.PasswordHash = new PasswordHasher().HashPassword("secret");
                context.Users.Add(user);

                var admin = new IdentityUser("SampleAdmin");
                admin.Roles.Add(new IdentityUserRole { Role = userRole });
                admin.Roles.Add(new IdentityUserRole { Role = adminRole });
                admin.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = "hasRegistered",
                    ClaimValue = "true"
                });

                admin.PasswordHash = new PasswordHasher().HashPassword("secret");
                context.Users.Add(admin);

                context.SaveChanges();
                base.Seed(context);
            }
        }
    }
}