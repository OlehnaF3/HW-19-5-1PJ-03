using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Метод регистрации пользователя! 
        /// Инкапсулирует подключение в БД 
        /// </summary>
        /// <param name="userRegistrationData"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>

        public void Registration(UserRegistrationData userRegistrationData)
        {
            if (userRegistrationData == null)
                throw new ArgumentNullException(nameof(userRegistrationData));

            if (String.IsNullOrEmpty(userRegistrationData.Firstname))
                throw new ArgumentException("Имя не может быть null или пустой строкой");

            if (String.IsNullOrEmpty(userRegistrationData.Lastname))
                throw new ArgumentException("Фамилия не может быть null или пустой строкой");

            if (String.IsNullOrEmpty(userRegistrationData.Password))
                throw new ArgumentException("Пароль не может быть null или пустой строкой");

            if (String.IsNullOrEmpty(userRegistrationData.Email))
                throw new ArgumentException("Электронная почта не может null или пустой строкой");

            if (userRegistrationData.Password.Length < 8)
                throw new ArgumentException("Пароль должен быть больше 8 символов", nameof(userRegistrationData.Password));

            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
                throw new ArgumentException("Неверный формат электронной почты", nameof(userRegistrationData.Email));

            if (_userRepository.FindByEmail(userRegistrationData.Email) != null)
                throw new ArgumentException("Пользователь с такой электронной почтой уже есть", nameof(userRegistrationData.Email));

            UserEntity userEntity = new UserEntity()
            {
                Firstname = userRegistrationData.Firstname,
                Lastname = userRegistrationData.Lastname,
                Email = userRegistrationData.Email,
                Password = userRegistrationData.Password,
            };

            if (_userRepository.Create(userEntity) == 0)
            {
                throw new InvalidOperationException("Ошибка при создании пользователя");
            }
        }


        /// <summary>
        /// Метод аутентификации пользователя
        /// </summary>
        /// <param name="userAuthenticationData"></param>
        /// <returns>Сущность User</returns>
        /// <exception cref="UserNotFoundException">Кидает исключение если пользователя нету в БД</exception>
        /// <exception cref="WrongPasswordException">Кидает исключение если не правильно введен пароль</exception>

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            if (userAuthenticationData == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(userAuthenticationData.Password))
                throw new ArgumentException();

            if (string.IsNullOrEmpty(userAuthenticationData.Email))
                throw new ArgumentException();

            if (!new EmailAddressAttribute().IsValid(userAuthenticationData.Email))
                throw new ArgumentException();

            if (String.IsNullOrEmpty(userAuthenticationData.Email))
                throw new ArgumentNullException();
            var findUserEntity = _userRepository.FindByEmail(userAuthenticationData.Email) ?? throw new UserNotFoundException();

            if (findUserEntity.Password != userAuthenticationData.Password) throw new WrongPasswordException();

            //int countInputMessages = _messageRepository.FindBySenderid(findUserEntity.Id).Count();
            //int countOutputMessages = _messageRepository.FindByRecipientId(findUserEntity.Id).Count();

            return ConstructUserModel(findUserEntity);
        }

        /// <summary>
        /// Метод коструирования сущности User
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>

        public User ConstructUserModel(UserEntity userEntity, int countInputMessages = 0, int countOutputMessages = 0)
        {
            return new User(
               id: userEntity.Id,
               firstName: userEntity.Firstname,
               lastName: userEntity.Lastname,
               password: userEntity.Password,
               email: userEntity.Email,
               photo: userEntity.Photo,
               favoriteMovie: userEntity.FavoriteMovie,
               favoriteBook: userEntity.FavoriteBook);
            //{
            //    CountInputMessage = countInputMessages,
            //    CountOutputMessage = countOutputMessages
            //};
        }

        /// <summary>
        /// Метод получения пользователя по Электронной почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Сущность User</returns>
        /// <exception cref="UserNotFoundException"></exception>

        public User FindByEmail(string email)
        {
            var user = _userRepository.FindByEmail(email) ?? throw new UserNotFoundException();
            return ConstructUserModel(user);
        }
        /// <summary>
        /// Метод получения пользователя по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserNotFoundException"></exception>

        public User FindById(int id) => ConstructUserModel(_userRepository.FindById(id)) ?? throw new UserNotFoundException();

        /// <summary>
        /// Метод обновления пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception">Если обновленных строчек 0</exception>

        public void Update(User user)
        {
            var updateUserEntity = new UserEntity()
            {
                Id = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Password = user.Password,
                Email = user.Email,
                Photo = user.Photo,
                FavoriteMovie = user.FavoriteMovie,
                FavoriteBook = user.FavoriteBook
            };

            if (_userRepository.Update(updateUserEntity) == 0) throw new Exception("Что то сломалось при обновлении пользователя в бд.");
        }

        /// <summary>
        /// Метод удаления пользователя по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="UserNotFoundException"></exception>

        public void DeleteById(int id)
        {
            if (id == 0)
                throw new Exception();

            if (_userRepository.Delete(id) == 0) throw new UserNotFoundException();
        }

    }
}
