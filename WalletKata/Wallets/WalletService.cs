using System.Collections.Generic;
using WalletKata.Exceptions;
using WalletKata.Interfaces.Users;
using WalletKata.Interfaces.Wallets;
using WalletKata.Users;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        private readonly IWalletDAO _walletDao;
        private readonly IUserSession _userSession;

        public WalletService(IWalletDAO walletDao, IUserSession userSession)
        {
            _walletDao = walletDao;
            _userSession = userSession;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            List<Wallet> walletList = new List<Wallet>();
            User loggedUser = _userSession.GetLoggedUser();
            bool isFriend = false;

            if (loggedUser != null)
            {
                foreach (var friend in user.Friends)
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