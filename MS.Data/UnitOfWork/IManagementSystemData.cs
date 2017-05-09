namespace MS.Data.UnitOfWork
{
    using Models;
    using Repositories;

    public interface IManagementSystemData
    {
        IRepository<User> Users { get; }

        IRepository<Task> Tasks { get; }

        IRepository<Comment> Comments { get; }

        int SaveChanges();
    }
}
