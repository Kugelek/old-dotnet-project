using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Validators
{
    public class TaskType : ValidationAttribute
    {
        enum TaskTypeEnum { frontend, backend, devops, management, other }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Invalid category. Available categories " +
                    string.Join(",", Enum.GetNames(typeof(TaskTypeEnum))));

            string taskTypeInput = value.ToString();

            foreach (TaskTypeEnum singleTaskType in Enum.GetValues(typeof(TaskTypeEnum))){
                if (singleTaskType.ToString().Equals(taskTypeInput))
                    return ValidationResult.Success;
            }
            return new ValidationResult("Invalid category. Available categories" +
                string.Join(",", Enum.GetNames(typeof(TaskTypeEnum))));

        }
    }

}
