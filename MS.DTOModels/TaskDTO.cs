namespace MS.DTOModels
{
    using System;
    using System.Collections.Generic;
    using Models.Enums;

    public class TaskDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public TaskType Type { get; set; }

        public string CreatedFromId { get; set; }

        public string CreatedFromUsername { get; set; }

        public string AssignedToId { get; set; }

        public string AssignedToUsername { get; set; }

        public DateTime? NextActionDate { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}
