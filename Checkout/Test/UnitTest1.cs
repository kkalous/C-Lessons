using Checkout;
using Checkout.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test
{
    public class Tests
    {
        private CheckoutService _products;

        [SetUp]
        public void Setup()
        {
            var fakeService = new Mock<IProductDb>();
            fakeService.Setup(x => x.GetProducts()).Returns(new Dictionary<string, Product>());
            _products = new CheckoutService(fakeService.Object);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}