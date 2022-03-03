
using SproomInbox.API.Data;
using SproomInbox.API.Data.Entities;
using SproomInbox.API.Mappers;
using SproomInbox.Shared;

namespace SproomInbox.API.Services
{
    /// <summary>
    /// Service that handles Documents
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserRepository _userRepository;
        private readonly INullMailService _mailService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userRepository"></param>
        public DocumentService(IDocumentRepository repository, IUserRepository userRepository, INullMailService mailService)
        {
            _documentRepository = repository;
            _userRepository = userRepository;
            _mailService = mailService;
        }

        /// <summary>
        /// Create document service
        /// </summary>
        /// <param name="newDocument"></param>
        /// <returns></returns>
        public async Task<ServiceResult<DocumentModel>> CreateDocumentAsync(DocumentModel newDocument)
        {
            var result = new ServiceResult<DocumentModel>
            {
                IsSuccessful = false
            };

            if (newDocument.Id == Guid.Empty)
            {
                result.ErrorMessage = "Document id is missing.";
                return result;
            }

            if (string.IsNullOrEmpty(newDocument.AssignedToUser) == false)
            {
                var foundUser = await _userRepository.GetUserByIdAsync(newDocument.AssignedToUser);

                if (foundUser == null)
                {
                    result.ErrorMessage = $"User '{newDocument.AssignedToUser}' is not found";
                    return result;
                }
            }

            // Check if the document with this Id is not already present in the db
            var foundDocument = await _documentRepository.GetDocumentByIdAsync(newDocument.Id);

            if (foundDocument != null)
            {
                result.ErrorMessage = $"Document '{newDocument.Id}' is already created";
                return result;
            }

            var document = new Document
            {
                Id = newDocument.Id,
                CreationDate = DateTime.Now,
                FileReference = newDocument.FileReference,
                DocumentType = (DocumentType)newDocument.DocumentType,
                State = State.Received,
                AssignedToUser = newDocument.AssignedToUser,
                DocumentStates = new List<StateHistory>
                {
                    new StateHistory
                    {
                        State = State.Received,
                        Timestamp = DateTime.Now,
                    }
                }
            };

            var createdDocument = await _documentRepository.CreateDocumentAsync(document);

            if(createdDocument != null)
            {
                
                result.Data = DocumentMapper.MapToModel(createdDocument);
                result.IsSuccessful = true;
            }
            else
            {
                result.ErrorMessage = "Unable to create document";
            }

            return result;
        }

        /// <summary>
        /// Get all documents service
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<DocumentModel>>> GetAllDocumentsAsync(DocumentQueryParams queryParams)
        {
            var result = new ServiceResult<IEnumerable<DocumentModel>>();
            var getAllDocuments = await _documentRepository.GetAllDocumentsAsync(queryParams);
            result.Data = getAllDocuments.Select(d => DocumentMapper.MapToModel(d));
            return result;
        }

        /// <summary>
        /// Aprove document service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>
        public async Task<ServiceResult<DocumentModel>> ApproveDocumentAsync(Guid id, ChangeStateParams changeStateParams)
        {
            var result = new ServiceResult<DocumentModel>();

            //document by GUID
            var foundDocument = await _documentRepository.GetDocumentByIdAsync(id);

            if (foundDocument == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document '{id}' not found";
                return result;
            }

            if (foundDocument.State == State.Approved) 
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document '{id}' already approved";
                return result;
            }

            if (string.IsNullOrEmpty(changeStateParams.Username) == false)
            {
                var foundUser = _userRepository.GetUserByIdAsync(changeStateParams.Username);

                if (foundUser == null)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"User '{changeStateParams.Username}' is not found";
                    return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "User is mandatory";
                return result;
            }

            //update state 
            foundDocument.State = State.Approved;
            //update states history
            foundDocument.DocumentStates.Add(
                                    new StateHistory
                                    {
                                        State = State.Approved,
                                        Timestamp = DateTime.Now,
                                        Username = changeStateParams.Username
                                    }
                );

            _mailService.SendEmail(foundDocument.AssignedToUser,  $"Document '{foundDocument.Id}' has been approved!");

            await _documentRepository.SaveAsync();
            result.IsSuccessful = true;

            return result;
        }

        /// <summary>
        /// Reject document service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>
        public async Task<ServiceResult<DocumentModel>> RejectDocumentAsync(Guid id, ChangeStateParams changeStateParams)
        {
            var result = new ServiceResult<DocumentModel>();

            //document by GUID
            var foundDocument = await _documentRepository.GetDocumentByIdAsync(id);

            if (foundDocument == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document {id} not found";
                return result;
            }

            if (foundDocument.State == State.Rejected)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document {id} already rejected";
                return result;
            }

            if (string.IsNullOrEmpty(changeStateParams.Username) == false)
            {
                var foundUser = _userRepository.GetUserByIdAsync(changeStateParams.Username);

                if (foundUser == null)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"User '{changeStateParams.Username}' is not found";
                    return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "User is mandatory";
                return result;
            }

            //update state 
            foundDocument.State = State.Rejected;
            
            //update states history
            foundDocument.DocumentStates.Add(
                                    new StateHistory
                                    {
                                        State = State.Rejected,
                                        Timestamp = DateTime.Now,
                                        Username = changeStateParams.Username
                                    }
                );
            await _documentRepository.SaveAsync();
            result.IsSuccessful = true;

            return result;
        }

        /// <summary>
        /// Get document by id service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<DocumentModel>> GetDocumentByIdAsync(Guid id)
        {
            var result = new ServiceResult<DocumentModel>
            {
                IsSuccessful = false,
            };

            var document = await _documentRepository.GetDocumentByIdAsync(id);

            if(document == null)
            {
                result.ErrorMessage = $"Unable to get the document by id {id}";
                return result;
            }

            result.Data = DocumentMapper.MapToModel(document);
            result.IsSuccessful = true;

            return result;
        }

        /// <summary>
        /// Get document history service 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<StateHistory>>> GetDocumentHistoryAsync(Guid id)
        {
            var result = new ServiceResult<IEnumerable<StateHistory>>();

            result.IsSuccessful = true;
            result.Data = await _documentRepository.GetDocumentHistory(id);

            return result;
        }
    }
}
