using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private readonly IAccountRepository accountRepository;
        private readonly INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }
        
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            if (!from.HasInsufficientFunds(amount))
                throw new InvalidOperationException($"Insufficient funds to make transfer");

            if (from.HasLowBalance())
                this.notificationService.NotifyFundsLow(from.User.Email);

            if (to.ReachedPaidInLimit(amount))
                throw new InvalidOperationException("Account pay in limit reached");

            var paidIn = to.PaidIn + amount;
            if (Account.PayInLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }

            from.Balance -= amount;
            from.Withdrawn -= amount;

            to.Balance += amount;
            to.PaidIn += amount;

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
