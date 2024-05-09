using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProdavnicaZaKnigi.Models;
using System.ComponentModel.DataAnnotations;
namespace ProdavnicaZaKnigi.ViewModels
{
    public class CreateBookViewModel
    {
        public Book book { get; set; }  
        public IFormFile? CoverImage { get; set; }
        public IFormFile? ElectronicVersion { get; set; }

        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
