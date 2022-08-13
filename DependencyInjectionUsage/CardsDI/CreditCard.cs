using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDesignPattern
{
    interface ICreditCard
    {
        string GetCardType();
        int GetCreditLimit();
        int GetAnnualCharge();
    }
}
