using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaZaKnigi.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(50)]

        public string GenreName { get; set; }

        public ICollection<BookGenre>? BookGenres { get; set; }
    }
}