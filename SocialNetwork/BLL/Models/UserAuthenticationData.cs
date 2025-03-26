using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    /// <summary>
    /// Класс Аутентификация пользователя!
    /// 2 свойства Электронная почта и Пароль
    /// </summary>
    public class UserAuthenticationData
    {
        /// <summary>
        /// Свойство Электронной почты пользователя
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Свойство Пароля пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
