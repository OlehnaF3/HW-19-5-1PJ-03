using System;

namespace SocialNetwork.BLL.Exceptions
{
    /// <summary>
    /// Класс исключения при попытке ввода пароля.
    /// Выпадает при неправильном вводе пароля
    /// </summary>
    public class WrongPasswordException : Exception
    {
        public override string Message { get; } = "Введен не правильный пароль";
    }
}
