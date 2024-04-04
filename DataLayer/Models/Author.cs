using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Author
    {
        public Author(string name, Guid publisherId)
        {
            Name = name;
            PublisherId = publisherId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Publisher Publisher { get; set; }
        public Guid PublisherId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
