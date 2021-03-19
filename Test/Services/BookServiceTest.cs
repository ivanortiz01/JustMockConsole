using Library.Models;
using Library.Repositories.Intefaces;
using Library.Services;
using NUnit.Framework;
using Telerik.JustMock;

namespace Test.Services
{
    [TestFixture]
    public class BookServiceTest
    {
        [Test]
        public void ShouldAssertMockForDynamicQueryWhenComparedUsingAVariable()
        {
            // Arrange 
            var repository = Mock.Create<IBookRepository>();
            var expected = new Book { Id = 1, Title = "Adventures" };
            var service = new BookService(repository);

            Mock.Arrange(() => repository.GetWhere(book => book.Id == 1))
                .Returns(expected)
                .MustBeCalled();

            // Act 
            var actual = service.GetSingleBook(1);

            // Assert 
            Assert.AreEqual(actual.Title, expected.Title);
        }
    }
}
