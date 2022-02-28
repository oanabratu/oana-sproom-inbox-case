namespace SproomInbox.Shared
{
    public class RejectAllDocumentsParams
    {
        public List<Guid> DocumentIds { get; set; }

        public string Username { get; set; }
    }
}
