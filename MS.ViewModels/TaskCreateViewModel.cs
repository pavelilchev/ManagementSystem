namespace MS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class TaskCreateViewModel
    {
        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Task {0} should be between {2} and {1}")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(2500, MinimumLength = 5, ErrorMessage = "Task {0} should be between {2} and {1}")]
        public string Description { get; set; }

        public Status Status { get; set; }

        public TaskType Type { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedToId { get; set; }

        public IEnumerable<SelectListItem> AssignedToUsernames { get; set; }

        public DateTime? NextActionDate { get; set; }
    }
}
