

using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.Services
{
    public interface IUserService
    {
        void Registration(UserRegistrationData userregistrationData);

        User Authenticate(UserAuthenticationData userAuthenticationData);

        User ConstructUserModel(UserEntity userEntity, int countInputMessages = 0, int countOutputMessages = 0);

        User FindByEmail(string email);

        User FindById(int id);

        void Update(User user);

        void DeleteById(int id);
    }


}
