using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WalletKata.Exceptions;
using WalletKata.Interfaces.Users;
using WalletKata.Interfaces.Wallets;
using WalletKata.Users;
using WalletKata.Wallets;

namespace WalletKata.Test
{
    [TestClass]
    public class WalletServiceTest
    {
        private Mock<IWalletDAO> _walletDaoMock;
        private Mock<IUserSession> _userSessionMock;

        private WalletService _service;

        [TestInitialize]
        public void Initialize()
        {
            _walletDaoMock = new Mock<IWalletDAO>();
            _userSessionMock = new Mock<IUserSession>();

            _service = new WalletService(_walletDaoMock.Object, _userSessionMock.Object);
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
            // and currently, the code throw a NullReferenceException, if user is logged in, which is not enough specific.
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
            var paramUser = new User();
            _walletDaoMock
                .Setup(dao => dao.FindWalletsByUser(It.IsAny<User>()))
                .Returns(new List<Wallet> { new Wallet { } });
            _userSessionMock
                .Setup(s => s.GetLoggedUser())
                .Returns(new User());

            // Act
            var result = _service.GetWalletsByUser(paramUser);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetWalletsByUser_ShouldReturnCorrectListOfWallets()
        {
            // Arrange
            var paramUser = new User();
            var expectedWallets = new List<Wallet> { new Wallet { } };
            _walletDaoMock
                .Setup(dao => dao.FindWalletsByUser(It.IsAny<User>()))
                .Returns(expectedWallets);
            var loggedUser = new User();
            paramUser.Friends.Add(loggedUser);
            _userSessionMock
                .Setup(s => s.GetLoggedUser())
                .Returns(loggedUser);


            // Act
            var result = _service.GetWalletsByUser(paramUser);

            // Assert
            Assert.AreEqual(expectedWallets, result);
        }
    }
}
