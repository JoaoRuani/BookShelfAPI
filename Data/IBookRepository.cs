using System.Collections.Generic;
using System.Threading.Tasks;
using BookShelfAPI.Models;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        Book GetBookByTitle(string title);

        Book CreateBook(Book book);

        bool SaveChanges();

        Book UpdateBook(Book book); 

        void DeleteBook(Book book);
    }
}