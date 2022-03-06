using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ProjectMVC.Validators;


namespace ProjectMVC.Models
{
    public class PlannedTask:IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name has to be atleast {2} characters long!")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 30, ErrorMessage = "Description is too short! Use atleast {2} characters")]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [TaskType]
        [Display(Name = "TaskType")]
        public string taskType { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Select importance from 1 to 10 [X/10]")]
        [Display(Name = "Importance")]
        public int importance { get; set; }


        [Required]
        [Display(Name = "Project ID")]
        public int projectId { get; set; }

        [Display(Name = "Project")]
        public virtual Project project { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (importance >= 9 && description.Length < 50)
                yield return new ValidationResult("Tasks of such high priority require much longer description!", new[] { "importance", "description" });
  
        }
       
    }
}
