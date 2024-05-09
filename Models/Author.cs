using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavnicaZaKnigi.Models
{
    public class Author
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Nationality")]
        public string? Nationality { get; set; }

        [StringLength(50)]
        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Author")]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public ICollection<Book>? Books { get; set; }
    }
}


