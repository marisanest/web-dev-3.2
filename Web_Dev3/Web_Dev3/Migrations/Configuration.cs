namespace Web_Dev3.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Web_Dev3.Controllers;

    internal sealed class Configuration : DbMigrationsConfiguration<Web_Dev3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Web_Dev3.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User   
            if (!roleManager.RoleExists(TodoController.ADMIN))
            {

                // first we create Admin rool  
                var role = new IdentityRole();
                role.Name = TodoController.ADMIN;
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 
            }
            if (!UserManager.Users.Any(u => u.UserName == TodoController.ADMIN_EMAIL))
            {
                var user = new ApplicationUser();
                user.UserName = TodoController.ADMIN_EMAIL;
                user.Email = TodoController.ADMIN_EMAIL;

                string userPWD = "Admin123%";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, TodoController.ADMIN);

                }
            }
        }
    }
}
