using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KingTransports.Data
{
    public class SampleRole
    {
        public string Name { get; set; }
    }

    public class SampleUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }

    public static class DbInitializer
    {
        private readonly static List<SampleRole> sampleRoles = new List<SampleRole>()
        {
            new SampleRole()
            {
                Name = "admin"
            },
            new SampleRole()
            {
                Name = "ticket-seller"
            },
            new SampleRole()
            {
                Name = "conductor"
            }
        };

        private readonly static List<SampleUser> sampleUsers = new List<SampleUser>()
        {
            new SampleUser()
            {
                Email = "admin@email.com",
                Password = "Administrator!1",
                Roles = new string[] {"admin"}
            },
            new SampleUser()
            {
                Email = "conductor@email.com",
                Password = "Conductor!1",
                Roles = new string[] {"conductor"}
            },
             new SampleUser()
            {
                Email = "ticketseller@email.com",
                Password = "Ticketseller!1",
                Roles = new string[] { "ticket-seller" }
            },
        };

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.Migrate();
            }

            using (UserManager<IdentityUser> _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
            using (RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            {

                foreach(var role in sampleRoles)
                {
                    var roleResult = await _roleManager.FindByNameAsync(role.Name);

                    if (roleResult == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role.Name));
                    }
                }

                foreach (var user in sampleUsers)
                {
                    var userResult = await _userManager.FindByEmailAsync(user.Email);

                    if (userResult == null)
                    {
                        var iUser = new IdentityUser
                        {
                            UserName = user.Email,
                            NormalizedUserName = user.Email,
                            Email = user.Email,
                            NormalizedEmail = user.Email,
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                        };

                        var result = await _userManager.CreateAsync(iUser, user.Password);

                        foreach (var role in user.Roles)
                        {
                            var result1 = await _userManager.AddToRoleAsync(iUser, role);
                        }
                    }
                }
            }
        }
    }
}
