using System;
using System.Collections.Generic;

namespace PhoneBook
{

    class Program
    {        

        static void Main(string[] args)
        {
            PhoneBookService phoneBook = new PhoneBookService();

            Console.WriteLine("Hi! Welcome to PhoneBook!\n");
            Console.WriteLine("If you want to search for a contact write S\n" +
                                "If you want add contact write A\n" +
                                "If you want update cantact write U\n" +
                                "If you want to delete contact write D\n" +
                                "If you want to delete number write DN\n" +
                                "Wrtie E to exit PhoneBook");
            var menuOption = Console.ReadLine().ToString();
            if (menuOption == "S" || menuOption == "s")
            {
                Console.WriteLine("Insert contact name: ");
                var name = Console.ReadLine();
                phoneBook.FindContact(name);

                Console.WriteLine("What else?\n");
                menuOption = Console.ReadLine().ToString();
            }
            else if (menuOption == "A" || menuOption == "a")
            {
                Console.WriteLine("Insert number: ");
                var number = Console.ReadLine();
                var numberLong = Convert.ToInt32(number);

                Console.WriteLine("Insert name: ");
                var name = Console.ReadLine();

                phoneBook.AddContact(name, numberLong);

                Console.WriteLine("What else?\n");
                menuOption = Console.ReadLine().ToString();

            }
            else if (menuOption == "U" || menuOption == "u")
            {
                Console.WriteLine("Insert the contact name you want to update number for: ");
                var name = Console.ReadLine();

                Console.WriteLine("Insert new number: ");
                var number = Console.ReadLine();
                var numberLong = Convert.ToInt32(number);

                phoneBook.UpdateNumber(name, numberLong);

                Console.WriteLine("What else?\n");
                menuOption = Console.ReadLine().ToString();
            }
            else if (menuOption == "D" || menuOption == "d")
            {
                Console.WriteLine("Insert the contact name you want to delete: ");
                var name = Console.ReadLine();
                phoneBook.DeleteContact(name);

                Console.WriteLine("What else?\n");
                menuOption = Console.ReadLine().ToString();

            }
            else if (menuOption == "DN" || menuOption == "dn")
            {
                Console.WriteLine("Insert number you want to delete: ");
                var number = Console.ReadLine();
                var numberLong = Convert.ToInt32(number);
                phoneBook.DeleteNumber(numberLong);

                Console.WriteLine("What else?\n");
                menuOption = Console.ReadLine().ToString();
            }
            else if (menuOption == "E" || menuOption == "e")
            {
                Console.WriteLine("Bye");
            }
            else
            {
                Console.WriteLine("I don't recognize this command. Please try it again!");
                menuOption = Console.ReadLine().ToString();
            }
            
        }
    }

    class MyGenericClass<T1, TField>
    {
        private readonly T1 _variable;
        public MyGenericClass(T1 variable)
        {
            _variable = variable;
        }

        public TField Field { get; set; }
    }

    class MyGenericMethods
    {
        private string ToString<T>(T it)
        {
            return it.ToString();
        }

        public static T SafeGetAt<T>(List<T> list, int index)
        {
            return index < list.Count ? list[index] : default(T);
        }
    }
}
