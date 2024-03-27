using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetOnIt.Models
{
    /// <summary>
    /// Tasks class will have information of what a student has to do 
    /// So their tasks, priority, type of task, etc.
    /// </summary>
    public class Tasks : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Specifies Id should be auto-assigned/incremented by 1
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter task description")]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime DateStart { get; set; } = DateTime.Today; //default todays date                                                                   
        
        [Display(Name = "Deadline")]
        public DateTime DateEnd{ get; set; } 

        [Required(ErrorMessage = "Select Type")]
        [Display(Name = "Type of Task")]
        public TaskType Type { get; set; } 

        //Priority Property 
        [Required(ErrorMessage = "Select Priority")]
        [Display(Name = "Priority Level")]
        public TaskPriority Priority { get; set; }



        [Display(Name = "Task Complete?")]
        public bool? IsCompleted { get; set; } //nullable because once they create a task they wont fill out this options

        //Foreign Key to Customer User who "owns" the task
        public string UserId { get; set; } //Using string instead of INT because ASP.NET Core Identity system stores the value as a string.
        public ApplicationUser User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Todays date and subtract one month to set lower limit of start date
            DateTime oneMonthAgo = DateTime.Today.AddMonths(-1);

            if (DateStart < oneMonthAgo)
            {
                yield return new ValidationResult("Start Date cannot exceed one month in the past", new[] { "DateStart" });
            }
            if (DateEnd < DateTime.Today)
            {
                yield return new ValidationResult("Deadline cannot be in the past", new[] {"DateEnd"});
            }

        }
    }
}
