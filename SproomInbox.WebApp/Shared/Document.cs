namespace SproomInbox.WebApp.Shared
{
    public class Document
    {
        public Guid Id { get; set; }

        public int DocumentType { get; set; }

        public int State { get; set; }

        public string FileReference { get; set; }

        public string AssignedToUser { get; set; }

        public DateTime CreationDate { get; set; }
    }
}