using Moq;
using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;

namespace SocialNetwork.UnitTests
{
    [TestFixture]
    public class RegistrationTests
    {
        private Mock<IUserRepository> _userRepositoryMock;

        private IUserService _userService;

        [SetUp]

        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            _userService = new UserService(_userRepositoryMock.Object);

        }

        [Test]

        public void RegistrationUser_IsValidData_Succes()
        {
            //Arrange 
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "p@gmail.com",
                Password = "password1"
            };

            _userRepositoryMock.Setup(rep => rep.Create(It.IsAny<UserEntity>())).Returns(1);

            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.DoesNotThrow(action);
            _userRepositoryMock.Verify(rep => rep.Create(It.IsAny<UserEntity>()), Times.Once);
        }


        [Test]

        public void RegistrationUser_IsInvalidFirstname_ExceptionThrown()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "",
                Lastname = "Po",
                Email = "p@gmail.com",
                Password = "password1"
            };

            //Act 
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Throws<ArgumentException>(action);


        }

        [Test]

        public void RegistrationUser_IsInvalidLastname_ExceptionThrown()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "",
                Email = "p@gmail.com",
                Password = "password1"
            };

            //Act 
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]

        public void RegistrationUser_IsInvalidPasswordLengthLessThan8_ExceptionThrown()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "p@gmail.com",
                Password = "passwor"
            };

            //Act 
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Throws<ArgumentException>(action);

        }

        [Test]

        public void RegistrationUser_IsInvalidEmail_ExceptionThrown()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "pgmail",
                Password = "password1"
            };

            //Act 
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]

        public void RegistrationUser_UserIsAlreadyRegistered_ExceptionThrown()
        {
            //Arrange
            UserRegistrationData userRegistrationData1 = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "pg@mail.com",
                Password = "password1"
            };

            UserEntity userEntity = new UserEntity()
            {
                Firstname = userRegistrationData1.Firstname,
                Lastname = userRegistrationData1.Lastname,
                Email = userRegistrationData1.Email,
                Password = userRegistrationData1.Password
            };
            _userRepositoryMock.Setup(rep => rep.FindByEmail(userRegistrationData1.Email)).Returns(userEntity);

            //Act 
            TestDelegate action1 = () => _userService.Registration(userRegistrationData1);


            //Assert
            Assert.Throws<ArgumentException>(action1);

            _userRepositoryMock.Verify(rep => rep.FindByEmail(userRegistrationData1.Email), Times.Once);

        }

        [Test]

        public void RegistrationUser_DataIsNull_ExceptionThrown()
        {
            //Arrange
            UserRegistrationData userRegistrationData = null;

            //Act 
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

    }

}
