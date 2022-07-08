using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook
{

    public class PhoneBookService
    {
        private readonly IPhoneBookStore _store;
        public Dictionary<string, string> _contactsList = new Dictionary<string, string>();

        public PhoneBookService(IPhoneBookStore store)
        {
            _store = store;
            PopulateContactList();
        }

        public bool AddContact(string name, string number)
        {
            if (IsValidPhoneNumber(number) && !Exists(name))
            {
                _contactsList.Add(name, number);
                _store.WriteContact(name, number);
                return true;
            }

            return false;
        }

        public string FindContact(string name)
        {
            _contactsList.TryGetValue(name, out string value);
            return value;
        }

        public string DeleteContact(string name)
        {
            var successful = _contactsList.TryGetValue(name, out string value);

            if (successful)
            {
                _contactsList.Remove(name);
                _store.DeleteContact(name, null);
            }

            return value;
        }

        public void DeleteNumber(string number)
        {
            foreach (var item in _contactsList)
            {
                if (item.Value == number)
                {
                    _contactsList.Remove(item.Key);
                    _store.DeleteContact(null, number);

                    return;
                }
            }

        }


        public string UpdateNumber(string name, string newNumber)
        {
            var success = _contactsList.TryGetValue(name, out string value);

            if (success)
            {
                _contactsList[name] = newNumber;
                _store.UpdateContact(name, newNumber);

                return value;
            }

            AddContact(name, newNumber);
            return null;

        }

        private bool Exists(string name)
        {
            return _contactsList.ContainsKey(name);
        }


        private bool IsValidPhoneNumber(string number)
        {
            bool isNumber = long.TryParse(number, out _);
            if (number.Length == 11 && isNumber)
            {
                return true;
            }

            return false;

        }

        private void PopulateContactList(bool forceReload = false)
        {
            if (_contactsList.Count == 0 || forceReload)
            {
                _contactsList = _store.GetContactList();
            }
        }
    }
}