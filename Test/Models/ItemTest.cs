using Library.Models;
using NUnit.Framework;
using Telerik.JustMock;

namespace Test.Models
{
    [TestFixture]
    public class ItemTest
    {
        [Test]
        public void ShouldAutoArrangePropertySetInConstructor()
        {
            // Arrange 
            var expected = "name";
            var item = Mock.Create<Item>(() => new Item(expected));

            // Assert 
            Assert.AreEqual(expected, item.Name);
        }
    }
}
