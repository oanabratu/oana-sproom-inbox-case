using System.ComponentModel.DataAnnotations;

namespace SproomInbox.Shared
{
    public class DocumentModel
    {
        public bool Selected { get; set; }

        [Required]
        public Guid Id { get; set; }


        [Required]
        /// <summary>
        /// 
        /// </summary>
        public DocumentTypeModel DocumentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DocumentStateModel State { get; set; }

        /// <summary>
        /// Creation date of the document
        /// </summary>

        public DateTime CreationDate { get; set; }

        [Required]
        /// <summary>
        /// file reference
        /// </summary>
        public string FileReference { get; set; }

        [Required]
        /// <summary>
        /// 
        /// </summary>
        public string AssignedToUser { get; set; }
    }
}