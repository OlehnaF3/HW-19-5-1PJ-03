using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;

namespace SocialNetwork.PLL.View
{
    public class UserProfileView
    {
        public void ShowProfile(User user)
        {
                Console.WriteLine("\tВаш ид в системе{0}", user.Id);
                Console.WriteLine("\tВаше имя {0}", user.FirstName);
                Console.WriteLine("\tВаша фамилия {0}", user.LastName);
                Console.WriteLine("\tВаша электронная почта {0}", user.Email);
                Console.WriteLine("\tВаш пароль {0}", user.Password);
                Console.WriteLine("\tВаш любимый фильм {0}", user.FavoriteMovie);
                Console.WriteLine("\tВаша любимая книга {0}", user.FavoriteBook);
        }
    }
}
