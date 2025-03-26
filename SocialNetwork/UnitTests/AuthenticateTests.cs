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
    public class AuthenticateTests
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

        public void AuthenticateUser_isValidData_Succes()
        {
            //Arrange 
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Password = "123456789",
                Email = "p@gmail.com"
            };

            UserEntity userEntity = new UserEntity()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Email = "p@gmail.com",
                Password = "123456789"
            };
            _userRepositoryMock.Setup(rep=>rep.FindByEmail(userAuthenticationData.Email)).Returns(userEntity);
             
            //Act
            TestDelegate action = () => _userService.Authenticate(userAuthenticationData);


            //Assert
            Assert.DoesNotThrow(action);
            _userRepositoryMock.Verify(rep => rep.FindByEmail(userAuthenticationData.Email), Times.Once());


        }

        [Test]

        public void AuthenticateUser_IsInvalidEmail_ExceptionThrown()
        {

            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Password = "123456789",
                Email = "gmail.ru"
            };

            //Act
            TestDelegate action = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(action);
        }

        [Test]

        public void AuthenticateUser_IsInvalidPassword_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "p@gmail.com",
                Password = "1234567891"
            };

            UserEntity userEntity = new UserEntity()
            {
                Email = "p@gmail.com",
                Password = "123456789"
            };

            _userRepositoryMock.Setup(rep => rep.FindByEmail(userAuthenticationData.Email)).Returns(userEntity);

            //Act
            TestDelegate action = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<WrongPasswordException>(action);
            _userRepositoryMock.Verify(rep => rep.FindByEmail(userAuthenticationData.Email), Times.Once);
        }


        [Test]

        public void AuthenticateUser_PasswordIsEmpty_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "p@gmail.com",
                Password = ""
            };

            //Act
            TestDelegate action = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(action);
            
        }

        [Test]

        public void AuthenticateUser_PasswordIsNull_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "p@gmail.com",
                Password = null
            };

            //Act
            TestDelegate action = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(action);
        }

    }
}
