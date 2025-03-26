using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;

namespace SocialNetwork.PLL.View
{
    public class FriendAddView
    {
        private readonly IFriendService _friendService;
        public FriendAddView(IFriendService friendService)
        {
            _friendService = friendService;
        }
        public void Show(int userId)
        {
            Console.WriteLine("Введите электронную почту пользователя которого хотите добавить в друзья");
            Console.WriteLine();
            string email = Console.ReadLine();
            FriendAddData data = new FriendAddData()
            {
                Email_Friend = email,
                User_Id = userId
            };
            try
            {
                _friendService.AddFriend(data);
                SuccessMessage.Show($"Пользователь с электронной почтой {data.Email_Friend}");
                SuccessMessage.Show("Добавлен в список друзей!");
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }
            catch (FriendAlreadyExistsException ex)
            {
                AlertMessage.Show(ex.Message);
            }
            catch (Exception)
            {
                AlertMessage.Show("Проверьте правильность вводимых данных!");
            }
        }
    }
}
