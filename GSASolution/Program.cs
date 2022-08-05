using GSASolution.Outputer;
using System;

namespace GSASolution
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Load data into strategies
            //DatabaseSeeder seeder = new DatabaseSeeder(@"C:\Users\Kamila\Desktop\C# projects\C-Lessons\Files", new CsvInputer());
            //seeder.SeedStrategies();

            CsvOutputer _outputer = new CsvOutputer();

            string returnValue = InsertReturnValue();

            var command = InsertCommand();

            var endApp = "N";

            while (endApp.ToUpper() == "N")
            {
                switch (returnValue.ToUpper())
                {
                    case "CA":
                        foreach(var line in _outputer.GetCapitalsInfo(command))
                        {
                            Console.WriteLine(line);
                        };                        
                        break;
                    case "PL":
                        foreach (var line in _outputer.GetCapitalsInfo(command))
                        {
                            Console.WriteLine(line);
                        };                       
                        break;
                    default:
                        Console.WriteLine("Command not recognized!");
                        break;
                }

                Console.WriteLine("Write 'N' to continue or 'Y' to close the app");
                endApp = Console.ReadLine();

                if (endApp.ToUpper() == "N")
                {
                    returnValue = InsertReturnValue();
                    command = InsertCommand();
                }
            }

        }

        private static string InsertCommand()
        {
            Console.WriteLine("Insert your command (e.g. Strategy1, Strategy2, Strategy15):");
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
