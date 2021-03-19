using Library.Services;
using NUnit.Framework;
using Telerik.JustMock;

namespace Test.Services
{
    [TestFixture]
    public class FooTest
    {

        [Test]
        public void SimpleTestMethod()
        {
            // Arrange 
            var foo = Mock.Create(() => new Foo(1));

            // Assert 
            Assert.IsNotNull(foo);
        }
    }
}
