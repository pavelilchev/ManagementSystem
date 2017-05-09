namespace MS.Models
{
    using System;
    using Enums;

    public class Comment
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public virtual Task Task { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public CommentType Type { get; set; }

        public DateTime? RemainderDate { get; set; }
    }
}
