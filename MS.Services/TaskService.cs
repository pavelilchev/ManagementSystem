namespace MS.Services
{
    using Data.UnitOfWork;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class TaskService
    {
        public TaskService(IManagementSystemData data)
        {
            this.Data = data;
        }

        private IManagementSystemData Data { get; set; }

        public ICollection<Task> GetAllTasks()
        {
            return this.Data.Tasks.All().Include(t => t.AssignedTo).ToList();
        }

        public Task GetTaskById(int id)
        {
            return this.Data.Tasks.Find(id);
        }

        public void AddTask(Task task)
        {
            this.Data.Tasks.Add(task);
            this.Data.SaveChanges();
        }
    }
}
