using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;

namespace SocialNetwork.PLL.View
{
    public class FriendListView
    {
        private readonly IFriendService _friendService;
        public FriendListView(IFriendService friendService)
        {
            _friendService = friendService;
        }

        public void Show(int id)
        {
            try
            {
                var list = _friendService.GetFriendsListById(id);

                SuccessMessage.Show("Список друзей!");

                foreach (var friend in list)
                {
                    Console.WriteLine("\tИд: {0},\n\t\tЭлектронная почта: {1},\n\t\tИмя пользователя: {2}",friend.Id,friend.EmailFriend,friend.NameFriend);
                }

            }
            catch (Exception ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
