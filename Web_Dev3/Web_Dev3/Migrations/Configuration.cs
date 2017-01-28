namespace Web_Dev3.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 
            }
            if (!UserManager.Users.Any(u => u.UserName == "admin@todo.de"))
            {
                var user = new ApplicationUser();
                user.UserName = "admin@todo.de";
                user.Email = "admin@todo.de";

                string userPWD = "Admin123%";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }


            // creating Creating Employee role   
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

            }



            /*        if (!context.Roles.Any(r => r.Name == "Admin"))
                    {
                        var store = new RoleStore<IdentityRole>(context);
                        var manager = new RoleManager<IdentityRole>(store);
                        var role = new IdentityRole { Name = "Admin" };

                        manager.Create(role);
                        var userStore = new UserStore<ApplicationUser, ApplicationRole, int, ApplicationsUserLogin, ApplicationsUserRole, ApplicationUserClaim>(context);
                        var userManager = new ApplicationUserManager();
                    }

        //            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));

                    if (!roleManager.Roles.Any())
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = ApplicationRole.AdminRoleName });
                        await roleManager.CreateAsync(new ApplicationRole { Name = ApplicationRole.AffiliateRoleName });
                    }

                    if (!userManager.Users.Any(u => u.UserName == "shimmy"))
                    {
                        var user = new ApplicationUser
                        {
                            UserName = "shimmy",
                            Email = "shimmy@gmail.com",
                            EmailConfirmed = true,
                            PhoneNumber = "0123456789",
                            PhoneNumberConfirmed = true
                        };

                        await userManager.CreateAsync(user, "****");
                        await userManager.AddToRoleAsync(user.Id, ApplicationRole.AdminRoleName);
                        //  This method will be called after migrating to the latest version.

                        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                        //  to avoid creating duplicate seed data. E.g.
                        //
                        //    context.People.AddOrUpdate(
                        //      p => p.FullName,
                        //      new Person { FullName = "Andrew Peters" },
                        //      new Person { FullName = "Brice Lambson" },
                        //      new Person { FullName = "Rowan Miller" }
                        //    );
                        //*/
        }
    }
}
