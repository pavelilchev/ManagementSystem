namespace MS.App.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using MS.Models;
    using Services;
    using ViewModels;

    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskService taskService;
        private readonly UserService userService;

        public TasksController(TaskService taskService, UserService userService)
        {
            this.taskService = taskService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var tasks = this.taskService.GetAllTasks();
            var tasksViewmodels = Mapper.Map<IEnumerable<TaskViewModel>>(tasks);

            return this.View(tasksViewmodels);
        }

        public ActionResult Create()
        {
            var users = this.userService.GetUsersUsernames();
            var model = new TaskCreateViewModel()
            {
                AssignedToUsernames = users
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(TaskCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var task = Mapper.Map<Task>(model);
                string currentUserId = this.User.Identity.GetUserId();
                task.CreatedFromId = currentUserId;
                this.taskService.AddTask(task);

                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }
    }
}
