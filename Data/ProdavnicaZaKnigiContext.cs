using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaZaKnigi.Models
{
    public class ProdavnicaZaKnigiContext : DbContext
    {
        public ProdavnicaZaKnigiContext (DbContextOptions<ProdavnicaZaKnigiContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Review> Review { get; set; }
        public DbSet<UserBooks> UserBooks { get; set; }

    }
}
