using System;
using System.IO;
using System.Threading.Tasks;
using BookShelfAPI.Storage;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Models
{
    public class BookBuilder
    {
        private Book Book { get; set; }
        private readonly CoverGenerator _coverGenerator;
        private readonly BlobBookStorage _bookStorage;

        private readonly IFormFile _file;

        public BookBuilder(Book book, IFormFile file) : this()
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            Book = book;
            _file = file;
        }
        private BookBuilder()
        {
            _coverGenerator = new CoverGenerator();
            _bookStorage = new BlobBookStorage();
        }

        public async Task<Book> Make()
        {
            var coverFileName = Book.Title.Replace(' ', '-') + "-cover.jpg";
            var bookFileName = Book.Title.Replace(' ', '-') + ".pdf";

            var coverStream = await _coverGenerator.GenerateCover(_file.OpenReadStream());
            Book.Capa = await _bookStorage.PersistCoverFile(coverStream, coverFileName);

            Book.Arquivo = await _bookStorage.PersistBookFile(_file.OpenReadStream(), bookFileName);

            return Book;
        }
    }
}