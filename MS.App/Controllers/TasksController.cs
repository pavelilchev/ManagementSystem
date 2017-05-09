namespace MS.App.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Services;
    using ViewModels;

    [Authorize]
    public class TasksController : Controller
    {
        private TaskService taskService;
        private UserService userService;

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

            return this.View(model);
        }
    }
}
