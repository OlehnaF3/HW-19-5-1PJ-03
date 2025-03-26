using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public int Create(FriendsEntity friendsEntity)
        {
            return Execute("insert into friends (user_id,friend_id) values (:User_id,:Friend_id)",friendsEntity);
        }

        public int Delete(int id)
        {
            return Execute("delete from friends where id = :Id_p", new {Id_p =  id});
        }

        public IEnumerable<FriendsEntity> FindAllByUserId(int userId)
        {
            return Query<FriendsEntity>("select * from friends where user_id = :User_Id_p", new { User_Id_p = userId });
        }

        public FriendsEntity FindFriendsByFriendId(int friend_id,int user_id)
        {
            return QueryFirstOrDefault<FriendsEntity>("select * from friends where friend_id = :Friend_Id_p and user_id = :User_Id_p",new {Friend_Id_p = friend_id,User_Id_p = user_id});
        }
    }
}
