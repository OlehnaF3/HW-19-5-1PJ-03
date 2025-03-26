using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SocialNetwork.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;

        }

        public void CreateMessage(MessageSendData messageSendData)
        {
            if (messageSendData == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(messageSendData.Content))
                throw new ArgumentNullException();

            if (messageSendData.Content.Length > 5000)
                throw new ArgumentException();

            if (string.IsNullOrEmpty(messageSendData.EmailRecipient))
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(messageSendData.EmailRecipient))
                throw new ArgumentException("Invalid email format", nameof(messageSendData.EmailRecipient));

            var userRecipient = _userRepository.FindByEmail(messageSendData.EmailRecipient) ?? throw new UserNotFoundException();

            if (_messageRepository.Create(new MessageEntity()
            {
                Content = messageSendData.Content,
                Recipient_id = userRecipient.Id,
                Sender_id = messageSendData.SenderId,
            }) == 0) throw new Exception("Что то сломалось");

        }

        public IEnumerable<MessageOutData> GetMessagesByRecipient(string recipientEmail,int senderId) //Исходящие сообщения
        {
            if(recipientEmail == null)
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(recipientEmail))
                throw new ArgumentException();

            var recipient = _userRepository.FindByEmail(recipientEmail) 
                ?? throw new UserNotFoundException();

            var messages = _messageRepository.FindByRecipientId(recipient.Id,senderId);

            List<MessageOutData> result = new List<MessageOutData>();

            foreach(var message in messages)
            {
                result.Add(new MessageOutData()
                {
                    Id = message.Id,
                    Recipient = recipient.Email,
                    Sender =  _userRepository.FindById(message.Sender_id).Email,
                    Content = message.Content,
                });
            }

            return result;
        }


        public IEnumerable<MessageOutData> GetMessagesBySenderId(int senderId) //Входящие сообщения
        {
            if (senderId == 0) 
                throw new ArgumentException();   

            var messages = _messageRepository.FindBySenderid(senderId);

            List<MessageOutData> result = new List<MessageOutData>();
            foreach(var message in messages)
            {
                result.Add(new MessageOutData()
                {
                    Id = message.Id,
                    Recipient = _userRepository.FindById(message.Recipient_id).Email,
                    Sender = _userRepository.FindById(message.Sender_id).Email,
                    Content = message.Content,
                });
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException">Такого id нету</exception>

        public void DeleteById(int id)
        {
            if (_messageRepository.DeleteById(id) == 0)
                throw new ArgumentNullException();
        }

        public IEnumerable<MessageOutData> GetMessagesByRecipient(int recipientId) //Исходящие сообщения
        {
            if (recipientId == 0)
                throw new ArgumentNullException();

            var user = _userRepository.FindById(recipientId);
            var messages = _messageRepository.FindBySenderIdOutcoming(recipientId);


            List<MessageOutData> result = new List<MessageOutData>();

            foreach (var message in messages)
            {
                result.Add(new MessageOutData()
                {
                    Id = message.Id,
                    Recipient = user.Email,
                    Sender = _userRepository.FindById(message.Sender_id).Email,
                    Content = message.Content,
                });
            }

            return result;
        }
    }
}
