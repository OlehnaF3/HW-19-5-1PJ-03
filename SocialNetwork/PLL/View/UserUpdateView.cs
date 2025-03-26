using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;

namespace SocialNetwork.PLL.View
{
    public class UserUpdateView
    {
        private readonly IUserService _userServices;

        public UserUpdateView(IUserService userService)
        {
            _userServices = userService;
        }

        public void Update(User user)
        {
            try
            {

                Console.WriteLine("Напишите свое новое имя:");
                user.FirstName = Console.ReadLine();

                Console.WriteLine("Напишите свою новую фамилию:");
                user.LastName = Console.ReadLine();

                Console.WriteLine("Напишите любимую книгу:");
                user.FavoriteBook = Console.ReadLine();

                Console.WriteLine("Напишите любимый фильм:");
                user.FavoriteMovie = Console.ReadLine();
                
                _userServices.Update(user);

                SuccessMessage.Show("Профиль успешно обновлен!");
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
