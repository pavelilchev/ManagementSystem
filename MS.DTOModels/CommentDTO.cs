namespace MS.DTOModels
{
    using System;
    using Models.Enums;

    public class CommentDTO
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public CommentType Type { get; set; }

        public DateTime? RemainderDate { get; set; }
    }
}
