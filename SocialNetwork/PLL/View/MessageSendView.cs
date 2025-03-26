using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;

namespace SocialNetwork.PLL.View
{
    public class MessageSendView
    {
        private readonly IMessageService _messageService;

        public MessageSendView(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Create(int id)
        {
            Console.WriteLine("Кому будем отправлять? Напишите электронную почту:");
            string email = Console.ReadLine();

            Console.WriteLine("Напишите текст сообщение:");
            string content = Console.ReadLine();
            if (content.Length > 5000) AlertMessage.Show("Сообщение должно быть меньше 5000 симолов");
            
            try
            {
                _messageService.CreateMessage(new MessageSendData()
                {
                    Content = content,
                    EmailRecipient = email,
                    SenderId = id
                });
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }

            catch(Exception) 
            {
                AlertMessage.Show("Что то сломалось!");
            }
        }
    }
}
