using NUnit.Framework;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;

namespace IntergrationTests
{
    public class IntergrationAddFriendTests
    {
        private IFriendRepository _friendRepository;

        private IUserRepository _userRepository;

        private IFriendService _friendService;

        private IUserService _userService;

        [SetUp]

        public void Setup()
        {
            _userRepository = new UserRepository();

            _friendRepository = new FriendRepository();

            _friendService = new FriendService(_userRepository, _friendRepository);

            _userService = new UserService(_userRepository);

            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Email = "Lol@gmail.com",
                Password = "1234567890",
                Firstname = "Lo",
                Lastname = "Po"
            };

            _userService.Registration(userRegistrationData);

        }

        [TearDown]

        public void TearDown()
        {
            var user = _userService.FindByEmail("Lol@gmail.com");
            _userService.DeleteById(user.Id);
        }

        [Test]

        public void AddFriend_IsValidDataFriend_Succes()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                Email_Friend = "Lol@gmail.com",
                User_Id = 1,
            };

            //Act
           TestDelegate actionAddFriend = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.DoesNotThrow(actionAddFriend);
            var user = _userService.FindByEmail(friendData.Email_Friend);
            Assert.DoesNotThrow(() => _friendService.DeleteFriend(friendData.Email_Friend, friendData.User_Id));

        }

        [Test]

        public void AddFriend_EmailIsNull_Succes()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                Email_Friend = null,
                User_Id = 1,
            };

            //Act
            TestDelegate actionAddFriend = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<ArgumentException>(actionAddFriend);

        }

        [Test]

        public void AddFriend_EmailIsEmpty_Succes()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                Email_Friend = string.Empty,
                User_Id = 1,
            };

            //Act
            TestDelegate actionAddFriend = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<ArgumentException>(actionAddFriend);

        }

        [Test]

        public void AddFriend_UserNotFound_Succes()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                Email_Friend = "l@mail.com",
                User_Id = 1,
            };

            TestDelegate actionAddFriend = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<UserNotFoundException>(actionAddFriend);

        }

        [Test]

        public void AddFriend_FriendDataIsNull_Succes()
        {
            //Arrange
            FriendAddData friendData = null;

            //Act
            TestDelegate actionAddFriend = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<ArgumentNullException>(actionAddFriend);

        }


    }
}
