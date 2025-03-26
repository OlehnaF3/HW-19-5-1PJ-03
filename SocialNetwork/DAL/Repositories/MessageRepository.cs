using SocialNetwork.DAL.Entities;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public int Create(MessageEntity messageEntity)
        {
            return Execute("insert into messages(content, sender_id, recipient_id) values (:Content,:Sender_id,:Recipient_id)", messageEntity);
        }

        public int DeleteById(int messageId)
        {
            return Execute("delete from messages where id = :Message_Id_p", new { Message_Id_p = messageId }); //Окончание _p значит, что это параметр для sql
        }

        public IEnumerable<MessageEntity> FindByRecipientId(int recipientId, int senderId)
        {
            return Query<MessageEntity>("select * from messages  where recipient_id = :Recipient_Id_p and sender_id = :Sender_Id_p", new { Recipient_Id_p = recipientId, Sender_Id_p = senderId });
        }

        public IEnumerable<MessageEntity> FindBySenderIdOutcoming(int senderId)
        {
            return Query<MessageEntity>("select * from messages where sender_id = :Sender_Id_p", new { Sender_Id_p = senderId });
        }

        public IEnumerable<MessageEntity> FindBySenderid(int senderId)
        {
            return Query<MessageEntity>("select * from messages where recipient_id = :Sender_Id_p", new { Sender_Id_p = senderId });
        }
    }
}
