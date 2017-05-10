namespace MS.App
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using DTOModels;
    using MS.Models;
    using ViewModels;

    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Task, TaskViewModel>()
                    .ForMember(tvm => tvm.AssignedTo, src => src.MapFrom(t => t.AssignedTo != null ? t.AssignedTo.UserName : "no one"))
                    .ForMember(tvm => tvm.DueDate, src => src.MapFrom(t => t.DueDate != null ? t.DueDate.Value.ToString("dd/MM/yyyy") : "Whenever"))
                    .ForMember(tvm => tvm.NextActionDate, src => src.MapFrom(t => t.NextActionDate != null ? t.NextActionDate.Value.ToString("dd/MM/yyyy") : "-"));

                cfg.CreateMap<Comment, CommentDTO>()
                    .ForMember(tdto => tdto.UserName, src => src.MapFrom(t => t.User.UserName))
                    .ForMember(tdto => tdto.CommentType, src => src.MapFrom(t => t.Type.ToString()));

                cfg.CreateMap<Task, TaskDTO>()
                 .ForMember(tdto => tdto.AssignedToUsername, src => src.MapFrom(t => t.AssignedTo.UserName))
                 .ForMember(tdto => tdto.CreatedFromUsername, src => src.MapFrom(t => t.CreatedFrom.UserName))
                 .ForMember(tdto => tdto.Comments, src => src.MapFrom(t => Mapper.Map<ICollection<CommentDTO>>(t.Comments)));

                cfg.CreateMap<TaskCreateViewModel, Task>()
                   .ForMember(tdto => tdto.CreatedDate, src => src.MapFrom(t => DateTime.Now));

                cfg.CreateMap<CommentCreateViewModel, Comment>()
                  .ForMember(tdto => tdto.DateAdded, src => src.MapFrom(t => DateTime.Now));
            });
        }
    }
}