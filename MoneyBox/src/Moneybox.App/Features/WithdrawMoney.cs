using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository accountRepository;
        private readonly INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);

            var balanceBefore = from.Balance;
            if (balanceBefore < amount)
            {
                throw new InvalidOperationException("You don't have enough money in your bankaccount!");
            }

            var balanceAfter = from.Balance - amount;
            if (balanceAfter < 500m)
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            from.Balance -= amount;
            from.Withdrawn -= amount;

            this.accountRepository.Update(from);
        }
    }
}
