using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Runtime.InteropServices;

namespace SocialNetwork.PLL.View
{
    public class UserMenuView
    {
        private readonly UserProfileView _userProfileView;

        private readonly MessageIncomingView _messageIncomingView;

        private readonly MessageOutcomingView _messageOutcomingView;

        private readonly UserUpdateView _userUpdateView;

        private readonly MessageSendView _sendMessageView;

        private readonly FriendMenuView _friendMenuView;

        private readonly MessagesAllScoreView _messagesAllScoreView;
        public UserMenuView(IUserService userService,IMessageService messageService,IFriendService friendService)
        {
            _userUpdateView = new UserUpdateView(userService);

            _userUpdateView = new UserUpdateView(userService);

            _sendMessageView = new MessageSendView(messageService);

            _messageIncomingView = new MessageIncomingView(messageService);

            _messageOutcomingView = new MessageOutcomingView(messageService);

            _userProfileView = new UserProfileView();

            _friendMenuView = new FriendMenuView(friendService, userService);

            _messagesAllScoreView = new MessagesAllScoreView(messageService);
        }


        public void Show(User user)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("У вас входящих сообщений: {0}",_messagesAllScoreView.ShowScoreMessagesIncoming(user));
                Console.WriteLine("У вас исходящих сообщений: {0}",_messagesAllScoreView.ShowScoreMessagesOutcoming(user));
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("Нажмите на 1 для получения информации о профиле");
                Console.WriteLine("Нажмите на 2 для изменения профиля");
                Console.WriteLine("Нажмите на 3 для Отправки сообщения");
                Console.WriteLine("Нажмите на 4 для получения всех входящих сообщений");
                Console.WriteLine("Нажмите на 5 для получения всех исходящих сообщений");
                Console.WriteLine("Нажмите на 6 переход в меню упралением друзьями");
                Console.WriteLine("Нажмите на 7 что бы выйти из сети!");
                Console.WriteLine();


                string key = Console.ReadLine();

                if (key == "7") break;
                switch (key)
                {

                    case "1":
                        _userProfileView.ShowProfile(user);
                        break;

                    case "2":
                        _userUpdateView.Update(user);
                        break;

                    case "3":
                        _sendMessageView.Create(user.Id);
                        break;

                    case "4":
                        _messageIncomingView.ShowMessagesListByIdSender(user);
                        break;

                    case "5":
                        _messageOutcomingView.ShowMessagasListByRecipient(user.Id);
                        break;

                    case "6":
                        _friendMenuView.Show(user);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
