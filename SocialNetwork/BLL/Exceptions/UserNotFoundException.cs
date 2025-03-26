using System;

namespace SocialNetwork.BLL.Exceptions
{
    /// <summary>
    /// Класс исключения если пользователя не нашло в БД
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public override string Message { get; } = "Пользователь не найден в системе!";

    }
}
