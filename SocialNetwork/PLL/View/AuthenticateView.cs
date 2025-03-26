using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;


namespace SocialNetwork.PLL.View
{
    public class AuthenticateView
    {
        private readonly IUserService _userServices;

        private readonly UserMenuView _userMenuView;

        public AuthenticateView(IUserService userService, IMessageService messageService,IFriendService friendService)
        {
            _userServices = userService;

            _userMenuView = new UserMenuView(userService, messageService,friendService);

        }

        public void Authenticate()
        {
            Console.WriteLine("Напишите свою электронную почту:");
            string email = Console.ReadLine();

            Console.WriteLine("Напиште пароль");
            string password = Console.ReadLine();

            try
            {
                var user = _userServices.Authenticate(new UserAuthenticationData()
                {
                    Email = email,
                    Password = password
                });
                SuccessMessage.Show("Добро пожаловать в систему!");

                _userMenuView.Show(user);
            }
            catch (UserNotFoundException ex) 
            { 
                AlertMessage.Show(ex.Message); 
            }
            catch (WrongPasswordException)
            {
                AlertMessage.Show("Указан не верный пароль!");
            }
            catch(Exception) 
            {
                AlertMessage.Show("Что то сломалось!");
            }
        }
    }
}
