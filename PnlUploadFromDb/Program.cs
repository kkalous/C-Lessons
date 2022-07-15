using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using PnlData.Scaffolded;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnlUploadFromDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var localConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            List<Pnl> query;
            using (var db = new Context())
            {
                query = db.Pnls
                    .OrderBy(x => x.Date)
                    .ToList();
            }

            Console.WriteLine($"Loaded {query.Count()} rows");


        }
    }

  

}
