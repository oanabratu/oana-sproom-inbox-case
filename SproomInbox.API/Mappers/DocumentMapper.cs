using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Mappers
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentMapper
    {
        /// <summary>
        /// Map from Entity Framework model to UI model 
        /// </summary>
        /// <param name="documentEntity"></param>
        /// <returns></returns>
        public static DocumentModel MapToModel(Document documentEntity)
        {
            var model = new DocumentModel
            {
                Id = documentEntity.Id,
                CreationDate = documentEntity.CreationDate,
                State = (DocumentStateModel)(int)documentEntity.State,
                FileReference = documentEntity.FileReference,
                AssignedToUser = documentEntity.AssignedToUser,
                DocumentType = (DocumentTypeModel)(int)documentEntity.DocumentType
            };

            return model;
        }

        /// <summary>
        /// Map from Shared model to Entity Framework model
        /// </summary>
        /// <param name="documentEntity"></param>
        /// <returns></returns>
        public static StateHistoryModel MapToModel(StateHistory documentEntity)
        {
            var model = new StateHistoryModel
            {
                Id = documentEntity.Id,
                Timestamp = documentEntity.Timestamp,
                State = (DocumentStateModel)(int)documentEntity.State,
                Username = documentEntity.Username
            };

            return model;
        }
    }
}
