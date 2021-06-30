using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication2.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        private ApplicationDbContext Admin = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Admin));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Admin));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ezzat_1969";
                user.Email = "ahmed.ezzata240@gmail.com";
                var Check = userManager.Create(user, "Yasser@123");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
