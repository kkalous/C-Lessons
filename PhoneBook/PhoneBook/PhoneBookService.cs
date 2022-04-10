using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook
{
    public class PhoneBookService
    {
        public IDictionary<string, long> contactsList = new Dictionary<string, long>();
        
        //Using dictionary
        public void AddContact(string name, long number)
        {
            if (name.Length <= 5)
            {
                try
                {
                    if (CheckNumber(number))
                    {
                        CheckIfExist(name, number);
                        return;
                    }
                }

                catch (Exception ex)
                {
                    Console.Write(ex);
                    throw;
                }
            }
            Console.WriteLine($"Name {name} is too long. Change to only 5 charactes");
            return;
        }       


        //using array
        public int[] numbers;
        public string[] names;

        public long? FindContact(string name)
        {
           contactsList.TryGetValue(name, out long value);
           long? result = null;
           result = value;
           if (result == null) Console.WriteLine($"No number saved for Contact: {name}");
            return result;        
        }

        public long? DeleteContact(string name)
        {
            var successful = contactsList.TryGetValue(name, out long value); ;
            if (successful == true)
            {
                contactsList.Remove(name);
                Console.WriteLine($"Contact with name: {name} and number:{value} deleted");
                return value;

            }
            else
            {
                Console.WriteLine("Contact doesn't exist");
                return null;
            }
        }

        public void DeleteNumber(long number)
        {
            foreach(var item in contactsList)
            {
                if (item.Value == number)
                {
                    contactsList.Remove(item.Key);
                    return;
                }
                else
                {
                    Console.WriteLine("This number is not save");
                    return;
                }
            }
        }

        public long UpdateNumber(string name, long newNumber)
        {
            var success = contactsList.TryGetValue(name, out long value);
            if(success == true)
            {
                if (value == newNumber) Console.WriteLine("New number is the same as saved number");
                contactsList[name] = newNumber;
                Console.WriteLine($"Contact: {name} Number: {newNumber} saved - Old number: {value}");
                return value;
            }

            Console.WriteLine($"Contact: {name} doesn't exist. ");
            AddContact(name, newNumber);
            Console.WriteLine($"New Contact saved! Name: {name} Number: {newNumber}");
            return newNumber;
        }

        private bool CheckIfExist(string name, long number)
        {
            if (contactsList.ContainsKey(name))
            {
                contactsList.TryGetValue(name, out long value);
                Console.WriteLine($"Contact with this name already exists! It's saved with number {value}");
                return false;
            }

            else
            {
                contactsList.Add(name, number);
                Console.WriteLine($"Contact saved under name {name} with number {number}");
                return true;
            }
        }

        private bool CheckNumber(long number)
        {
            if (number.ToString().Length == 10)
            {
                Console.WriteLine("Number OK");
                return true;
            }
            else
            {
                Console.WriteLine($"Number {number} is in wrong format. Change to the UK mobile phone number format '07911 123456'");
                return false;
            }
        }

    }
}
