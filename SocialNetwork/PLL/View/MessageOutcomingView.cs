using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Linq;

namespace SocialNetwork.PLL.View
{
    internal class MessageOutcomingView
    {
        private readonly IMessageService _messageService;

        public MessageOutcomingView(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void ShowMessagasListByRecipient(int senderId)
        {

            Console.WriteLine("Укажите электронную почту получателя:");
            string email = Console.ReadLine();

            try
            {
                var messages = _messageService.GetMessagesByRecipient(email,senderId);

                foreach (var message in messages)
                {
                    Console.WriteLine("\t\nИд сообщения: {0},\t\nПолучатель: {1},\t\nSТекст сообщения: {2}", message.Id, message.Recipient, message.Content);
                }
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }

        }
    }
}
