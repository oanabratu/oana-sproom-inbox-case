namespace SproomInbox.API.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StateHistory
    {
        /// <summary>
        /// State Id - primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public State State { get; set; } 

        /// <summary>
        /// Username that make the action
        /// </summary>
        public string? Username { get; set; }
    }
}
