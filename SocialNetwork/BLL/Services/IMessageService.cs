using SocialNetwork.BLL.Models;
using System.Collections.Generic;

namespace SocialNetwork.BLL.Services
{
    public interface IMessageService
    {
        void CreateMessage(MessageSendData messageSendData);

        IEnumerable<MessageOutData> GetMessagesByRecipient(string recipientEmail,int senderId);

        IEnumerable<MessageOutData> GetMessagesBySenderId(int senderId);

        void DeleteById(int id);

        IEnumerable<MessageOutData> GetMessagesByRecipient(int recipientId);
    }
}
