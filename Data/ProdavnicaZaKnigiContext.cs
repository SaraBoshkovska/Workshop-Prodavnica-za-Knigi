using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ProdavnicaZaKnigi.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaZaKnigi.Models
{
    public class ProdavnicaZaKnigiContext : IdentityDbContext<ProdavnicaZaKnigiUser>

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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
