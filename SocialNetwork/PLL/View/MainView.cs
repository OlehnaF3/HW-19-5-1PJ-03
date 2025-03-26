using SocialNetwork.BLL.Services;
using System;

namespace SocialNetwork.PLL.View
{
    public class MainView
    {
        private readonly AuthenticateView _authenticateView;

        private readonly RegistrationView _registrationView;


        public MainView(IUserService userService,IMessageService messageService,IFriendService friendService)
        {
            _authenticateView = new AuthenticateView(userService,messageService, friendService);

            _registrationView = new RegistrationView(userService);
        }

        public void Start()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("Нажмите 1 для входа в сеть");
            Console.WriteLine("Нажмите 2 для регистрации в сети");
            Console.WriteLine();
            string key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    _authenticateView.Authenticate();
                    break;
                case "2":
                    _registrationView.Registration();
                    break;

            }
        }
    }
}
