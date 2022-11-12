using System;

namespace Multithreading2
{
    class BankAccount
    {
        // This bit is thread safe - Why?
        private decimal m_balance = 0.0M;
        private object m_balanceLock = new object();
        internal void Deposit(decimal delta)
        {
            //Safe because we are going to lock and check balance (one at the time)
            lock (m_balanceLock) { m_balance += delta; }
        }
        internal void Withdraw(decimal delta)
        {
            //Safe because we can Withdraw just one amount at the time 
            lock (m_balanceLock)
            {
                if (m_balance < delta)
                    throw new Exception("Insufficient funds");
                m_balance -= delta;
            }
        }
        // End This bit is thread safe

        // Proposed transfer method - why is it not safe?
        internal static void Transfer(
          BankAccount a, BankAccount b, decimal delta)
        {   
            //Withdraw(a, delta);
            //Deposit(b, delta);
        }

        //Note:
        //if the transfer will be from same accounts in opposite directions,  but at the same time?
        //Transfer withdraw 100 from a deposite b and at the same the other way
        //Transfer withdraw 100 from b and deposite a and at the same time the other way
        //it will get lock with the wrong balance 

    }
}

