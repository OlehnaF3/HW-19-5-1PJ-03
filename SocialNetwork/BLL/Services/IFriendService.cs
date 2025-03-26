using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public interface IFriendService
    {
        /// <summary>
        ///Метод добавления пользователя в список друзей
        /// </summary>
        /// <param name="friendAddData"></param>

        void AddFriend(FriendAddData friendAddData);

        /// <summary>
        /// Метод удаления пользователя из списка друзей
        /// </summary>
        /// <param name="id"></param>

        void DeleteFriend(string email,int userId);

        /// <summary>
        /// Метод получения списка друзей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        IEnumerable<FriendOutputData> GetFriendsListById(int id);

        FriendsEntity ContructFriendEntity(int friendId,int userId);


    }
}
