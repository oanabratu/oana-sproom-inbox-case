namespace SproomInbox.API.Data.Entities
{
    public class Document
  {
        //document id
        public Guid Id { get; set; }
        //document type
        public DocumentType DocumentType { get; set; }
        //Current state
        public State State { get; set; }
        //document state history
        public ICollection<StateHistory> DocumentStates { get; set; }
        //creation date of the document
        public DateTime CreationDate { get; set; }
        //file reference
        public string FileReference { get; set; }

        public string AssignedToUser { get; set; }
    }
}
