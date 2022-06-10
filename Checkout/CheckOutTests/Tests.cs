using Checkout;
using Checkout.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckOutTests
{
    public class Tests
    {
        private CheckoutService _service;

        [SetUp]
        public void Setup()
        {
            var fakeService = new Mock<IProductDb>();
            fakeService.Setup(x => x.GetProducts()).Returns(new Dictionary<string, Product>());
            _service = new CheckoutService(fakeService.Object);
        }

        [Test]
        public void TotalCheckoutTest()
        {
           var something =  _service.TotalCheckOut("ABBDDD");

            Assert.Pass();
        }
    }
}