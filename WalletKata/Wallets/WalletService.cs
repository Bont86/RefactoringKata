using System.Collections.Generic;
using WalletKata.Exceptions;
using WalletKata.Interfaces.Wallets;
using WalletKata.Users;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        private readonly IWalletDAO _walletDao;

        public WalletService(IWalletDAO walletDao)
        {
            _walletDao = walletDao;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            List<Wallet> walletList = new List<Wallet>();
            User loggedUser = UserSession.GetInstance().GetLoggedUser();
            bool isFriend = false;

            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    walletList = _walletDao.FindWalletsByUser(user);
                }

                return walletList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}