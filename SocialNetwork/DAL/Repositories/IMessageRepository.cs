using SocialNetwork.DAL.Entities;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Repositories
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Метод добавления сообщения в БД
        /// </summary>
        /// <param name="messageEntity"></param>
        /// <returns></returns>

        int Create(MessageEntity messageEntity);

        /// <summary>
        /// Метод выборки из БД сообщения по ид отправителя
        /// </summary>
        /// <param name="senderId"></param>
        /// <returns></returns>

        IEnumerable<MessageEntity> FindBySenderid(int senderId);

        /// <summary>
        /// Метод выборки из БД сообщения по ид получателя и по ид отправителя
        /// </summary>
        /// <param name="recipientId"></param>
        /// <returns></returns>

        IEnumerable<MessageEntity> FindByRecipientId(int recipientId,int senderId);

        /// <summary>
        /// Метод удаления сообщения из БД
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>

        int DeleteById(int messageId);

        /// <summary>
        /// Метод выборки из БД сообщения по ид получателя
        /// </summary>
        /// <param name="recipientId"></param>
        /// <returns></returns>
        IEnumerable<MessageEntity> FindBySenderIdOutcoming(int recipientId);
    }
}
