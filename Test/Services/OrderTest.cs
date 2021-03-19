using Library.Services;
using Library.Services.Interfaces;
using NUnit.Framework;
using System;
using Telerik.JustMock;

namespace Test.Services
{
    [TestFixture]
    public class OrderTest
    {
        [Test]
        public void DoInstead_TestMethod()
        {
            //Arrange 
            var warehouse = Mock.Create<Iwarehouse>();
            var order = new Order("Camera", 2);

            bool called = false;
            Mock.Arrange(() => warehouse.HasInventory("Camera", 2)).DoInstead(() => called = true);

            //Act 
            order.Fill(warehouse);

            //Assert 
            Assert.IsTrue(called);
        }

        [Test]
        public void CallOriginal_TestMethod()
        {
            //Arrange 
            var order = Mock.Create<Order>(Behavior.CallOriginal, "Camera", 2);

            Mock.Arrange(() => order.Receipt(DateTime.Today)).CallOriginal();
            Mock.Arrange(() => order.Receipt(Arg.Matches<DateTime>(d => d > DateTime.Today))).Returns("Invalid DateTime");

            //Act 
            var callWithToday = order.Receipt(DateTime.Today);
            var callWithDifferentDay = order.Receipt(DateTime.Today.AddDays(1));

            //Assert 
            Assert.AreEqual("Ordered 2 Camera on " + DateTime.Today.ToString("d"), callWithToday);
            Assert.AreEqual("Invalid DateTime", callWithDifferentDay);
        }

        [Test]
        public void Throws_TestMethod()
        {
            // Arrange 
            var order = new Order("Camera", 0);
            var warehouse = Mock.Create<Iwarehouse>();

            // Set up that the warehouse has inventory of any products with any quantities. 
            Mock.Arrange(() => warehouse.HasInventory(Arg.IsAny<string>(), Arg.IsAny<int>())).Returns(true);

            // Set up that call to warehouse. Remove with zero quantity is invalid and throws an exception. 
            Mock.Arrange(() => warehouse.Remove(Arg.IsAny<string>(), Arg.Matches<int>(x => x == 0)))
                        .Throws(new InvalidOperationException());

            // Act 
            Assert.That(() => order.Fill(warehouse), Throws.InvalidOperationException);
        }

        [Test]
        public void MockingProperties_TestMethod()
        {
            // Arrange 
            var warehouse = Mock.Create<Iwarehouse>();

            Mock.Arrange(() => warehouse.Manager).Returns("John");

            string manager = string.Empty;

            // Act 
            manager = warehouse.Manager;

            // Assert 
            Assert.AreEqual("John", manager);
        }

        [Test]
        public void RaisingAnEvent_TestMethod()
        {
            // Arrange 
            var warehouse = Mock.Create<Iwarehouse>();

            Mock.Arrange(() => warehouse.Remove(Arg.IsAny<string>(), Arg.IsInRange(int.MinValue, int.MaxValue, RangeKind.Exclusive)))
                .Raises(() => warehouse.ProductRemoved += null, "Camera", 2);

            string productName = string.Empty;
            int quantity = 0;

            warehouse.ProductRemoved += (p, q) => { productName = p; quantity = q; };

            // Act 
            warehouse.Remove(Arg.AnyString, Arg.AnyInt);

            // Assert 
            Assert.AreEqual("Camera", productName);
            Assert.AreEqual(2, quantity);
        }

    }
}
