using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ProjectMVC.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Project name is too long or too short")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Project description is too long or too short")]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Project deadline")]
        public DateTime deadline { get; set; }

        public virtual ICollection<PlannedTask> plannedTasks { get; set; }
    }
}
