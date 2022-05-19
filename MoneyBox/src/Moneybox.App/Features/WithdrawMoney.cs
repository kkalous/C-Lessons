using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Domain;
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

            if (!from.HasInsufficientFunds(amount))
                throw new InvalidOperationException($"Insufficient funds to withdraw money");

            from.Balance -= amount;
            from.Withdrawn -= amount;

            if(from.HasLowBalance())
                this.notificationService.NotifyFundsLow(from.User.Email);

            this.accountRepository.Update(from);
        }
       
    }
}
