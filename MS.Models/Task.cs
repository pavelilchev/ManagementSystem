namespace MS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using TaskType = Enums.TaskType;

    public class Task
    {
        private ICollection<Comment> comments;

        public Task()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Task {0} should be between {2} and {1}")]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(2500, MinimumLength = 5, ErrorMessage = "Task {0} should be between {2} and {1}")]
        public string Description { get; set; }

        public Status Status { get; set; }

        public TaskType Type { get; set; }

        public string CreatedFromId { get; set; }

        public virtual User CreatedFrom { get; set; }

        public string AssignedToId { get; set; }

        public virtual User AssignedTo { get; set; }

        public DateTime? NextActionDate { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
