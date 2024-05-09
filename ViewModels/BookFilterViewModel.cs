using Microsoft.AspNetCore.Mvc.Rendering;
using ProdavnicaZaKnigi.Models;
using System.Collections.Generic;

namespace ProdavnicaZaKnigi.ViewModels
{
    public class BookFilterViewModel
    {
        public IList<Book> Books { get; set; }

        public SelectList Authors { get; set; }

        public SelectList Genres { get; set; }

        public string TitleFilter { get; set; }

        public int? SelectedAuthorId { get; set; }

        public int? SelectedGenreId { get; set; }

    }
}
