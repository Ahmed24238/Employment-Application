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
        private ApplicationDbContext ARDB = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ARDB));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ARDB));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("admin"))
            {
                role.Name = "admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "ALIIII69";
                user.Email = "Nasser22@gmail.com";
                var Check = userManager.Create(user, "Nasser88856**");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "admin");
                }
            }
        }
    }
}