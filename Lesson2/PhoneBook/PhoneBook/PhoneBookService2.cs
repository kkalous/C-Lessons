using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhoneBook
{
    public class PhoneBookService2
    {
        public IDictionary<string, string> contactsList = new Dictionary<string, string>();

        public string path = "C:\\Users\\Kamila\\Desktop\\C# projects\\C-Lessons\\Lesson2\\PhoneBook\\PhoneBook\\PhoneBookList.txt";

        public void AddContact(string name, string number)
        {
            try
            {
                if (CheckNumber(number))
                {
                    if (!Exists(name, number))
                    {
                        WriteContact(name, number);
                        PopulateContactList(true);
                    }

                }

            }

            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }

        }

        public string FindContact(string name)
        {
            PopulateContactList();
            if (contactsList.ContainsKey(name))
            {
                contactsList.TryGetValue(name, out string value);
                return value;
            }
            else
            {
                return null;
            }
        }

        public string DeleteContact(string name)
        {
            PopulateContactList();
            var successful = contactsList.TryGetValue(name, out string value);

            if (successful == true)
            {
                contactsList.Remove(name);

                File.Delete(path);
                File.AppendAllLines(path, contactsList.Select(x => $"{x.Key},{x.Value}"));

                return value;
            }
            else
            {
                return null;
            }
        }

        public void DeleteNumber(string number)
        {

            PopulateContactList();
            foreach (var item in contactsList)
            {
                if (item.Value == number)
                {
                    contactsList.Remove(item.Key);

                    File.Delete(path);
                    File.AppendAllLines(path, contactsList.Select(x => $"{x.Key},{x.Value}"));

                    return;
                }
            }

            Console.WriteLine("Number is not saved");
        }


        public string UpdateNumber(string name, string newNumber)
        {
            PopulateContactList();
            var success = contactsList.TryGetValue(name, out string value);

            if (success == true)
            {
                if (value == newNumber) Console.WriteLine("New number is the same as saved number");

                contactsList[name] = newNumber;

                File.Delete(path);
                File.AppendAllLines(path, contactsList.Select(x => $"{x.Key},{x.Value}"));

                return value;
            }

            Console.WriteLine($"Contact: {name} doesn't exist. ");
            AddContact(name, newNumber);
            Console.WriteLine($"New Contact saved! Name: {name} Number: {newNumber}");
            return newNumber;
        }

        private bool Exists(string name, string number)
        {
            PopulateContactList();
            if (contactsList.ContainsKey(name))
            {
                contactsList.TryGetValue(name, out string value);
                Console.WriteLine($"Contact with this name already exists! It's saved with number {value}");
                return true;
            }
            return false;

        }

        private void WriteContact(string name, string number)
        {
            File.AppendAllText(path, $"{name},{number}" + Environment.NewLine);
            Console.WriteLine($"Contact saved under name {name} with number {number}");
        }
        private bool CheckNumber(string number)
        {
            if (number.Length == 11)
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

        private IDictionary<string, string> PopulateContactList(bool forceReload = false)
        {
            if (contactsList.Count == 0 || forceReload)
            {
                contactsList = new Dictionary<string, string>();

                var allContacts = File.ReadAllLines(path).ToList();

                if (allContacts.Count > 0)
                {
                    foreach (var contact in allContacts)
                    {
                        var split = contact.Split(",");
                        contactsList.Add(split[0], split[1]);
                    }
                }
            }

            return contactsList;

        }
        //private void UpdatePhoneBook(string name, string number)
        //{
        //    var allContacts = File.ReadAllLines(path).ToList();

        //    if (allContacts.Count > 0)
        //    {
        //        var i = 0;
        //        foreach (var contact in allContacts)
        //        {
        //            var split = contact.Split(",");

        //            if (name != null && split[0] == name && number == null)
        //            {
        //                File.Delete(path);

        //                allContacts.RemoveAt(i);
        //                File.AppendAllLines(path, allContacts);
        //                return;
        //            }

        //            else if(number != null && split[1] == number)
        //            {
        //                File.Delete(path);

        //                allContacts.RemoveAt(i);
        //                File.AppendAllLines(path, allContacts);
        //                return;
        //            }

        //            else if(number != null && name != null && split[0] == name)
        //            {
        //                File.Delete(path);

        //                allContacts.RemoveAt(i);
        //                allContacts.Add($"{name},{number}");
        //                File.AppendAllLines(path, allContacts);
        //                return;
        //            }

        //            i++;                   

        //        }
        //    }            
        //}
    }
}
