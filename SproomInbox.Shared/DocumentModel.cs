﻿using System.ComponentModel.DataAnnotations;

namespace SproomInbox.Shared
{
    public class DocumentModel
    {
        public bool Selected { get; set; }

        public Guid Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int DocumentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Creation date of the document
        /// </summary>

        public DateTime CreationDate { get; set; }

        /// <summary>
        /// file reference
        /// </summary>
        public string FileReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AssignedToUser { get; set; }
    }
}