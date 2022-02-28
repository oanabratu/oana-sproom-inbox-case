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
        /// 
        /// </summary>
        /// <param name="documentEntity"></param>
        /// <returns></returns>
        public static DocumentModel? MapToModel(Document documentEntity)
        {
            if (documentEntity == null) return null;

            var model = new DocumentModel
            {
                Id = documentEntity.Id,
                CreationDate = documentEntity.CreationDate,
                State = (StateModel)(int)documentEntity.State,
                AssignedToUser = documentEntity.AssignedToUser,
                DocumentType = (DocumentTypeModel)(int)documentEntity.DocumentType
            };

            return model;
        }
    }
}
