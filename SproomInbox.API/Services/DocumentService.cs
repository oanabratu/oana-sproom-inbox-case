
using SproomInbox.API.Data;
using SproomInbox.API.Data.Entities;
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
        public async Task<ServiceResult<Document>> CreateDocumentAsync(DocumentModel newDocument)
        {
            var result = new ServiceResult<Document>();

            if (string.IsNullOrEmpty(newDocument.AssignedToUser) == false)
            {
                var foundUser = await _userRepository.GetUserByIdAsync(newDocument.AssignedToUser);

                if (foundUser == null)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"User {newDocument.AssignedToUser} is not found";
                    return result;
                }
            }

            // Check if the document with this Id is not already present in the db
            var foundDocument = await _documentRepository.GetDocumentByIdAsync(newDocument.Id);

            if (foundDocument != null)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document {newDocument.Id} is already created";
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

            result.Data = await _documentRepository.CreateDocumentAsync(document);
            result.IsSuccessful = true;
            return result;
        }

        /// <summary>
        /// Get all documents service
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<Document>>> GetAllDocumentsAsync(DocumentQueryParams queryParams)
        {
            var result = new ServiceResult<IEnumerable<Document>>();
            var getAllDocuments = await _documentRepository.GetAllDocumentsAsync(queryParams);
            result.Data = getAllDocuments;
            return result;
        }

        /// <summary>
        /// Aprove document service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>
        public async Task<ServiceResult<Document>> ApproveDocumentAsync(Guid id, ChangeStateParams changeStateParams)
        {
            var result = new ServiceResult<Document>();

            //document by GUID
            var foundDocument = await _documentRepository.GetDocumentByIdAsync(id);

            if (foundDocument == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document {id} not found";
                return result;
            }

            if (foundDocument.State == State.Approved) 
            {
                result.IsSuccessful = false;
                result.ErrorMessage = $"Document {id} already approved";
                return result;
            }

            if (string.IsNullOrEmpty(changeStateParams.Username) == false)
            {
                var foundUser = _userRepository.GetUserByIdAsync(changeStateParams.Username);

                if (foundUser == null)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"User {changeStateParams.Username} is not found";
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

            _mailService.SendEmail(foundDocument.AssignedToUser,  $"Document {foundDocument.Id} has been approved!");

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
        public async Task<ServiceResult<Document>> RejectDocumentAsync(Guid id, ChangeStateParams changeStateParams)
        {
            var result = new ServiceResult<Document>();

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
                    result.ErrorMessage = $"User {changeStateParams.Username} is not found";
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
        public async Task<ServiceResult<Document>> GetDocumentByIdAsync(Guid id)
        {
            var result = new ServiceResult<Document>();

            var document = await _documentRepository.GetDocumentByIdAsync(id);

            result.Data = document;
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
