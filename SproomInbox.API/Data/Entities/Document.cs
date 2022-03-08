namespace SproomInbox.API.Data.Entities
{
    /// <summary>
    /// Document entity that get stored in the database
    /// </summary>
    public class Document
  {
        /// <summary>
        /// Document id - primary key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Document type 
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Current state
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Document state history
        /// </summary>
        public ICollection<StateHistory> DocumentStates { get; set; }

        /// <summary>
        /// Creation date of the document
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// File reference
        /// </summary>
        public string FileReference { get; set; }

        /// <summary>
        /// Assigned To User
        /// </summary>
        public string AssignedToUser { get; set; }
    }
}
