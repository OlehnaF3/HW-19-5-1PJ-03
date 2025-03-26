using NUnit.Framework;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;

namespace IntergrationTests
{
    [TestFixture]
    public class IntegrationDeleteFriendTests
    {

        private IFriendRepository _friendRepository;

        private IFriendService _friendService;

        private IUserRepository _userRepository;

        private IUserService _userService;

        [SetUp]

        public void Setup()
        {
            _friendRepository = new FriendRepository();

            _userRepository = new UserRepository();

            _friendService = new FriendService(_userRepository, _friendRepository);

            _userService = new UserService(_userRepository);

            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Email = "Lo@gmail.com",
                Firstname = "Lo",
                Lastname = "Po",
                Password = "1234567890"
            };

            _userService.Registration(userRegistrationData);
        }

        [TearDown]

        public void TearDown()
        {
            var user = _userService.FindByEmail("Lo@gmail.com");
            _userService.DeleteById(user.Id);
        }
        /// <summary>
        /// Тест проверяет успешное удаление друга при корректных данных.
        /// </summary>
        [Test]

        public void DeleteFriend_IsValidData_Succes()
        {
            //Arrange
            FriendAddData friendAddData = new FriendAddData()
            {
                Email_Friend = "Lo@gmail.com",
                User_Id = 1
            };


            //Act
            TestDelegate actionAddFriend = () => _friendService.AddFriend(friendAddData);
            TestDelegate actionDeleteFriend = () => _friendService.DeleteFriend(friendAddData.Email_Friend, friendAddData.User_Id);

            //Assert
            Assert.DoesNotThrow(actionAddFriend);
            Assert.DoesNotThrow(actionDeleteFriend);
        }
        /// <summary>
        /// Тест проверяет, что при попытке удалить друга с null email выбрасывается исключение ArgumentNullException.
        /// </summary>
        [Test]

        public void DeleteFriend_EmailIsNull_ThrowsException()
        {
            //Arrange
            FriendAddData friendAddData = new FriendAddData()
            {
                Email_Friend = null,
                User_Id = 1
            };

            //Act
            TestDelegate actionDeleteFriend = () => _friendService.DeleteFriend(friendAddData.Email_Friend, friendAddData.User_Id);

            //Assert
            Assert.Catch<ArgumentNullException>(actionDeleteFriend);
        }
        /// <summary>
        /// Тест проверяет, что при попытке удалить друга с пустым email выбрасывается исключение ArgumentNullException
        /// </summary>
        [Test]

        public void DeleteFriend_EmailIsEmpty_ThrowsException()
        {
            //Arrange
            FriendAddData friendAddData = new FriendAddData()
            {
                Email_Friend = string.Empty,
                User_Id = 1
            };

            //Act
            TestDelegate actionDeleteFriend = () => _friendService.DeleteFriend(friendAddData.Email_Friend, friendAddData.User_Id);

            //Assert
            Assert.Catch<ArgumentNullException>(actionDeleteFriend);



        }
        /// <summary>
        /// Тест проверяет, что при попытке удалить друга, который не существует, выбрасывается исключение UserNotFoundException.
        /// </summary>
        [Test]

        public void DeleteFriend_UserNotFound_ThrowsException()
        {
            //Arrange
            FriendAddData friendAddData = new FriendAddData()
            {
                Email_Friend = "Lo1@gmail.com",
                User_Id = 1
            };

            //Act
            TestDelegate actionDeleteFriend = () => _friendService.DeleteFriend(friendAddData.Email_Friend, friendAddData.User_Id);

            //Assert
            Assert.Catch<UserNotFoundException>(actionDeleteFriend);



        }
        /// <summary>
        /// Тест проверяет, что при попытке удалить друга, с которым нет дружбы, выбрасывается общее исключение.
        /// </summary>
        [Test]

        public void DeleteFriend_AreNotFriend_ThrowsException()
        {
            //Arrange
            FriendAddData friendAddData = new FriendAddData()
            {
                Email_Friend = "Lo@gmail.com",
                User_Id = 1
            };

            //Act
            TestDelegate actionDeleteFriend = () => _friendService.DeleteFriend(friendAddData.Email_Friend, friendAddData.User_Id);

            //Assert
            Assert.Catch<Exception>(actionDeleteFriend);

        }
    }
}
