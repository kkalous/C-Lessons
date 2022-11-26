using PhoneBookWithDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{

    public class PhoneBookServiceAsync
    {
        private readonly IPhoneBookStoreLinq _store;
        public Dictionary<string, string> _contactsList = new Dictionary<string, string>();

        public PhoneBookServiceAsync(IPhoneBookStoreLinq store)
        {
            _store = store;
            PopulateContactList();
        }

        public async Task<bool> AddContactAsync(string name, string number)
        {
            if (IsValidPhoneNumber(number) && !Exists(name))
            {
                _contactsList.Add(name, number);
                await _store.WriteContactAsync(name, number).ConfigureAwait(false);
                return true;
            }

            return false;


        }

        public string FindContact(string name)
        {
            _contactsList.TryGetValue(name, out string value);
            return value;

        }

        public string DeleteContact(string name) //Not Async as we don't have DeleteAsync method in linq
        {
            var successful = _contactsList.TryGetValue(name, out string value);
            if (successful)
            {
                _contactsList.Remove(name);
                _store.DeleteContact(name, null);
            }
            return value;
        }

        public void DeleteNumber(string number) //Not Async as we don't have DeleteAsync method in linq
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


        public async Task<string> UpdateNumberAsync(string name, string newNumber)
        {
            var success = _contactsList.TryGetValue(name, out string value);
            if (success)
            {
                _contactsList[name] = newNumber;
                await _store.UpdateContactAsync(name, newNumber);
                return value;
            }
            await AddContactAsync(name, newNumber);
            return null;
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
            if (_contactsList.Count == 0 || forceReload)
            {
                _contactsList = _store.GetContactList();
            }

        }
    }
}

