using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.PLL.View;
using System;
using System.Text;

namespace SocialNetwork
{
    public class Program
    {

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            IUserRepository userRepository = new UserRepository();
            IMessageRepository messageRepository = new MessageRepository();
            IFriendRepository friendRepository = new FriendRepository();
            IFriendService friendService = new FriendService(userRepository, friendRepository);
            IUserService userService = new UserService(userRepository);
            IMessageService messageService = new MessageService(messageRepository,userRepository);
            MainView mainView = new MainView(userService,messageService, friendService);
            while(true)
            {
                mainView.Start();
            }
        }
    }
}