using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SproomInbox.API.Data;
using SproomInbox.API.Data.Entities;
using SproomInbox.API.Services;
using SproomInbox.Shared;
using System.Threading.Tasks;

namespace SproomInbox.API.Tests
{
    [TestClass]
    public class DocumentServiceTests
    {
        IDocumentRepository _documentRepository;
        IUserRepository _userRepository;
        INullMailService _mailService;

        [TestInitialize]
        public void TestInitialize()
        {
            _documentRepository = Substitute.For<IDocumentRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _mailService = Substitute.For<INullMailService>();
        }

        [TestMethod]
        public async Task CreateDocumentAsync_ReturnsError_When_User_Is_Not_Found()
        {
            DocumentService documentService = new DocumentService(_documentRepository, _userRepository, _mailService);

            string username = "Oana";

            var result = await documentService.CreateDocumentAsync(new DocumentModel
            {
                AssignedToUser = username
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsSuccessful, false);
            Assert.AreEqual(result.ErrorMessage, $"User {username} is not found");
        }

        [TestMethod]
        public async Task CreateDocumentAsync_IsSuccessful()
        {
            
            _userRepository.GetUserByIdAsync(Arg.Any<string>()).Returns(new User());
            _documentRepository.CreateDocumentAsync(Arg.Any<Document>()).Returns(new Document());

            DocumentService documentService = new DocumentService(_documentRepository, _userRepository, _mailService);

            string username = "Oana";

            var result = await documentService.CreateDocumentAsync(new DocumentModel
            {
                FileReference = "FileReference123",
                DocumentType = DocumentTypeModel.Invoice,
                State = StateModel.Received,
                AssignedToUser = username,
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}