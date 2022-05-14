using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhoneBook
{
    public interface IPhoneBookStore
    {
        Dictionary<string, string> GetContactList();
        void SaveAll(Dictionary<string, string> contactsList);
        void WriteContact(string name, string number);
    }

    public class DbVersion : IPhoneBookStore
    {
        public Dictionary<string, string> GetContactList()
        {
            throw new NotImplementedException();
        }

        public void SaveAll(Dictionary<string, string> contactsList)
        {
            throw new NotImplementedException();
        }

        public void WriteContact(string name, string number)
        {
            throw new NotImplementedException();
        }
    }

    public class PhoneBookStore : IPhoneBookStore
    {
        private string path = "C:\\Users\\Kamila\\Desktop\\C# projects\\C-Lessons\\Lesson2\\PhoneBook\\PhoneBook\\PhoneBookList.txt";
        public void WriteContact(string name, string number)
        {
            File.AppendAllText(path, $"{name},{number}" + Environment.NewLine);
        }

        public void SaveAll(Dictionary<string, string> contactsList)
        {
            File.Delete(path);
            File.AppendAllLines(path, contactsList.Select(x => $"{x.Key},{x.Value}"));
        }

        public Dictionary<string, string> GetContactList()
        {
            var contactsList = new Dictionary<string, string>();

            var allContacts = File.ReadAllLines(path).ToList();

            if (allContacts.Count > 0)
            {
                foreach (var contact in allContacts)
                {
                    var split = contact.Split(",");
                    contactsList.Add(split[0], split[1]);
                }
            }

            return contactsList;
        }
    }
}
