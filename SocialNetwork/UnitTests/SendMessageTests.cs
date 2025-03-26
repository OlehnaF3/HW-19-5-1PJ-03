using Moq;
using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Text;

namespace SocialNetwork.UnitTests
{
    [TestFixture]
    public class SendMessageTests
    {
        private Mock<IUserRepository> _userRepositoryMock;

        private Mock<IMessageRepository> _messageRepositoryMock;

        private IMessageService _messageService;

        [SetUp]

        public void SetUp()
        {
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _messageService = new MessageService(_messageRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Test]

        public void SendMessage_IsValidData_Succes()
        {
            //Arrange
            var messageSendData = new MessageSendData
            {
                Content = "Test",
                EmailRecipient = "p@gmail.com",
                SenderId = 1
            };

            var userRecipient = new UserEntity { Id = 2, Email = messageSendData.EmailRecipient };

            _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendData.EmailRecipient)).Returns(userRecipient);
            _messageRepositoryMock.Setup(repo => repo.Create(It.IsAny<MessageEntity>())).Returns(1); // Simulate successful creation

            //Act
            TestDelegate action = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.DoesNotThrow(action);
            _userRepositoryMock.Verify(repo => repo.FindByEmail(messageSendData.EmailRecipient), Times.Once);
            _messageRepositoryMock.Verify(repo => repo.Create(It.IsAny<MessageEntity>()), Times.Once);
        }

        [Test]

        public void SendMessage_ContentLengthMoreThan5000_ThrowException()
        {
            //Arrange
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5001; i++)
            {
                sb.Append(i.ToString());
            }
            var messageSendData = new MessageSendData()
            {
                Content = sb.ToString(),
                EmailRecipient = "p@gmail.com",
                SenderId = 1
            };

            var userRecipient = new UserEntity { Id = 2, Email = messageSendData.EmailRecipient };
            _userRepositoryMock.Setup(rep => rep.FindByEmail(messageSendData.EmailRecipient)).Returns(userRecipient);

            //Act
            TestDelegate action = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentException>(action);
        }

        [Test]

        public void SendMessage_ContentIsNull_ThrowsException()
        {
            //Arrange
            var messageSendData = new MessageSendData()
            {
                Content = null,
                EmailRecipient = "p@gmail.com",
                SenderId = 1
            };

            var userRecipient = new UserEntity { Id = 2, Email = messageSendData.EmailRecipient };
            _userRepositoryMock.Setup(rep => rep.FindByEmail(messageSendData.EmailRecipient)).Returns(userRecipient);

            //Act
            TestDelegate action = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentNullException>(action);
        }

        [Test]

        public void SendMessage_RecipientIsNull_ThrowsException()
        {
            //Arrange
            var messageSendData = new MessageSendData()
            {
                Content = "Test",
                EmailRecipient = null,
                SenderId = 1
            };

            var userRecipient = new UserEntity { Id = 2, Email = messageSendData.EmailRecipient };
            _userRepositoryMock.Setup(rep => rep.FindByEmail(messageSendData.EmailRecipient)).Returns(userRecipient);

            //Act
            TestDelegate action = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentNullException>(action);
        }

        [Test]

        public void SendMessage_RecipientIsEmpty_ThrowsException()
        {
            //Arrange
            var messageSendData = new MessageSendData()
            {
                Content = "Test",
                EmailRecipient = "",
                SenderId = 1
            };

            var userRecipient = new UserEntity { Id = 2, Email = messageSendData.EmailRecipient };
            _userRepositoryMock.Setup(rep => rep.FindByEmail(messageSendData.EmailRecipient)).Returns(userRecipient);

            //Act
            TestDelegate action = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentNullException>(action);
        }

    }
}
