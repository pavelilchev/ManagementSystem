namespace MS.ViewModels
{
    using System;
    using Models.Enums;

    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string DueDate { get; set; }

        public string AssignedTo { get; set; }

        public TaskType Type { get; set; }
    }
}
