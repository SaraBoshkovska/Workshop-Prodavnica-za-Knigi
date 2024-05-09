using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProdavnicaZaKnigi.Models
{
    public class Book
    {
        public int Id { get; set; }


        [StringLength(100)]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }

        public Book()
        {
            BookGenres = new List<BookGenre>();
        }

        [Display(Name = "Year Published")]
        public int? YearPublished { get; set; }

        [Display(Name = "Pages")]
        public int? NumPages { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [StringLength(50)]

        public string? Publisher { get; set; }


        public string? FrontPage { get; set; } = "";

        public string? DownloadUrl { get; set; } = "";


        public ICollection<Review>? Reviews { get; set; }

        public ICollection<UserBooks>? UserBooks { get; set; }

        [Display(Name ="Average Rating")]
        public double? AverageRating()
        {
            if (Reviews != null && Reviews.Any() && Reviews.All(r => r.Rating != null))
            {
                int total = (int)Reviews.Sum(r => r.Rating);
                return (double)total / Reviews.Count;
            }

            return 0;
        }

    }
}
