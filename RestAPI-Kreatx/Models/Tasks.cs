﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestAPI_Kreatx.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        [DisplayName("Task Name")]
        [StringLength(40)]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Project description is required")]
        [DisplayName("Task Description")]
        [StringLength(120)]
        public string TaskDesc { get; set; }

        [Required(ErrorMessage = "Task status is required")]
        [DisplayName("Task Status")]
        public string TaskStatus { get; set; }


        [Required(ErrorMessage = "Project Id cannot be null")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "User Id cannot be null")]
        public int UserId { get; set; }
    }
}
