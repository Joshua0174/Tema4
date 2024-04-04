using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Publisher
    {
        public Publisher(string name, Guid authorId)
        {
            Name = name;
            AuthorId = authorId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
        public ICollection<BookPublisher> BookPublishers { get; set; }
    }
}
