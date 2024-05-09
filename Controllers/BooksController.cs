using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cstore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProdavnicaZaKnigi.Models;
using ProdavnicaZaKnigi.ViewModels;

namespace ProdavnicaZaKnigi.Controllers
{
    public class BooksController : Controller
    {
        private readonly ProdavnicaZaKnigiContext _context;
        private readonly IBufferedFileUploadService _bufferedFileUploadService;

        public BooksController(ProdavnicaZaKnigiContext context, IBufferedFileUploadService bufferedFileUploadService)
        {
            _context = context;
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        // GET: Books
       public async Task<IActionResult> Index(string TitleFilter, int? selectedAuthorId, int? selectedGenreId)
        {
            IQueryable<Book> book = _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .Include(b=>b.Reviews)
                .AsQueryable();
           

            if (!string.IsNullOrEmpty(TitleFilter))
            {
                book = book.Where(b => b.Title.Contains(TitleFilter));
            }
            if (selectedAuthorId.HasValue)
            {
                book = book.Where(b => b.AuthorId == selectedAuthorId);
            }
            if (selectedGenreId.HasValue)
            {
                book = book.Where(b => b.BookGenres.Any(bg => bg.GenreId == selectedGenreId));
            }

            var viewModel = new BookFilterViewModel
            {
                Books = await book.ToListAsync(),
                Authors = new SelectList(_context.Author, "Id", "FullName"),
                Genres = new SelectList(_context.Genre, "Id", "GenreName"),
                TitleFilter = TitleFilter,
                SelectedAuthorId = selectedAuthorId,
                SelectedGenreId = selectedGenreId
            };

            return View(viewModel);

        }


        // GET: Books/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        // GET: Books/Create
        public async Task<IActionResult> Create()
        {

            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName");

            var genres = await _context.Genre.OrderBy(s => s.GenreName).ToListAsync();

            CreateBookViewModel viewmodel = new CreateBookViewModel
            {
                book = new Book(),
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = new List<int>()
            };

            return View(viewmodel);


        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel viewModel, IFormFile DownloadUrl, IFormFile FrontPage )
        {

            if (ModelState.IsValid)
            {
             
                Book book = new Book
                {
                    Title = viewModel.book.Title,
                    AuthorId = viewModel.book.AuthorId,
                    YearPublished = viewModel.book.YearPublished,
                    NumPages = viewModel.book.NumPages,
                    Description = viewModel.book.Description,
                    Publisher = viewModel.book.Publisher,
                };

            
                await _bufferedFileUploadService.UploadFile(DownloadUrl);
                book.DownloadUrl = "images/" + DownloadUrl.FileName;

                await _bufferedFileUploadService.UploadFile(FrontPage);
                book.FrontPage = "images/" + FrontPage.FileName;

                // Add selected genres to the book
                if (viewModel.SelectedGenres != null)
                {
                    foreach (int genreId in viewModel.SelectedGenres)
                    {
                        book.BookGenres.Add(new BookGenre { GenreId = genreId, Book = book });
                    }
                }

              
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

           
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", viewModel.book.AuthorId);
            var genres = await _context.Genre.OrderBy(s => s.GenreName).ToListAsync();
            viewModel.GenreList = new MultiSelectList(genres, "Id", "GenreName");
            return View(viewModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Book.Where(b => b.Id == id).Include(b => b.BookGenres).First();
            if (book == null)
            {
                return NotFound();
            }
            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(s => s.GenreName);
            EditBookViewModel viewmodel = new EditBookViewModel
            {
                Book = book,
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = book.BookGenres.Select(sg => sg.GenreId)
            };
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", book.AuthorId);
            return View(viewmodel);
        }


        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookViewModel viewmodel)
        {
            if (id != viewmodel.Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                    _context.Update(viewmodel.Book);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> newGenreList = viewmodel.SelectedGenres;
                    IEnumerable<int> prevGenreList = _context.BookGenre.Where(s => s.BookId == id).Select(s => s.GenreId);
                    IQueryable<BookGenre> toBeRemoved = _context.BookGenre.Where(s => s.BookId == id);
                    if (newGenreList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newGenreList.Contains(s.GenreId));
                        foreach (int genreId in newGenreList)
                        {
                            if (!prevGenreList.Any(s => s == genreId))
                            {
                                _context.BookGenre.Add(new BookGenre { GenreId = genreId, BookId = id });
                            }
                        }
                    }
                    if (viewmodel.NewElectronicVersion != null)
                    {
                    await _bufferedFileUploadService.UploadFile(viewmodel.NewElectronicVersion);
                    viewmodel.Book.DownloadUrl = "/images/" + viewmodel.NewElectronicVersion.FileName;
                    }

                    if (viewmodel.NewCoverImage != null)
                    {
                    await _bufferedFileUploadService.UploadFile(viewmodel.NewCoverImage);
                    viewmodel.Book.FrontPage = "/images/" + viewmodel.NewCoverImage.FileName;
                    }
                _context.BookGenre.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", viewmodel.Book.AuthorId);
            return View(viewmodel);
        }
      
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
