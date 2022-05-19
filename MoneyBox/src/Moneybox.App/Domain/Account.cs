using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public bool HasInsufficientFunds(decimal amount)
        {
            if (Balance - amount >= 0)
            {
                return true;
            }

            return false;
        }

        public bool HasLowBalance()
        {
            if (Balance < 500m)
            {
                return true;
            }

            return false;
        }

        public bool ReachedPaidInLimit(decimal amount)
        {
            if(PaidIn + amount > PayInLimit)
            {
                return true;                
            }
            return false;
        }

    }
}
