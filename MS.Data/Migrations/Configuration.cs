namespace MS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<ManagementSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ManagementSystemDbContext context)
        {
            this.SeedUsers(context);
            this.SeedTasks(context);
            this.SeedComments(context);
        }

        private void SeedUsers(ManagementSystemDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "admin", Email = "admin@gmail.com" };

                userManager.Create(userToInsert, "123456");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "user1"))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "user1", Email = "user1@gmail.com"};

                userManager.Create(userToInsert, "123456");
            }

            if (!context.Users.Any(u => u.UserName == "user2"))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "user2", Email = "use2r@gmail.com" };

                userManager.Create(userToInsert, "123456");
            }
        }
        private void SeedTasks(ManagementSystemDbContext context)
        {
            var task1 = new Task()
            {
                Id = 1,
                CreatedFromId = context.Users.FirstOrDefault(u => u.UserName == "admin").Id,
                CreatedFrom = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                CreatedDate = DateTime.Now,
                Status = Status.Active,
                Type = TaskType.Normal,
                Title = "First task",
                Description = "Create a Task model"
            };

            var task2 = new Task()
            {
                Id = 2,
                CreatedFromId = context.Users.FirstOrDefault(u => u.UserName == "admin").Id,
                CreatedFrom = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                CreatedDate = DateTime.Now,
                Status = Status.Active,
                Type = TaskType.Major,
                DueDate = DateTime.Now.AddDays(3),
                Title = "Second task",
                Description = "Create a Comments model"
            };

            var task3 = new Task()
            {
                Id = 3,
                CreatedFromId = context.Users.FirstOrDefault(u => u.UserName == "admin").Id,
                CreatedFrom = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                CreatedDate = DateTime.Now,
                Status = Status.Active,
                Type = TaskType.Critical,
                DueDate = DateTime.Now.AddDays(1),
                Title = "Third task",
                Description = "Do all the rest",
                AssignedToId = context.Users.FirstOrDefault(u => u.UserName == "user1").Id,
                AssignedTo = context.Users.FirstOrDefault(u => u.UserName == "user1")
            };

            context.Tasks.AddOrUpdate(t => t.Id, task1, task2, task3);
            context.SaveChanges();
        }
        private void SeedComments(ManagementSystemDbContext context)
        {
            var taskId = context.Tasks.Find(1).Id;
            var userId = context.Users.FirstOrDefault(u => u.UserName == "user1").Id;

            var comment1 = new Comment()
            {
                Id = 1,
                TaskId = taskId,
                Type = CommentType.Info,
                DateAdded = DateTime.Now,
                UserId = userId,
                Content = "Created Task Model"
            };

            var comment2 = new Comment()
            {
                Id = 2,
                TaskId = taskId,
                Type = CommentType.Info,
                DateAdded = DateTime.Now,
                UserId = userId,
                Content = "Seeded some tasks"
            };

            context.Comments.AddOrUpdate(c => c.Id, comment1, comment2);
            context.SaveChanges();
        }
    }
}
