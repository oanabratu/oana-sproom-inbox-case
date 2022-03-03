namespace SproomInbox.Shared
{
    public class ApproveAllDocumentsParams
    {
        public IList<Guid> DocumentIds { get; set; }

        public string Username { get; set; }
    }
}
