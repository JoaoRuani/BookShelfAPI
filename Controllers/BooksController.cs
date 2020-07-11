using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShelfAPI.Data;
using BookShelfAPI.Dtos;
using BookShelfAPI.Models;
using BookShelfAPI.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfAPI.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        
        private readonly IBookRepository _repository;
        private readonly IBookStorage _storage;

        public BooksController(IBookRepository repository, IBookStorage storage)
        {
            _repository = repository;
            _storage = storage;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _repository.GetAllBooks();
            if(books.Count() > 0)
            {
                return Ok(books);
            }
            return NoContent();
        }
        [HttpGet("{id}", Name="GetBook")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _repository.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        [RequestSizeLimit(200_000_000)]
        public async Task<ActionResult<Book>> CreateBook([FromForm]CreateBookDto bookDto)
        {
            if(!(_repository.GetBookByTitle(bookDto.Title) is null))
            {
                ModelState.AddModelError("duplicate", $"{bookDto.Title} already exists");
                return ValidationProblem(ModelState);
            }
            var book = new Book{Title = bookDto.Title};

            var bookBuilded = await new BookBuilder(book, bookDto.File).Make();
            var createdBook = _repository.CreateBook(bookBuilded);
            _repository.SaveChanges();
            return CreatedAtRoute(nameof(GetBook), new {Id = createdBook.Id}, createdBook);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, UpdateBookDto updateBook)
        {
            var bookFromRepo = _repository.GetBookById(id);
            if(bookFromRepo is null)
            {
                return NotFound();
            }

            //refatorar em um atualizador de livros
            var bookByTitle = _repository.GetBookByTitle(updateBook.Title);
            if(!(bookByTitle is null))
            {
                if(!bookByTitle.Equals(bookFromRepo))
                {
                    ModelState.AddModelError("duplicate", $"{updateBook.Title} already exists");
                    return ValidationProblem(ModelState);
                }
            }
            bookFromRepo.Title = updateBook.Title;
            _repository.UpdateBook(bookFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = _repository.GetBookById(id);
            if(book is null)
            {
                return NotFound();
            }
            var bookDestructor = new BookDestructor(book, _repository); 
            bookDestructor.Destruct();
            return NoContent();
        }
    }
}