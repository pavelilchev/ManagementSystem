namespace MS.Services
{
    using System;
    using Data.UnitOfWork;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using AutoMapper;
    using DTOModels;
    using Models;
    using Models.Enums;
    using ViewModels;

    public class CommentService
    {
        public CommentService()
        {
            this.Data = new ManagementSystemData();
        }

        private IManagementSystemData Data { get; set; }

        public TaskDTO GetComments(int id)
        {
            var task = this.Data.Tasks
               .All()
               .Include(t => t.Comments.Select(c => c.User))
               .Include(t => t.AssignedTo)
               .Include(t => t.CreatedFrom)
               .FirstOrDefault(t => t.Id == id);

            var taskDto = Mapper.Map<TaskDTO>(task);

            return taskDto;
        }

        public Comment RemoveComment(int id)
        {
            var result = this.Data.Comments.Remove(id);
            this.Data.SaveChanges();

            return result;
        }

        public CommentCreateViewModel AddComment(CommentCreateViewModel model)
        {
            Comment comment = Mapper.Map<Comment>(model);
            model.UserUserName = this.Data.Users.Find(model.UserId).UserName;

            if (comment.RemainderDate != null)
            {
                var task = this.Data.Tasks.Find(model.TaskId);
                task.NextActionDate = model.RemainderDate;
            }

            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();

            return model;
        }

        public CommentEditDTO GetCommentById(int id)
        {
            var comment = this.Data.Comments.Find(id);

            CommentEditDTO dto = new CommentEditDTO()
            {
                Id = id,
                Content = comment.Content,
                Types = Enum.GetNames(typeof(CommentType)),
                Date = comment.DateAdded.ToString("dd/MM/yyyy")
            };

            return dto;
        }

        public void Edit(CommentEditDTO model)
        {
            var comment = this.Data.Comments.Find(model.Id);

            comment.Content = model.Content;
            this.Data.SaveChanges();
        }
    }
}
