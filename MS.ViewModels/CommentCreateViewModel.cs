namespace MS.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Models.Enums;

    public class CommentCreateViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Comment should be at least {1} characters.")]
        public string Content { get; set; }

        public int TaskId { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public CommentType Type { get; set; }

        public DateTime? RemainderDate { get; set; }
    }
}
