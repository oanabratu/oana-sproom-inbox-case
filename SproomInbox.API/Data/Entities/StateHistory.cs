namespace SproomInbox.API.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StateHistory
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public State State { get; set; } 
        public string? Username { get; set; }
    }
}
