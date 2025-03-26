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
    public class FriendService : IFriendService
    {
        private readonly IUserRepository _userRepository;

        private readonly IFriendRepository _friendRepository;

        public FriendService(IUserRepository userRepository, IFriendRepository friendRepository)
        {
            _userRepository = userRepository;

            _friendRepository = friendRepository;

        }

        public void AddFriend(FriendAddData friendData)
        {

            if (friendData == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(friendData.Email_Friend))
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(friendData.Email_Friend))
                throw new ArgumentException();

            var friend = _userRepository.FindByEmail(friendData.Email_Friend)
                ?? throw new UserNotFoundException();

            if (_friendRepository.FindFriendsByFriendId(friend.Id, friendData.User_Id) != null)
                throw new FriendAlreadyExistsException();

            var friendsEntity = ContructFriendEntity(friend.Id,friendData.User_Id);

            if (_friendRepository.Create(friendsEntity) == 0)
                throw new Exception("Что то сломалось ;(");

        }

        public FriendsEntity ContructFriendEntity(int frinedId, int userId)
        {
            return new FriendsEntity()
            {
                Friend_id = frinedId,
                User_id = userId
            };
        }

        public void DeleteFriend(string email,int userId)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();
            var friend = _userRepository.FindByEmail(email)
                ?? throw new UserNotFoundException();

            var friendEntity = _friendRepository.FindFriendsByFriendId(friend.Id,userId);

            if (_friendRepository.Delete(friendEntity.Id) == 0)
                throw new Exception("Друга под таким ид нету");
        }

        public IEnumerable<FriendOutputData> GetFriendsListById(int id)
        {
            var listFriends = _friendRepository.FindAllByUserId(id);

            if (listFriends.Count() == 0) 
                throw new Exception("Список друзей пустой");

            List<FriendOutputData> friends = new List<FriendOutputData>();
            foreach (var obj in listFriends)
            {
                var _ = _userRepository.FindById(obj.Friend_id);
                friends.Add(new FriendOutputData()
                {
                    Id = obj.Id,
                    EmailFriend = _.Email,
                    NameFriend = _.Firstname
                });
            }

            return friends;
        }
    }
}
