using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;

namespace IntergrationTests
{
    [TestFixture]
    public class IntegrationGetIncomingAndOutcomingMessagesTests
    {
        private IMessageRepository _messageRepository;

        private IMessageService _messageService;

        private IUserRepository _userRepository;

        private IUserService _userService;

        protected int SenderId {get; private set;}

        protected int RecipientId { get; private set;}

    

        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository();

            _messageRepository = new MessageRepository();

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

            SenderId = _userService.FindByEmail(userRegistrationData2.Email).Id;
            RecipientId = _userService.FindByEmail(userRegistrationData1.Email).Id;

            MessageSendData messageSendData1 = new MessageSendData()
            {
                Content = "Lol",
                EmailRecipient = userRegistrationData1.Email,
                SenderId = SenderId
            };

            MessageSendData messageSendData2 = new MessageSendData()
            {
                Content = "Pol",
                EmailRecipient = userRegistrationData1.Email,
                SenderId = SenderId
            };

            MessageSendData messageSendData3 = new MessageSendData()
            {
                Content = "PoP",
                EmailRecipient = userRegistrationData1.Email,
                SenderId = SenderId
            };

            _messageService.CreateMessage(messageSendData1);
            _messageService.CreateMessage(messageSendData2);
            _messageService.CreateMessage(messageSendData3);


        }

        [TearDown]
        public void TearDown()
        {
            var messages = _messageService.GetMessagesByRecipient("lol@gmail.com", SenderId);
            foreach (var obj in messages)
            {
                _messageService.DeleteById(obj.Id);
            }
            _userService.DeleteById(RecipientId);
            _userService.DeleteById(SenderId);

        }

        [Test]

        public void GetMessageByRecipient_IsValidData_Succes()
        {
            //Arrange
            string email = "lol@gmail.com";

            //Act
            TestDelegate actionGetMessage = () => _messageService.GetMessagesByRecipient(email, SenderId);

            //Assert
            Assert.DoesNotThrow(actionGetMessage);


        }

        [Test]

        public void GetMessageByRecipient_RecipientEmailIsNull_Succes()
        {
            //Arrange
            string email = null;

            //Act
            TestDelegate actionGetMessage = () => _messageService.GetMessagesByRecipient(email, SenderId);

            //Assert
            Assert.Catch<ArgumentNullException>(actionGetMessage);

        }

        [Test]

        public void GetMessageBySenderId_IsValidData_Succes()
        {
            //Arrange
            var sender = _userService.FindByEmail("lol2@gmail.com");

            //Act
            TestDelegate actionGetMessage = () => _messageService.GetMessagesBySenderId(sender.Id);

            //Assert
            Assert.DoesNotThrow(actionGetMessage);

        }

        [Test]

        public void GetMessageBySenderId_SenderIdIsZero_Succes()
        {
            //Arrange
            int senderId = 0;

            //Act
            TestDelegate actionGetMessage = () => _messageService.GetMessagesBySenderId(senderId);

            //Assert
            Assert.Catch<ArgumentException>(actionGetMessage);

        }
    }
}
