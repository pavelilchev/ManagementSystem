namespace MS.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class ManagementSystemDbContext : IdentityDbContext<User>
    {
        public ManagementSystemDbContext()
            : base("MSConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ManagementSystemDbContext, Configuration>());
        }

        public static ManagementSystemDbContext Create()
        {
            return new ManagementSystemDbContext();
        }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }
    }
}
