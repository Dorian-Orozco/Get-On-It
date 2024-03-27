using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetOnIt.Models
{
    /// <summary>
    /// Tasks class will have information of what a student has to do 
    /// So their tasks, priority, type of task, etc.
    /// </summary>
    public class Tasks
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
        public TaskType Type { get; set; } 

        //Priority Property 
        [Required(ErrorMessage = "Select Priority")]
        public TaskPriority Priority { get; set; }



        [Display(Name = "Task Complete?")]
        public bool IsCompleted { get; set; }

        //Foreign Key to Customer User who "owns" the task
        public string UserId { get; set; } //Using string instead of INT because ASP.NET Core Identity system stores the value as a string.
        public ApplicationUser User { get; set; }   
    }
}
