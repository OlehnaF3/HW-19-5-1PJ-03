using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Linq;

namespace SocialNetwork.PLL.View
{
    public class MessageIncomingView
    {
        private readonly IMessageService _messageService;

        public MessageIncomingView(IMessageService messageService)
        {

            _messageService = messageService;
        }

        public void ShowMessagesListByIdSender(User user)
        {
            try
            {
                var messages = _messageService.GetMessagesBySenderId(user.Id);

                foreach (var message in messages)
                {
                    Console.WriteLine("\t\nИд сообщения: {0},\t\nОтправитель: {1},\t\nТекст сообщения: {2}", message.Id, message.Sender, message.Content);
                }
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
