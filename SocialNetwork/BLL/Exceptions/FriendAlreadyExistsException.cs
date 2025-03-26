using System;

namespace SocialNetwork.BLL.Exceptions
{
    public class FriendAlreadyExistsException : Exception
    {
        public override string Message { get; } = "Пользователь уже находится в вашем списке друзей!";
    }
}
