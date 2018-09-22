using System.Collections.Generic;

namespace WalletKata.Users
{
    public class User
    {
        public List<User> Friends { get; private set; } = new List<User>();
    }
}