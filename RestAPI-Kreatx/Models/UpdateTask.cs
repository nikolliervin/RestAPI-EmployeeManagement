using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestAPI_Kreatx.Models
{
    public class UpdateTask
    {
        [Required(ErrorMessage = "Task name is required")]
        [StringLength(40)]
        [DisplayName("Old Task Name")]
        public string TaskName { get; set; }


        [Required(ErrorMessage = "Task name is required")]
        [DisplayName("New Task Name")]
        [StringLength(40)]
        public string NewTaskName { get; set; }

        [Required(ErrorMessage = "Task description is required")]
        [DisplayName("New Task Description")]
        [StringLength(120)]
        public string NewTaskDesc { get; set; }

        [Required(ErrorMessage = "Task status is required")]
        [DisplayName("New Task Status")]
        public bool IsFinished { get; set; }


    }
}
