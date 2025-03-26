using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Метод добавления пользователя в БД
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>

        int Create(UserEntity userEntity);

        /// <summary>
        /// Метод выборки из БД пользователя по електронной почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserEntity</returns>
 
        UserEntity FindByEmail(string email);

        /// <summary>
        /// Метод выборки из БД получения списка все пользователей
        /// </summary>
        /// <returns></returns>

        IEnumerable<UserEntity> FindAll();

        /// <summary>
        /// Метод выборки из БД пользователя по Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserEntity</returns>

        UserEntity FindById(int id);

        /// <summary>
        /// Метод обновления пользователя в БД! 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>

        int Update(UserEntity userEntity);

        /// <summary>
        /// Метод удаления пользователя из БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        int Delete(int id);
    }
}
