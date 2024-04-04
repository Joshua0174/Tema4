using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace DataLayer
{
    // dotnet ef migrations add Initial
    // dotnet ef database update
    public class MyDbContext : DbContext
    {
        private readonly string _windowsConnectionString = @"Server=.\SQLExpress;Database=Tema;Trusted_Connection=True;TrustServerCertificate=true";
        //private readonly string _windowsConnectionString = @"Server=localhost\SQLEXPRESS;Database=Lab5Database1;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookPublisher> bookPublishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Seed(builder);
            base.OnModelCreating(builder);
             
            builder.Entity<Author>()   //one to one -- fiecare autor are un publisher si viceversa
                .HasOne(a => a.Publisher)
                .WithOne(p => p.Author)
                .HasForeignKey<Publisher>(p => p.AuthorId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<Book>()  //one to many -- un autor poate avea mai multe carti
                .HasOne(a => a.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Publisher>();
            
            builder.Entity<BookPublisher>()                        //|
                .HasKey(bp => new { bp.PublisherId, bp.BookId });  //|
                                                                   //|
            builder.Entity<BookPublisher>()                        //|
                .HasOne(bp => bp.Book)                             //|
                .WithMany(b => b.BookPublishers)                   //|
                .HasForeignKey(bp => bp.PublisherId)               //|
                .OnDelete(DeleteBehavior.NoAction);                //| many to many intre book si publisher
                                                                   //|
        }
         //Am incercat sa fac si ultima cerinta, cea cu seeding, dar nu am reusit. In metoda seed ar trebui sa folosesc constructorii pentru fiecare model, insa nu
         //cred ca e destul.
        //protected void Seed(ModelBuilder modelBuilder)
        //{
        //    Author author1, author2;

        //      modelBuilder.Entity<Author>().HasData(
        //       author1= new Author { Id = Guid.NewGuid(), Name = "Author1", PublisherId = Guid.NewGuid() },
        //       author2=new Author { Id = Guid.NewGuid(), Name = "Author2", PublisherId = Guid.NewGuid() }
        //// Adaugă alți autori dacă este nevoie
        //);
  
        // Publisher publisher1, publisher2; 
        //    modelBuilder.Entity<Publisher>().HasData(
        //       publisher1= new Publisher { Id = Guid.NewGuid(), Name = "Publisher1", AuthorId = Guid.NewGuid() },
        //       publisher2=new Publisher { Id = Guid.NewGuid(), Name = "Publisher2", AuthorId = Guid.NewGuid() }
        //        // Adaugă alți editori dacă este nevoie
        //    );
        //    Book book1, book2;
        //    modelBuilder.Entity<Book>().HasData(
        //       book1=new Book { Id = Guid.NewGuid(), Title = "Book1", AuthorId =author1.Id},
        //       book2= new Book { Id = Guid.NewGuid(), Title = "Book2", AuthorId = author2.Id}
        //        // Adaugă alte cărți dacă este nevoie
        //    );

        //    modelBuilder.Entity<BookPublisher>().HasData(
        //        new BookPublisher { BookId = book1.Id, PublisherId = publisher1.Id },
        //        new BookPublisher { BookId = book2.Id, PublisherId = publisher2.Id }
        //        // Adaugă alte asocieri între cărți și editori dacă este nevoie
        //    );  
        //}
    }
}
