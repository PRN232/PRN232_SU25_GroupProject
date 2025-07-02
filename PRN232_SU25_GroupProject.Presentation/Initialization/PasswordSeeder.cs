using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class PasswordSeeder
    {
        public static async Task SeedPasswordsAsync(UserManager<User> userManager)
        {
            var defaultPassword = "String_1";
            var usernames = new[] { "admin", "mgr001", "nurse001", "nurse002", "parent001", "parent002", "parent003", "parent004", "parent005", "parent006", "parent007" };
            foreach (var username in usernames)
            {
                var user = await userManager.FindByNameAsync(username);
                if (user != null && string.IsNullOrEmpty(user.PasswordHash))
                {
                    var result = await userManager.AddPasswordAsync(user, defaultPassword);
                    if (!result.Succeeded)
                        Console.WriteLine($"⚠️ Failed to set password for {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}