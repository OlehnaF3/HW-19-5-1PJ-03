namespace SocialNetwork.BLL.Models
{
    public class User
    {
        /// <summary>
        /// Свойство Айди пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Свойство Имя пользователя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Свойство Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Свойство Пароль пользователя
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Свойство Электронная почта пользователя
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Свойство Фотография пользователя
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Свойство Любимого фильма пользователя
        /// </summary>
        public string FavoriteMovie { get; set; }
        /// <summary>
        /// Свойство Любимой книги пользователя
        /// </summary>
        public string FavoriteBook { get; set; }


        public int CountInputMessage { get; set; } = 0;

        public int CountOutputMessage { get; set; } = 0;

        public User(
            int id,
            string firstName,
            string lastName,
            string password,
            string email,
            string photo,
            string favoriteMovie,
            string favoriteBook)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Photo = photo;
            FavoriteMovie = favoriteMovie;
            FavoriteBook = favoriteBook;

        }
    }
}
