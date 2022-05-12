using NUnit.Framework;
using PhoneBook;

namespace PhoneBookTests
{   
    class Tests
    {
        private readonly PhoneBookService _phoneBook;
        public Tests()
        {
            _phoneBook = new PhoneBookService(new PhoneBookStore());
        }

        [Test]
        public void DeleteContactSuccess()
        {
            var deletedNumber = _phoneBook.DeleteContact("Kamila");
            Assert.AreEqual("07900001234", deletedNumber); // Contact deleted - returns deleted number 
        }

        [Test]
        public void DeleteContactFail()
        {
            var deletedNumber = _phoneBook.DeleteContact("Kamila");
            Assert.AreEqual(null, deletedNumber); //Contact doesn't exist - nothing was deleted - returns null
        }


        [Test]
        public void AddContactSuccess()
        {
            _phoneBook.AddContact("Kamila", "07900001234");
            _phoneBook._contactsList.TryGetValue("Kamila", out string value);
            Assert.AreEqual("07900001234", value); //Added new contact and returned number that was saved 
        }

        [Test]
        public void AddContactAlreadyExists()
        {
            var contactAdded = _phoneBook.AddContact("Kamila", "07900001234");
            Assert.AreEqual(contactAdded, false); //Can't add a contact because contact with this name is already saved
        }

        [Test]
        public void AddContactNotValidNumber()
        {
            var contactAdded = _phoneBook.AddContact("Kamila", "Nekdo Dalsi");
            Assert.AreEqual(contactAdded, false); //Can't add a contact because contact with this name is already number is not valid
        }

        [Test]
        public void SearchContactSuccess()
        {
            _phoneBook._contactsList.TryGetValue("Kamila", out string value);
            Assert.AreEqual("07900001234", value); // Contact exists - returns saved number
        }

        [Test]
        public void SearchContactFail()
        {
            _phoneBook._contactsList.TryGetValue("John", out string value);
            Assert.AreEqual(null, value); //Contact doesn't exists - returns null
        }

        [Test]
        public void DeleteNumberSuccess()
        {
            _phoneBook.AddContact("El", "07905555555");
            _phoneBook._contactsList.TryGetValue("El", out string value);
            Assert.AreEqual("07905555555", value); //Checking if contact was really added
            _phoneBook.DeleteNumber("07905555555");
            _phoneBook._contactsList.TryGetValue("El", out string value1);
            Assert.AreEqual(null, value1); // Checking if contact was deleted
        }
        [Test]
        public void UpdateContactSuccess()
        {
            var result = _phoneBook.UpdateNumber("Kamila", "07911001234"); //Contact updated - Returns old number
            Assert.AreEqual("07900001234", result);
        }

        [Test]
        public void UpdateContactFail()
        {
            var result = _phoneBook.UpdateNumber("Amber", "07911001234"); //Contact doesn't exist - returns null
            Assert.AreEqual(null, result);
        } 
    }
}