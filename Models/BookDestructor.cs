using BookShelfAPI.Data;
using BookShelfAPI.Storage;

namespace BookShelfAPI.Models
{
    public class BookDestructor
    {
        public Book Book { get; private set; }
        private readonly IBookStorage _storage;
        private readonly IBookRepository _repository;

        public BookDestructor(Book book, IBookRepository repository)
        {
            Book = book;
            _storage = new BlobBookStorage();
            _repository = repository;
        }

        public void Destruct()
        {
            DestructFiles();
            _repository.DeleteBook(Book);
            _repository.SaveChanges();
        }
        public void DestructFiles()
        {
            if(!(Book.Capa is null))
            {
                _storage.DeleteBookFileRelated(Book.Capa);
            }
            _storage.DeleteBookFileRelated(Book.Arquivo);
        }
    }
}