using WalletKata.Users;

namespace WalletKata.Interfaces.Users
{
    public interface IUserSession
    {
        User GetLoggedUser();
    }
}
