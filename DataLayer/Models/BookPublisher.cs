using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class BookPublisher
    {
        public BookPublisher(Guid bookId, Guid publisherId)
        {
            BookId = bookId;
            PublisherId = publisherId;
        }
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public Publisher Publisher { get; set; }
        public Guid PublisherId { get; set; }
    }
}
