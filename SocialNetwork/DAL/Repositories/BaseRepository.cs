using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region
/*
 *Возмжно бд работает в оперативной памяти! Так как при закрытии приложения не сохраняет данные!
 *Очень странно реализованно подключение к бд без апп конфига
 *
 */
#endregion
namespace SocialNetwork.DAL.Repositories
{
    public class BaseRepository
    {
        protected T QueryFirstOrDefault<T>(string sql, object parameters = null) //Тут получаем 1 объект или дефолтное значение!
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected List<T> Query<T>(string sql, object parameters = null) // Тут получаем лист запроса! 
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query<T>(sql, parameters).ToList();
            }
        }

        protected int Execute(string sql, object parameters = null) //Метод возвращающий количество затронутых строк
        {
            using(var connection = CreateConnection())
            { 
                connection.Open();
                return connection.Execute(sql, parameters);
            }
        }

        private IDbConnection CreateConnection() //Открываем соединение с базой данных!
        {
            return new SQLiteConnection("Data Source = D:/New folder/SocialNetwork/DAL/DB/social_network_db.db ; Version = 3");
        }
    }
}
