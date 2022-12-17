﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestAPI_Kreatx.Models
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required")]
        [StringLength(40)]
        [DisplayName("Project Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        [DisplayName("User Id")]
        public APIUser User { get; set; }

    }
}
