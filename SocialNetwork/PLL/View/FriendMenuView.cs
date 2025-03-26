using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;

namespace SocialNetwork.PLL.View
{
    public class FriendMenuView
    {
        private readonly FriendAddView _friendAddView;

        private readonly FriendListView _friendListView;

        private readonly FriendDeleteView _friendDeleteView;
        public FriendMenuView(IFriendService friendService,IUserService userService)
        {
            _friendAddView = new FriendAddView(friendService);

            _friendDeleteView = new FriendDeleteView(friendService,userService);

            _friendListView = new FriendListView(friendService);
        }

        public void Show(User user)
        {
            while (true)
            {
                Console.WriteLine("Для просмотра списка друзей нажмите 1");
                Console.WriteLine("Для добавления в список друзей нажмите 2");
                Console.WriteLine("Для удаления из списка друзей нажмите 3");
                Console.WriteLine("Для выхода из меню нажмите 4");
                string key = Console.ReadLine();

                if (key == "4") return;

                switch (key)
                {
                    case "1":
                        _friendListView.Show(user.Id);
                        break;
                    case "2":
                        _friendAddView.Show(user.Id);
                        break;
                    case "3":
                        _friendDeleteView.Show(user.Id);
                        break;

                }
            }
        }
    }
}
