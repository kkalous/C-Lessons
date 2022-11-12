using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            lock (_contactsList)
            {
                if (IsValidPhoneNumber(number) && !Exists(name))
                {
                    _contactsList.Add(name, number);
                    _store.WriteContactAsync(name, number);
                    return true;
                }

                return false;
            }

        }

        public string FindContact(string name)
        {
            lock (_contactsList)
            {
                _contactsList.TryGetValue(name, out string value);
                return value;
            }
        }

        public string DeleteContact(string name)
        {
            lock (_contactsList)
            {
                var successful = _contactsList.TryGetValue(name, out string value);
                if (successful)
                {
                    _contactsList.Remove(name);
                    _store.DeleteContactAsync(name, null);
                }
                return value;
            }
        }

        public void DeleteNumber(string number)
        {
            lock (_contactsList)
            {
                foreach (var item in _contactsList)
                {
                    if (item.Value == number)
                    {
                        _contactsList.Remove(item.Key);
                        _store.DeleteContactAsync(null, number);

                        return;
                    }
                }
            }

        }


        public string UpdateNumber(string name, string newNumber)
        {
            lock (_contactsList)
            {
                var success = _contactsList.TryGetValue(name, out string value);

                if (success)
                {
                    _contactsList[name] = newNumber;
                    _store.UpdateContactAsync(name, newNumber);

                    return value;
                }

                AddContact(name, newNumber);
                return null;
            }

        }


        //Can't add lock here in the way how it is now, as it's used in another lock
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
             lock (_contactsList)
            {
                if (_contactsList.Count == 0 || forceReload)
                {
                    _contactsList = _store.GetContactList();
                }
            }
        }
    }
}