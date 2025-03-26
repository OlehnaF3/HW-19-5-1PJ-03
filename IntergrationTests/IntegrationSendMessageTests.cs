using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;
using System.Linq;
using System.Text;

namespace IntergrationTests
{
    [TestFixture]
    public class IntegrationSendMessageTests
    {
        private IMessageRepository _messageRepository;

        private IMessageService _messageService;

        private IUserService _userService;

        private IUserRepository _userRepository;

        protected int SenderId { get; private set; }

        protected int RecipientId { get; private set; }

        [SetUp]

        public void Setup()
        {
            _messageRepository = new MessageRepository();

            _userRepository = new UserRepository();

            _userService = new UserService(_userRepository);

            

            _messageService = new MessageService(_messageRepository, _userRepository);
            UserRegistrationData userRegistrationData1 = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "lol@gmail.com",
                Password = "1234567890"
            };

            UserRegistrationData userRegistrationData2 = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "lol2@gmail.com",
                Password = "1234567890"
            };

            _userService.Registration(userRegistrationData1);
            _userService.Registration(userRegistrationData2);

            RecipientId = _userService.FindByEmail("lol@gmail.com").Id;
            SenderId = _userService.FindByEmail("lol2@gmail.com").Id;

        }

        [TearDown]

        public void Teardown()
        {

            _userService.DeleteById(RecipientId);
            _userService.DeleteById(SenderId);
        }

        [Test]

        public void SendMessage_IsValidData_Succes()
        {
            //Arrange
            MessageSendData messageSendData = new MessageSendData()
            {
                Content = "lOL",
                EmailRecipient = "lol@gmail.com", // получатель!
                SenderId = SenderId
            };

            //Act
            TestDelegate actionSendMessage = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.DoesNotThrow(actionSendMessage);
            var message = _messageService.GetMessagesByRecipient(messageSendData.EmailRecipient, SenderId).First();
            _messageService.DeleteById(message.Id);
        }

        [Test]

        public void SendMessage_ContentIsNull_ThrowsException()
        {
            //Arrange
            MessageSendData messageSendData = new MessageSendData()
            {
                Content = null,
                EmailRecipient = "Lol@gmail.com", // получатель!
                SenderId = SenderId
            };

            //Act
            TestDelegate actionSendMessage = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentNullException>(actionSendMessage);
        }

        [Test]

        public void SendMessage_ContentMoreThan5000Length_ThrowsException()
        {
            //Arrange
            StringBuilder sr = new StringBuilder();

            for (int i = 0; i < 5001; i++)
            {
                sr.Append(i.ToString());
            }

            MessageSendData messageSendData = new MessageSendData()
            {
                Content = sr.ToString(),
                EmailRecipient = "Lol@gmail.com", // получатель!
                SenderId = SenderId
            };

            //Act
            TestDelegate actionSendMessage = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentException>(actionSendMessage);
        }

        [Test]

        public void SendMessage_EmailIsNull_ThrowsException()
        {
            //Arrange
            MessageSendData messageSendData = new MessageSendData()
            {
                Content = "lOL",
                EmailRecipient = null, // получатель!
                SenderId = SenderId
            };

            //Act
            TestDelegate actionSendMessage = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentException>(actionSendMessage);
        }

        [Test]

        public void SendMessage_EmailIsInValid_ThrowsException()
        {
            //Arrange
            MessageSendData messageSendData = new MessageSendData()
            {
                Content = "lOL",
                EmailRecipient = "Lolgmail.com", // получатель!
                SenderId = SenderId
            };

            //Act
            TestDelegate actionSendMessage = () => _messageService.CreateMessage(messageSendData);

            //Assert
            Assert.Catch<ArgumentException>(actionSendMessage);
        }
    }

}
