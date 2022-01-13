namespace SproomInbox.API
{
    public class Document
    {
        /// <summary>
        /// A unique identification of the document
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDate { get; set; }
    }
}