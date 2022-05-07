using NUnit.Framework;
using PhoneBook;

namespace PhoneBookTests
{   
    class Tests
    {

        [Test]
        public void SaveContact()
        {
            PhoneBookService phoneBook = new PhoneBookService();
            phoneBook.AddContact("David", "07900001234");
            phoneBook.contactsList.TryGetValue("David", out string value);
            Assert.AreEqual(07900001234, value);
        }

        [Test]
        public void FindContact()
        {
            PhoneBookService phoneBook = new PhoneBookService();
            phoneBook.AddContact("David", "07900001234");
            var result = phoneBook.FindContact("David");            
            Assert.AreEqual(07900001234, result);
        }

        [Test]
        public void DeleteContact()
        {
            PhoneBookService phoneBook = new PhoneBookService();
            phoneBook.AddContact("David", "07905555555");
            var result = phoneBook.DeleteContact("David");
            Assert.AreEqual(07905555555, result);
        }

        [Test]
        public void DeleteNumber()
        {
            PhoneBookService phoneBook = new PhoneBookService();
            phoneBook.AddContact("David", "07905555555");
            phoneBook.DeleteNumber("07905555555");
            phoneBook.contactsList.TryGetValue("David", out string value);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void UpdateContact()
        {
            PhoneBookService phoneBook = new PhoneBookService();
            //phoneBook.AddContact("David", 07900001234);
            var result = phoneBook.UpdateNumber("David", "07911001234");
            Assert.AreEqual(07911001234, result);
        }

    }
}