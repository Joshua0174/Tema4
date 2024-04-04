using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        
        private readonly IRepository<Author> _authorRepository;
        public AuthorController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }


        [HttpGet("get")]
        public IEnumerable<Author> Get()
        {
            return _authorRepository.GetAll();
        }

        [HttpPost("add")]
        public ObjectResult Add(AuthorDto authorDto)
        {
            _authorRepository.Add(new Author(authorDto.Name, authorDto.PublisherId));
            _authorRepository.SaveChanges();

            return Ok("Added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, AuthorDto authorDto)
        {
            var author=_authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound("User cannot be found.");
            }
            author.PublisherId = authorDto.PublisherId;
            author.Name = authorDto.Name;
            _authorRepository.Update(author);
            _authorRepository.SaveChanges();
            return Ok("Updated succesfully."); 
        }   



        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            var author= _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound("Author cannot be found.");
            }

            _authorRepository.Remove(author);
            _authorRepository.SaveChanges();
            return Ok("Author is deleted succesfully.");

        }
    }
}
