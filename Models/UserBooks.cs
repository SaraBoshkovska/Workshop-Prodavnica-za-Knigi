using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdavnicaZaKnigi.Models
{
    public class UserBooks
    {
        public int? Id { get; set; }

        [Display(Name = "User")]
        [StringLength(450)]
        public string? AppUser { get; set; }


        [Display(Name = "Books")]
        public int? BookId { get; set; }
        public Book? Book { get; set; }

    }
}