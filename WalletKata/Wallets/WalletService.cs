using System;
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

        /// <summary>
        /// Returns wallets belonging to the given user.
        /// </summary>
        /// <param name="user">The user from which we want the wallets.</param>
        /// <returns>A list of wallets belonging to the given user, or an empty list if given user and current user are not friends.</returns>
        /// <exception cref="ArgumentNullException">User parameter is null.</exception>
        /// <exception cref="UserNotLoggedInException">User calling this method is not logged in.</exception>
        public List<Wallet> GetWalletsByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var loggedUser = _userSession.GetLoggedUser();
            if (loggedUser == null)
                throw new UserNotLoggedInException();

            if (!user.Friends.Contains(loggedUser))
                return new List<Wallet>();

            return _walletDao.FindWalletsByUser(user);
        }
    }
}