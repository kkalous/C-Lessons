using Moq;
using NUnit.Framework;
using PhoneBook;
using PhoneBookWithDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookTest
{
    class UnitTest2
    {
        private PhoneBookServiceAsync _phoneBook;

        [Test]
        public void DeleteContactSuccessAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            _phoneBook.AddContactAsync("Kamila", "07900001234");

            var deletedNumber = _phoneBook.DeleteContact("Kamila");

            Assert.AreEqual("07900001234", deletedNumber); // Contact deleted - returns deleted number 
        }

        [Test]
        public void DeleteContactFailAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            var deletedNumber = _phoneBook.DeleteContact("Kamila");

            Assert.AreEqual(null, deletedNumber); //Contact doesn't exist - nothing was deleted - returns null
        }


        [Test]
        public void AddContactSuccessAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            _phoneBook.AddContactAsync("Kamila", "07900001234");
            _phoneBook._contactsList.TryGetValue("Kamila", out string value);
            Assert.AreEqual("07900001234", value); //Added new contact and returned number that was saved 
        }

        [Test]
        public void AddContactAlreadyExistsAsync()
        {

            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>() { { "Kamila", "07900000000" } });
            var contactPhonebook = new PhoneBookServiceAsync(fakeStore.Object);

            var contactAdded = contactPhonebook.AddContactAsync("Kamila", "07900001234");
            Assert.AreEqual(contactAdded.Result, false); //Can't add a contact because contact with this name is already saved
        }

        [Test]
        public void AddContactNotValidNumberAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            var contactAdded = _phoneBook.AddContactAsync("Kamila", "Nekdo Dalsi");
            Assert.AreEqual(contactAdded.Result, false); //Can't add a contact because contact with this name is already number is not valid
        }

        [Test]
        public void SearchContactSuccessAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);
            _phoneBook.AddContactAsync("Kamila", "07900001234");
            _phoneBook._contactsList.TryGetValue("Kamila", out string value);
            Assert.AreEqual("07900001234", value); // Contact exists - returns saved number
        }

        [Test]
        public void SearchContactFail()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            _phoneBook._contactsList.TryGetValue("John", out string value);
            Assert.AreEqual(null, value); //Contact doesn't exists - returns null
        }

        [Test]
        public void DeleteNumberSuccessAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            _phoneBook.AddContactAsync("El", "07905555555");
            _phoneBook._contactsList.TryGetValue("El", out string value);
            Assert.AreEqual("07905555555", value); //Checking if contact was really added
            _phoneBook.DeleteNumber("07905555555");
            _phoneBook._contactsList.TryGetValue("El", out string value1);
            Assert.AreEqual(null, value1); // Checking if contact was deleted
        }
        [Test]
        public void UpdateContactSuccessAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            _phoneBook.AddContactAsync("Kamila", "07911111234");
            var result = _phoneBook.UpdateNumberAsync("Kamila", "07911111234"); //Contact updated - Returns old number
            Assert.AreEqual("07911111234", result.Result);
        }

        [Test]
        public void UpdateContactFailAsync()
        {
            var fakeStore = new Mock<IPhoneBookStoreLinq>();
            fakeStore.Setup(x => x.GetContactList()).Returns(new Dictionary<string, string>());
            _phoneBook = new PhoneBookServiceAsync(fakeStore.Object);

            var result = _phoneBook.UpdateNumberAsync("Amber", "07911001234"); //Contact doesn't exist - returns null
            Assert.AreEqual(null, result.Result);
        }
    }
}