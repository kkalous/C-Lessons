using Db.Scaffolded;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWithDb
{        
    public interface IPhoneBookStoreLinq
    {
        Dictionary<string, string> GetContactList();
        Task WriteContactAsync(string name, string number);
        void DeleteContact(string name, string number);
        Task UpdateContactAsync(string name, string number);
    }

    public class PhoneBookStoreLinq : IPhoneBookStoreLinq
    {
        public async Task WriteContactAsync(string name, string number)
        {
            using var db = new PhoneBookContext();

            var newContact = new Contact
            {
                ContactName = name,
                ContactNumber = number 
            };

             await db.Contacts.AddAsync(newContact).ConfigureAwait(false);

            await db.SaveChangesAsync().ConfigureAwait(false);

            return;
        }
        
        public void DeleteContact(string name, string number)
        {
            using var db = new PhoneBookContext();

            var contact = new Contact();
            if (name == null)
            {
                name = db.Contacts.Where(w => w.ContactNumber == number).Select(s => s.ContactName).SingleOrDefault();

                contact = new Contact
                {
                    ContactName = name,
                    ContactNumber = number 
                };
            }

            else if (number == null)
            {
                contact = new Contact
                {
                    ContactName = name
                };
            }
            else
            {
                contact = new Contact
                {
                    ContactName = name,
                    ContactNumber = number
                };
            }            

              db.Contacts.Remove(contact);
                db.SaveChanges();

            return;
        }

        public async Task UpdateContactAsync(string name, string number)
        {
            using var db = new PhoneBookContext();

            var result = await db.Contacts.FindAsync(name).ConfigureAwait(false);

            if (result == null)
            {
               await WriteContactAsync(name, number).ConfigureAwait(false);

                await db.SaveChangesAsync().ConfigureAwait(false);
                return;
            }

            DeleteContact(name, null);
            await WriteContactAsync(name, number).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return;
        }

        public Dictionary<string, string> GetContactList()
        {
            try
            {
                var contactsList = new Dictionary<string, string>();

                using var db = new PhoneBookContext();

                var result = db.Contacts.ToList();

                foreach (var contact in result)
                {
                    contactsList.Add(contact.ContactName.ToString(), contact.ContactNumber.ToString());
                }

                return contactsList;

            }
            catch (Exception ex)
            {
                //display error message
                throw ex;
            }

        }
    }

}
