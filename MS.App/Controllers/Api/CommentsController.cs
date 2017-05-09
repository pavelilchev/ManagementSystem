namespace MS.App.Controllers.Api
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Data.UnitOfWork;
    using DTOModels;

    public class CommentsController : ApiController
    {
        public IHttpActionResult GetComments(int id)
        {
            var data = new ManagementSystemData();
            var task = data.Tasks
                .All()
                .Include(t => t.Comments.Select(c => c.User))
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedFrom)
                .FirstOrDefault(t => t.Id == id);

            var taskDto = Mapper.Map<TaskDTO>(task);

            return this.Ok(taskDto);
        }
    }
}
