using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.View
{
    public class MessagesAllScoreView
    {                     
            
      private readonly IMessageService _messageService;

        public MessagesAllScoreView(IMessageService messageService)
        {

            _messageService = messageService;
        }

        public int ShowScoreMessagesIncoming(User user)
        {
            try
            {
               return _messageService.GetMessagesBySenderId(user.Id).Count();
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }
            return 0;
        }

        public int ShowScoreMessagesOutcoming(User user)
        {
            try
            {
                return _messageService.GetMessagesByRecipient(user.Id).Count();

            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show(ex.Message);
            }
            return 0;
        }
    }
}
