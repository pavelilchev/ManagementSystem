namespace MS.App.Controllers.Api
{
    using System.Web.Http;
    using DTOModels;
    using Microsoft.AspNet.Identity;
    using MS.Models;
    using Services;
    using ViewModels;

    public class CommentsController : ApiController
    {
        public CommentsController()
        {
            this.CommentService = new CommentService();
        }

        private CommentService CommentService { get; set; }

        public IHttpActionResult GetComments(int id)
        {
            var taskDto = this.CommentService.GetComments(id);

            return this.Ok(taskDto);
        }

        [Route("api/comment/{id:int}")]
        public IHttpActionResult GetCommentById(int id)
        {
            var comment = this.CommentService.GetCommentById(id);

            return this.Ok(comment);
        }

        [Route("api/remove/{id:int}")]
        [HttpDelete]
        public IHttpActionResult RemoveComment(int id)
        {
            Comment comment = this.CommentService.RemoveComment(id);
            if (comment == null)
            {
                return this.NotFound();
            }

            return this.Ok(comment);
        }

        [Route("api/addcomment")]
        [HttpPost]
        public IHttpActionResult AddComment([FromBody] CommentCreateViewModel model)
        {
            model.UserId = this.User.Identity.GetUserId();
            CommentCreateViewModel comment = this.CommentService.AddComment(model);

            return this.Ok(comment);
        }

        [Route("api/edit")]
        [HttpPost]
        public IHttpActionResult AddComment([FromBody] CommentEditDTO model)
        {
            this.CommentService.Edit(model);

            return this.Ok();
        }
    }
}
