using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShelfAPI.Models;
using BookShelfAPI.Storage;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShelfContext _context;
        private readonly IBookStorage _storage;

        public BookRepository(BookShelfContext context, IBookStorage storage)
        {
            _context = context;
            _storage = storage;
        }
        public Book CreateBook(Book book)
        {
            if(book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.Books.Add(book);
            return book;
        }

        public void DeleteBook(Book book)
        {
            if(book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.Books.Remove(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public Book GetBookByTitle(string title)
        {
            if(title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }
            return _context.Books.FirstOrDefault(b => b.Title == title);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public Book UpdateBook(Book book)
        {
            // Not needed
            return book;
        }
    }
}