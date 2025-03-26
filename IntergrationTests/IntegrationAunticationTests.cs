using NUnit.Framework;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;

namespace IntergrationTests
{
    [TestFixture]
    public class IntegrationAunticationTests
    {
        private IUserRepository _userRepository;

        private IUserService _userService;

        [SetUp]

        public void Setup()
        {
            _userRepository = new UserRepository();

            _userService = new UserService(_userRepository);

            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Email = "pp@gmail.com",
                Password = "123456789",
                Firstname = "Lo",
                Lastname = "Po"
            };
            _userService.Registration(userRegistrationData);
        }

        [TearDown]

        public void Teardown()
        {
            var user = _userService.FindByEmail("pp@gmail.com");
            _userService.DeleteById(user.Id);
        }

        [Test]

        public void AuthenticateUser_isValidData_Succes()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "pp@gmail.com",
                Password = "123456789"
            };


            //Act
            TestDelegate action = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.DoesNotThrow(action);
            var user = _userService.FindByEmail(userAuthenticationData.Email);
            Assert.That(user.Password, Is.EqualTo(userAuthenticationData.Password));
            Assert.That(user.Email, Is.EqualTo(userAuthenticationData.Email));
        }

        [Test]

        public void AuthenticateUser_DataIsNull_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = null;

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);


            //Assert
            Assert.Catch<ArgumentNullException>(actionAuthenticate);
        }

        [Test]

        public void AuthenticateUser_PasswordIsNull_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "pp@gmail.com",
                Password = null
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(actionAuthenticate);
            var user = _userService.FindByEmail(userAuthenticationData.Email);
            Assert.That(user.Email, Is.EqualTo(userAuthenticationData.Email));
        }

        [Test]

        public void AuthenticateUser_PasswordIsEmpty_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "pp@gmail.com",
                Password = string.Empty
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(actionAuthenticate);
            var user = _userService.FindByEmail(userAuthenticationData.Email);
            Assert.That(user.Email, Is.EqualTo(userAuthenticationData.Email));

        }

        [Test]

        public void AuthenticateUser_EmailIsNull_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = null,
                Password = "123456789"
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(actionAuthenticate);

        }

        [Test]

        public void AuthenticateUser_EmailIsInvalid_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "logmail.com",
                Password = "123456789"
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(actionAuthenticate);


        }

        [Test]

        public void AuthenticateUser_EmailIsEmpty_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = string.Empty,
                Password = "123456789"
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<ArgumentException>(actionAuthenticate);


        }

        [Test]

        public void AuthenticateUser_UserNotFound_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "pp1@gmail.com",
                Password = "123456789"
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);

            //Assert
            Assert.Catch<UserNotFoundException>(actionAuthenticate);


        }

        [Test]

        public void AuthenticateUser_PasswordIsWrong_ExceptionThrown()
        {
            //Arrange
            UserAuthenticationData userAuthenticationData = new UserAuthenticationData()
            {
                Email = "pp@gmail.com",
                Password = "1234567890"
            };

            //Act
            TestDelegate actionAuthenticate = () => _userService.Authenticate(userAuthenticationData);


            //Assert
            Assert.Catch<WrongPasswordException>(actionAuthenticate);

        }
    }
}
