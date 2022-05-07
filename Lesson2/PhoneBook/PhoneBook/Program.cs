using System;
using System.Collections.Generic;
using System.IO;

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
            var menuOption = Console.ReadLine();
            //var result = menuOption.Equals("e", StringComparison.InvariantCultureIgnoreCase);
            //var results = "e".Equals(menuOption, StringComparison.InvariantCultureIgnoreCase);
            while (menuOption != "E" && menuOption != "e")
            {                
                if (menuOption == "S" || menuOption == "s")
                {
                    Console.WriteLine("Insert contact name: ");
                    var name = Console.ReadLine();
                    var result = phoneBook.FindContact(name);

                    if (result == null)
                    {
                        Console.WriteLine($"No number saved for Contact: {name}");
                    }
                    else
                    {
                        Console.WriteLine($"Number saved under Contact {name} is {result}");
                    }

                    Console.WriteLine("What else?\n");
                    menuOption = Console.ReadLine().ToString();
                }
                else if (menuOption == "A" || menuOption == "a")
                {
                    Console.WriteLine("Insert number: ");
                    var number = Console.ReadLine();

                    Console.WriteLine("Insert name: ");
                    var name = Console.ReadLine();

                    phoneBook.AddContact(name, number);

                    Console.WriteLine("What else?\n");
                    menuOption = Console.ReadLine().ToString();

                }
                else if (menuOption == "U" || menuOption == "u")
                {
                    Console.WriteLine("Insert the contact name you want to update number for: ");
                    var name = Console.ReadLine();

                    Console.WriteLine("Insert new number: ");
                    var number = Console.ReadLine();

                    var newNumber = phoneBook.UpdateNumber(name, number);
                    Console.WriteLine($"Contact: {name} Number: {newNumber} saved");

                    Console.WriteLine("What else?\n");
                    menuOption = Console.ReadLine().ToString();
                }
                else if (menuOption == "D" || menuOption == "d")
                {
                    Console.WriteLine("Insert the contact name you want to delete: ");
                    var name = Console.ReadLine();
                    var deletedNumber = phoneBook.DeleteContact(name);

                    if (deletedNumber != null) Console.WriteLine($"Contact with name: {name} and number:{deletedNumber} deleted");
                    else Console.WriteLine("Contact doesn't exist");

                    Console.WriteLine("What else?\n");
                    menuOption = Console.ReadLine().ToString();

                }
                else if (menuOption == "DN" || menuOption == "dn")
                {
                    Console.WriteLine("Insert number you want to delete: ");
                    var number = Console.ReadLine();
                    phoneBook.DeleteNumber(number);

                    Console.WriteLine("What else?\n");
                    menuOption = Console.ReadLine().ToString();
                }
                else
                {
                    Console.WriteLine("I don't recognize this command. Please try it again!");
                    menuOption = Console.ReadLine().ToString();
                }               

            }           
            
        }
    }
}
