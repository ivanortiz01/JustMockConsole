using Library.Models;
using System;
using System.Linq.Expressions;

namespace Library.Repositories.Intefaces
{
    public interface IBookRepository
    {
        Book GetWhere(Expression<Func<Book, bool>> expression);
    }
}
