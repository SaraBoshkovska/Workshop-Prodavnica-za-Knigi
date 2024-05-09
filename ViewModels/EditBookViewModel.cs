using Microsoft.AspNetCore.Mvc.Rendering;
using ProdavnicaZaKnigi.Models;

namespace ProdavnicaZaKnigi.ViewModels
{
    public class EditBookViewModel
    {
        public Book Book { get; set; }
        public IFormFile? NewCoverImage { get; set; }
        public IFormFile? NewElectronicVersion { get; set; }

        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
