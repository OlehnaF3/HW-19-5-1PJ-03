using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using System;
using SocialNetwork.PLL.Helpers;
namespace SocialNetwork.PLL.View
{
    public class RegistrationView
    {
        private readonly IUserService _userServices;

        public RegistrationView(IUserService userService)
        {
            _userServices = userService;
        }

        public void Registration()
        {
            Console.WriteLine("Введите имя:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            Console.WriteLine("Введите электронную почту:");
            string email = Console.ReadLine();

            var user = new UserRegistrationData()
            {
                Firstname = firstName,
                Lastname = lastName,
                Password = password,
                Email = email
            };
            try
            {

                _userServices.Registration(user);
            }
            catch (ArgumentNullException)
            {
                AlertMessage.Show("Проверте написание полей!");
            }
            catch (InvalidOperationException)
            {
                AlertMessage.Show("При записи случилась ошибка!");
            }
            catch(Exception ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
