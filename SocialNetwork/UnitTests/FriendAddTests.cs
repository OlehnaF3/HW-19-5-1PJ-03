using Moq;
using NUnit.Framework;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;

namespace SocialNetwork.UnitTests
{
    [TestFixture]
    public class FriendAddTests
    {
        private Mock<IFriendRepository> _friendRepository;

        private Mock<IUserRepository> _userRepository;

        private IFriendService _friendService;



        [SetUp]

        public void Setup()
        {
            _friendRepository = new Mock<IFriendRepository>();

            _userRepository = new Mock<IUserRepository> ();

            _friendService = new FriendService(_userRepository.Object, _friendRepository.Object);
        }


        [Test]

        public void AddFriend_IsValidData_Succes()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                User_Id = 1,
                Email_Friend = "lol@gmail.com"
            };

            UserEntity userEntity = new UserEntity()
            {
                Id = 2,
                Firstname = "Lo"
            };

            _userRepository.Setup(rep => rep.FindByEmail(It.IsAny<string>())).Returns(userEntity);

            _friendRepository.Setup(rep => rep.FindFriendsByFriendId(userEntity.Id,friendData.User_Id)).Returns((FriendsEntity)null);

            _friendRepository.Setup(rep => rep.Create(It.IsAny<FriendsEntity>())).Returns(1);

            //Act
            TestDelegate action = () => _friendService.AddFriend(friendData);


            //Assert
            Assert.DoesNotThrow(action);

            _userRepository.Verify(rep => rep.FindByEmail(It.IsAny<string>()), Times.Once());

            _friendRepository.Verify(rep => rep.FindFriendsByFriendId(userEntity.Id, friendData.User_Id), Times.Once);

            _friendRepository.Verify(rep=> rep.Create(It.IsAny<FriendsEntity>()), Times.Once());
        }

        [Test]

        public void AddFriend_UserNotFound_ThrowsException()
        {
            //Arragne
            FriendAddData friendData = new FriendAddData()
            {
                User_Id = 5,
                Email_Friend = "lol@asd.ru"
            };
            _userRepository.Setup(rep => rep.FindByEmail(friendData.Email_Friend)).Returns((UserEntity)null);
            //Act
            TestDelegate action = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<UserNotFoundException>(action);
            _userRepository.Verify(rep=>rep.FindByEmail(friendData.Email_Friend), Times.Once());
        }

        [Test]

        public void AddFriend_EmailIsEmpty_ThrowsException()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                User_Id = 1,
                Email_Friend = string.Empty,
            };

            //Act
            TestDelegate action = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<ArgumentNullException>(action);
        }

        [Test]

        public void AddFriend_EmailIsNull_ThrowsException()
        {
            FriendAddData friendData = new FriendAddData()
            {
                User_Id = 1,
                Email_Friend = null
            };

            //Act
            TestDelegate action = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<ArgumentNullException>(action);
        }

        [Test]

        public void AddFriend_EmailIsInvalid_ThrowsException()
        {
            //Arrange
            FriendAddData friendData = new FriendAddData()
            {
                User_Id = 1,
                Email_Friend = "LSa"
            };

            //Act
            TestDelegate action = () => _friendService.AddFriend(friendData);

            //Assert
            Assert.Catch<ArgumentException>(action);
        }
    }
}
