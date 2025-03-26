using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public int Create(UserEntity userEntity)
        {
            return Execute(@"insert into users (firstname,lastname,password,email) values (:Firstname,:Lastname,:Password,:Email)", userEntity);
        }

        public int Delete(int id)
        {
            return Execute(@"delete from users where id = :Id_p",new {Id_p = id });
        }

        public IEnumerable<UserEntity> FindAll()
        {
            return Query<UserEntity>("select * from users");
        }

        public UserEntity FindByEmail(string email)
        {
            return QueryFirstOrDefault<UserEntity>("select * from users where email = :Email_p", new { Email_p = email });
        }

        public UserEntity FindById(int id)
        {
            return QueryFirstOrDefault<UserEntity>("select * from users where id = :Id_p", new { Id_p = id });
        }

        public int Update(UserEntity userEntity)
        {
            return Execute(@"update users set firstname = :Firstname , lastname = :Lastname , password = :Password , email = :Email,
                              photo = :Photo , favorite_movie = :FavoriteMovie , favorite_book = :FavoriteBook",userEntity);
        }
    }
}
