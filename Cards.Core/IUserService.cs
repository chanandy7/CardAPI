using Cards.Core.DTO;
using Cards.DB;


namespace Cards.Core
{
    public interface IUserService
    {
        //new user
        Task <AuthenticatedUser> SignUp(User user);
        
        
        //existing user
        Task<AuthenticatedUser> SignIn(User user);
    }
}
