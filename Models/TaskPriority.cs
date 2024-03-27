using System.ComponentModel;

namespace GetOnIt.Models
{
    /// <summary>
    /// Allows student to decide the importance of their task
    /// </summary>
    public enum TaskPriority
    {
        [Description("No Priority")]
        None,
        [Description("Low Priority")]
        Low,
        [Description("Medium Priority")]
        Medium,
        [Description("High Priority")]
        High,
    }
}
