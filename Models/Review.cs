using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdavnicaZaKnigi.Models
{
    public class Review
    {
        public int? Id { get; set; }

        [Display(Name = "Book")]
        public int? BookId { get; set; }
        public Book? Book { get; set; }

        [StringLength(450)]
        public string? AppUser { get; set; }

        [StringLength(500)]
        [Display(Name = "Comment")]
        public string? Comment { get; set; }

    
        public int? Rating { get; set; }
    }
}