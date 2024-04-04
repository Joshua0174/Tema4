using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IRepository<Book> _bookRepository;
        public BookController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }


        [HttpGet("get")]
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetAll();
        }

        [HttpPost("add")]
        public ObjectResult Add(BookDto bookDto)
        {
            _bookRepository.Add(new Book(bookDto.Title, bookDto.AuthorId));
            _bookRepository.SaveChanges();

            return Ok("Added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, BookDto authorDto)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound("Book cannot be found.");
            }
            book.AuthorId = authorDto.AuthorId;
            book.Title = authorDto.Title;
            _bookRepository.Update(book);
            _bookRepository.SaveChanges();
            return Ok("Updated succesfully.");
        }



        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound("Book cannot be found.");
            }

            _bookRepository.Remove(book);
            _bookRepository.SaveChanges();
            return Ok("Book is deleted succesfully.");

        }
    }
}
