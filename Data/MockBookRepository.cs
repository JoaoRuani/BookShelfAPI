using System.Collections.Generic;
using System.Threading.Tasks;
using BookShelfAPI.Models;
using BookShelfAPI.Storage;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Data
{
    public class MockBookRepository : IBookRepository
    {
        private readonly IBookStorage _storage;

        public MockBookRepository(IBookStorage storage)
        {
            _storage = storage;
        }
        public IEnumerable<Book> GetAllBooks() => new List<Book>() {
            new Book{Id = 1, Title = "Clean Code", Arquivo = "arquivo", Capa = "capa"},
            new Book{Id = 2, Title = "Pragmatic Programmer", Arquivo = "arquivo", Capa = "capa"},
            new Book{Id = 3, Title = "Design Patterns", Arquivo = "https://bookshelf.blob.core.windows.net/assets/design_patterns.pdf", Capa = "capa"}
        };

        public Book GetBookById(int id) 
        => new Book{Id = 1, Title = "Clean Code", Arquivo = "arquivo", Capa = "capa"};

        public Book CreateBook(Book book)
        {
            book.Id = 1;
            return book;
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public Book UpdateBook(Book book)
        {
            throw new System.NotImplementedException();
        }

        public Book GetBookByTitle(string title)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBook(Book book)
        {
            throw new System.NotImplementedException();
        }
    }
}