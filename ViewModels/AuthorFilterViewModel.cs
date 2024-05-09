using Microsoft.AspNetCore.Mvc.Rendering;
using ProdavnicaZaKnigi.Models;

namespace ProdavnicaZaKnigi.ViewModels
{
    public class AuthorFilterViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        public int? SelectedAuthorId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}