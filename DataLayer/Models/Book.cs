using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Book
    {
        public Book(string title, Guid authorId)
        {
            Title = title;
            AuthorId = authorId;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }  
        public ICollection<BookPublisher> BookPublishers { get; set; }
    }
}
