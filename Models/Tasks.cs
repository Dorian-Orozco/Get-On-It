namespace GetOnIt.Models
{
    /// <summary>
    /// Tasks class will have information of what a user has to do, when they start/deadline, as well as 
    /// if they have completed it or not.
    /// </summary>
    public class Tasks
    {
        //Cache the user ID in session when logging in to prevent injectable attacks or spoofing IDs through inputs.
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd{ get; set; }
        public bool IsCompleted { get; set; }


        //Foreign Key to Customer User who "owns" the task
        public string UserId { get; set; } //Using string instead of INT because ASP.NET Core Identity system stores the value as a string.
        public ApplicationUser User { get; set; }   
    }
}
