using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;

namespace SocialNetwork.PLL.View
{
    public class FriendDeleteView
    {
        private readonly IFriendService _friendService;

        private readonly IUserService _userService;
        public FriendDeleteView(IFriendService friendService, IUserService userService)
        {
            _friendService = friendService;
            _userService = userService;

        }

        public void Show(int userId)
        {
            Console.WriteLine("Напишите электронную почту пользователя");
            Console.WriteLine();
            string email = Console.ReadLine();
            try
            {
                _friendService.DeleteFriend(email,userId);
                SuccessMessage.Show($"Пользователь с электронной почтой {email}");
                SuccessMessage.Show("Удален из списка друзей");
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);

            }
            catch (ArgumentNullException)
            {
                AlertMessage.Show("Проверьте правильность вводимых данных!");
            }
            catch(Exception)
            {
                AlertMessage.Show("Что то сломалось!");
            }
        }
    }
}
