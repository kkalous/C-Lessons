using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace PhoneBookWithDb
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext() : base("myConnectionString")
        {
        }
    }
}