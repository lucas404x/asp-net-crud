using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models 
{
    public class TaskModel 
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get ; set; }
        public bool Done { get; set; } = false;
        [DataType(DataType.DateTime)]
        public DateTime LastTimeUpdated { get; set; } = DateTime.Now;
    }
}