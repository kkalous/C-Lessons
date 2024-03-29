﻿using GSASolution.Outputer;
using System;

namespace GSASolution
{
    // Move out other code into a core library that can be shared by this and the web proj
    class Program
    {
        static void Main(string[] args)
        {
            ////Load data into strategies
            //DatabaseSeeder seeder = new DatabaseSeeder(@"C:\Users\Kamila\Desktop\C# projects\C-Lessons\Files", new CsvInputer());
            //seeder.SeedStrategies();

            GSAService _outputer = new GSAService(new PnlService());


            string returnValue = InsertReturnValue();

            string command;

            var endApp = "N";

            while (endApp.ToUpper() == "N")
            {
                switch (returnValue.ToUpper())
                {
                    case "CA":
                        command = InsertCommand();
                        foreach (var line in _outputer.GetCapitalsInfo(command))
                        {
                            var rslt = @$"{line.Strategy.StrategyName}, date:{line.Date}, capital:{line.Amount}";
                            Console.WriteLine(rslt);
                        };                        
                        break;
                    case "PL":
                        command = InsertRegion();
                        Console.WriteLine(@$"Cumulative Pnl for region {command.ToUpper()}");

                        foreach (var line in _outputer.GetCumulativePnlsInfo(command))
                        {  
                            var rslt = @$"date:{line.Date}, capital:{line.Amount}";

                            Console.WriteLine(rslt);
                        };                       
                        break;
                    default:
                        Console.WriteLine("Command not recognized!");
                        break;
                }

                Console.WriteLine("Finished? Write 'N' to continue or 'Y' to close the app");
                endApp = Console.ReadLine();

                if (endApp.ToUpper() == "N")
                {
                    returnValue = InsertReturnValue();
                }
            }

        }

        private static string InsertCommand()
        {
            Console.WriteLine("Insert your command (e.g. Strategy1, Strategy2, Strategy15):");
            var command = Console.ReadLine();

            return command;
        }

        private static string InsertRegion()
        {
            Console.WriteLine("Insert your region (e.g. EU):");
            var command = Console.ReadLine();

            return command;
        }

        private static string InsertReturnValue()
        {
            Console.WriteLine("Choose a return value:\n For Capitals write 'CA' \n For Cumulative P&Ls write 'PL'\n");
            var value = Console.ReadLine();

            return value;
        }
    }
}
