using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : Controller
    {
        private readonly IRepository<Publisher> _publisherContext;
        public PublisherController(IRepository<Publisher> publisherRepository)
        {
            _publisherContext = publisherRepository;
        }


        [HttpGet("get")]
        public IEnumerable<Publisher> Get()
        {
            return _publisherContext.GetAll();
        }

        [HttpPost("add")]
        public ObjectResult Add(PublisherDto publisherDto)
        {
            _publisherContext.Add(new Publisher(publisherDto.Name, publisherDto.AuthorId));
            _publisherContext.SaveChanges();

            return Ok("Added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, PublisherDto publisherDto)
        {
            var publisher = _publisherContext.GetById(id);
            if (publisher == null)
            {
                return NotFound("Publisher cannot be found.");
            }
            publisher.AuthorId = publisherDto.AuthorId;
            publisher.Name = publisherDto.Name;
            _publisherContext.Update(publisher);
            _publisherContext.SaveChanges();
            return Ok("Updated succesfully.");
        }



        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var publisher = _publisherContext.GetById(id);
            if (publisher == null)
            {
                return NotFound("Publisher cannot be found.");
            }

            _publisherContext.Remove(publisher);
            _publisherContext.SaveChanges();
            return Ok("Publisher is deleted succesfully.");

        }
    }
}
