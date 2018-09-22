using WalletKata.Exceptions;
using WalletKata.Interfaces.Users;

namespace WalletKata.Users
{
    public class UserSession : IUserSession
    {
        public User GetLoggedUser()
        {
            throw new ThisIsAStubException("UserSession.IsUserLoggedIn() should not be called in an unit test");
        }
    }
}