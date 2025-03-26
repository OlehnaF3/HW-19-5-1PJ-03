using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Exceptions;
using System;

namespace IntegrationTests
{
    [TestFixture]

    public class IntegrationRegistrationTests
    {
        private IUserService _userService;

        private IUserRepository _userRepository;

        [SetUp]

        public void Setup()
        {
            _userRepository = new UserRepository();

            _userService = new UserService(_userRepository);

        }
        /// <summary>
        /// Тестирует успешную регистрацию пользователя с корректными данными.
        /// </summary>
        [Test]

        public void RegistrationUsers_DataIsValid_Succes()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Password = "123456789",
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert 
            Assert.DoesNotThrow(action);

            var userDb = _userService.FindByEmail(userRegistrationData.Email);
            Assert.That(userDb, Is.Not.EqualTo(null));
            Assert.That(userDb.FirstName, Is.EqualTo("Lo"));
            Assert.That(userDb.LastName, Is.EqualTo("Po"));

            _userService.DeleteById(userDb.Id);

            Assert.Catch<UserNotFoundException>(() => _userService.FindByEmail(userRegistrationData.Email));

        }
        /// <summary>
        ///  Тестирует регистрацию пользователя с пустым именем.
        /// </summary>
        [Test]

        public void RegistrationUsers_FirstNameIsEmpty_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = string.Empty,
                Lastname = "Po",
                Password = "123456789",
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        /// Тестирует регистрацию пользователя с null в имени.
        /// </summary>
        [Test]

        public void RegistrationUsers_FirstNameIsNull_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = null,
                Lastname = "Po",
                Password = "123456789",
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        /// Тестирует регистрацию пользователя с пустой фамилией.
        /// </summary>
        [Test]

        public void RegistrationUsers_LastNameIsEmpty_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = string.Empty,
                Password = "123456789",
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        ///  Тестирует регистрацию пользователя с null в фамилии.
        /// </summary>
        [Test]

        public void RegistrationUsers_LastNameIsNull_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = null,
                Password = "123456789",
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        /// Тестирует регистрацию пользователя с пустым адресом электронной почты.
        /// </summary>
        [Test]

        public void RegistrationUsers_EmailIsEmpty_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Password = "123456789",
                Email = string.Empty
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        /// Тестирует регистрацию пользователя с null в адресе электронной почты.
        /// </summary>
        [Test]

        public void RegistrationUsers_EmailIsNull_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Password = "123456789",
                Email = null
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        /// Тестирует регистрацию пользователя с пустым паролем.
        /// </summary>
        [Test]

        public void RegistrationUsers_PasswordIsEmpty_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Password = string.Empty,
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        /// Тестирует регистрацию пользователя с null в пароле.
        /// </summary>
        [Test]

        public void RegistrationUsers_PasswordIsNull_ThrowException()
        {
            //Arrange
            UserRegistrationData userRegistrationData = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Password = null,
                Email = "jo@gmail.com"
            };


            //Act
            TestDelegate action = () => _userService.Registration(userRegistrationData);

            //Assert
            Assert.Catch<ArgumentException>(action);

        }
        /// <summary>
        ///  Тестирует регистрацию пользователя с уже существующим адресом электронной почты.
        /// </summary>
        [Test]

        public void RegistrationUsers_UserAlreadyRegistered_ThrowException() 
        {
            //Arrange
            UserRegistrationData userRegistrationData1 = new UserRegistrationData()
            {
                Firstname = "Lo",
                Lastname = "Po",
                Password = "123456789",
                Email = "jo@gmail.com"
            };

            
            UserRegistrationData userRegistrationData2 = new UserRegistrationData()
            {
                Firstname = "Lu",
                Lastname = "Pu",
                Password = "123456789",
                Email = "jo@gmail.com"
            };

            //Act
            TestDelegate actionSucces = () => _userService.Registration(userRegistrationData1);
            TestDelegate actionFail = () => _userService.Registration(userRegistrationData2);

            //Assert
            Assert.DoesNotThrow(actionSucces);
            var user = _userService.FindByEmail(userRegistrationData1.Email);
            Assert.Catch<ArgumentException>(actionFail);
            _userService.DeleteById(user.Id);
            Assert.Catch<UserNotFoundException>(()=>_userService.FindByEmail(userRegistrationData1.Email));

        }
    }
}
