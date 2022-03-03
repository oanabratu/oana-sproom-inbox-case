using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SproomInbox.API.Data;
using SproomInbox.API.Data.Entities;
using SproomInbox.API.Services;
using SproomInbox.Shared;
using System;
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
        public async Task CreateDocumentAsync_Without_DocumentId_ReturnsError()
        {
            DocumentService documentService = new DocumentService(_documentRepository, _userRepository, _mailService);

            var result = await documentService.CreateDocumentAsync(new DocumentModel
            {
                Id = Guid.Empty,
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsSuccessful, false);
            Assert.AreEqual(result.ErrorMessage, $"Document id is missing.");
        }


        [TestMethod]
        public async Task CreateDocumentAsync_ReturnsError_When_User_Is_Not_Found()
        {
            DocumentService documentService = new DocumentService(_documentRepository, _userRepository, _mailService);

            string username = "Oana";


            var result = await documentService.CreateDocumentAsync(new DocumentModel
            {
                Id = Guid.NewGuid(),
                AssignedToUser = username
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsSuccessful, false);
            Assert.AreEqual(result.ErrorMessage, $"User '{username}' is not found");
        }

        [TestMethod]
        public async Task CreateDocumentAsync_ReturnsError_When_Document_Is_Already_Created()
        {
            DocumentService documentService = new DocumentService(_documentRepository, _userRepository, _mailService);

            string username = "Oana";

            _documentRepository.GetDocumentByIdAsync(Arg.Any<Guid>()).Returns(new Document());

            var result = await documentService.CreateDocumentAsync(new DocumentModel
            {
                Id = Guid.NewGuid(),
                AssignedToUser = username
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsSuccessful, false);
            Assert.AreEqual(result.ErrorMessage, $"User '{username}' is not found");
        }


        [TestMethod]
        public async Task CreateDocumentAsync_ReturnsError_When_Not_Able_To_Store_To_Database()
        {

            _userRepository.GetUserByIdAsync(Arg.Any<string>()).Returns(new User());

            DocumentService documentService = new DocumentService(_documentRepository, _userRepository, _mailService);

            string username = "Oana";

            var result = await documentService.CreateDocumentAsync(new DocumentModel
            {
                Id = Guid.NewGuid(),
                FileReference = "FileReference123",
                DocumentType = DocumentTypeModel.Invoice,
                State = DocumentStateModel.Received,
                AssignedToUser = username,
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ErrorMessage, "Unable to create document");
            Assert.IsFalse(result.IsSuccessful);
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
                Id = Guid.NewGuid(),
                FileReference = "FileReference123",
                DocumentType = DocumentTypeModel.Invoice,
                State = DocumentStateModel.Received,
                AssignedToUser = username,
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}