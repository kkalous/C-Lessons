using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;
using System;

namespace MoneyBoxTest
{
    class Tests
    {
        [Test]
        public void WithdrawMoneyNotEnoughMoney()
        {
            var fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccountRepository.Setup(x => x.GetAccountById(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"))).Returns(new Account());
            var fakeNotificationService = new Mock<INotificationService>();
            var _transferMoney = new TransferMoney(fakeAccountRepository.Object, fakeNotificationService.Object);
            var _withdrawMoney = new WithdrawMoney(fakeAccountRepository.Object, fakeNotificationService.Object);            

            var ex = Assert.Throws<InvalidOperationException>(() => _withdrawMoney.Execute(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"), 123m));
            Assert.That(ex.Message, Is.EqualTo("Insufficient funds to withdraw money"));
        }

        [Test]
        public void WithdrawMoneySuccess()
        {
            User testUser = new User()
            {
                Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"),
                Name = "Kamila",
                Email = "email@email.com"
            };

            Account testAccount = new Account()
            {
                Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"),
                User = testUser,
                Balance = 35467,
                Withdrawn = 0,
                PaidIn = 0
            };
           

            var fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccountRepository.Setup(x => x.GetAccountById(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"))).Returns(testAccount);
            fakeAccountRepository.Setup(x => x.Update(testAccount)).Callback(() => testAccount.Balance = 35467 - 123);

            var fakeNotificationService = new Mock<INotificationService>();
            var _transferMoney = new TransferMoney(fakeAccountRepository.Object, fakeNotificationService.Object);
            var _withdrawMoney = new WithdrawMoney(fakeAccountRepository.Object, fakeNotificationService.Object);

            _withdrawMoney.Execute(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"), 123);
            Assert.AreEqual(testAccount.Balance, 35467 - 123);

        }

        [Test]
        public void TransferMoneyNotEnoughMoney()
        {
            var fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccountRepository.Setup(x => x.GetAccountById(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"))).Returns(new Account());
            fakeAccountRepository.Setup(x => x.GetAccountById(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3302"))).Returns(new Account());
            var fakeNotificationService = new Mock<INotificationService>();
            var _transferMoney = new TransferMoney(fakeAccountRepository.Object, fakeNotificationService.Object);
            var _withdrawMoney = new WithdrawMoney(fakeAccountRepository.Object, fakeNotificationService.Object);


            var ex = Assert.Throws<InvalidOperationException>(() => _transferMoney.Execute(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"), Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3302"), 123m));
            Assert.That(ex.Message, Is.EqualTo("Insufficient funds to make transfer"));
        }

        [Test]
        public void TransferMoneySuccess()
        {
            User testUser = new User()
            {
                Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"),
                Name = "Kamila",
                Email = "email@email.com"
            };

            Account testAccount1 = new Account()
            {
                Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"),
                User = testUser,
                Balance = 35467,
                Withdrawn = 0,
                PaidIn = 0
            };

            Account testAccount2 = new Account()
            {
                Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3302"),
                User = testUser,
                Balance = 35467,
                Withdrawn = 0,
                PaidIn = 0
            };


            var fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccountRepository.Setup(x => x.GetAccountById(testAccount1.Id)).Returns(testAccount1);
            fakeAccountRepository.Setup(x => x.GetAccountById(testAccount2.Id)).Returns(testAccount2);
            fakeAccountRepository.Setup(x => x.Update(testAccount1)).Callback(() => testAccount1.Balance = 35467 - 123);
            fakeAccountRepository.Setup(x => x.Update(testAccount2)).Callback(() => testAccount2.Balance = 35467 + 123);

            var fakeNotificationService = new Mock<INotificationService>();
            var _transferMoney = new TransferMoney(fakeAccountRepository.Object, fakeNotificationService.Object);
            var _withdrawMoney = new WithdrawMoney(fakeAccountRepository.Object, fakeNotificationService.Object);

            _transferMoney.Execute(testAccount1.Id, testAccount2.Id, 123);
            Assert.AreEqual(testAccount1.Balance, 35467 - 123);
            Assert.AreEqual(testAccount2.Balance, 35467 + 123);

        }
    }
}
