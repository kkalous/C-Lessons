

using System;

namespace PnlUploadFromDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _service = new Service();

            var resultsCapital = _service.GetPnlCapital("capital", @"C:\Users\Kamila\Desktop\C# projects\CSharpJames\Gsa\capital.csv");
            var resultsPnl = _service.GetPnlCapital("pnl", @"C:\Users\Kamila\Desktop\C# projects\CSharpJames\Gsa\pnl.csv");

            Console.WriteLine("Loaded Capitals and Pnls!");

            _service.SavePnlCapital(resultsPnl, resultsCapital);

            Console.WriteLine("Finished saving to DB!");
        }
    }      
}


  


