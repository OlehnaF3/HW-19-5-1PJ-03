using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public interface IFriendRepository
    {

        /// <summary>
        /// Метод добавления друга в БД
        /// </summary>
        /// <param name="friendsEntity"></param>
        /// <returns></returns>

        int Create(FriendsEntity friendsEntity);

        /// <summary>
        /// Метод получения списка друзей из БД по user_id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        IEnumerable<FriendsEntity> FindAllByUserId(int userId);

        /// <summary>
        /// Метод удаления друга из БД по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        int Delete(int id);

        FriendsEntity FindFriendsByFriendId(int friendId,int userId);
    }
}
