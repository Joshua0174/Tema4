using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookPublisherController : Controller
    {
        private readonly IRepository<BookPublisher> _bpRepository;
        public BookPublisherController(IRepository<BookPublisher> bpRepository)
        {
            _bpRepository = bpRepository;
        }


        [HttpGet("get")]
        public IEnumerable<BookPublisher> Get()
        {
            return _bpRepository.GetAll();
        }

        [HttpPost("add")]
        public ObjectResult Add(BookPublisherDto bpDto)
        {
            _bpRepository.Add(new BookPublisher(bpDto.BookId, bpDto.PublisherId));
            _bpRepository.SaveChanges();

            return Ok("Added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, BookPublisherDto bpDto)
        {
            var bp = _bpRepository.GetById(id);
            if (bp == null)
            {
                return NotFound("BookPublisher cannot be found.");
            }
            bp.BookId = bpDto.BookId;
            bp.PublisherId = bpDto.PublisherId;
            _bpRepository.Update(bp);
            _bpRepository.SaveChanges();
            return Ok("Updated succesfully.");
        }



        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var bp = _bpRepository.GetById(id);
            if (bp == null)
            {
                return NotFound("BookPublisher cannot be found.");
            }
            _bpRepository.Remove(bp);
            _bpRepository.SaveChanges();
            return Ok("BookPublisher is deleted succesfully.");

        }
    }
}
