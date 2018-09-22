using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WalletKata.Exceptions;
using WalletKata.Users;
using WalletKata.Wallets;

namespace WalletKata.Test
{
    [TestClass]
    public class WalletServiceTest
    {
        private WalletService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new WalletService();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWalletsByUser_ShouldThrowWhenNoUserIsGivenInParameter()
        {
            // Arrange

            // Act
            _service.GetWalletsByUser(null);

            // Assert
            // Expected exception
            // Maybe we do not want this behavior, but there is no spec about this,
            // and currently, the code throw a UserNotLoggedInException, which do not match to this case.
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void GetWalletsByUser_ShouldThrowWhenUserIsNotLoggedIn()
        {
            // Arrange
            var paramUser = new User();

            // Act
            _service.GetWalletsByUser(paramUser);

            // Assert
            // Expected exception
        }


        [TestMethod]
        public void GetWalletsByUser_ShouldReturnEmptyListWhenUserIsNotFriendWithParamUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail("This is not implemented yet");
        }

        [TestMethod]
        public void GetWalletsByUser_ShouldReturnCorrectListOfWallets()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail("This is not implemented yet");
        }
    }
}
