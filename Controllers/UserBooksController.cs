using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProdavnicaZaKnigi.Areas.Identity.Data;
using ProdavnicaZaKnigi.Models;

namespace ProdavnicaZaKnigi.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly ProdavnicaZaKnigiContext _context;
        private readonly UserManager<ProdavnicaZaKnigiUser> _userManager;

        public UserBooksController(ProdavnicaZaKnigiContext context, UserManager<ProdavnicaZaKnigiUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }
        private Task<ProdavnicaZaKnigiUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: UserBooks
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var ProdavnicaZaKnigiContext = _context.UserBooks.Include(u => u.Book);
            return ProdavnicaZaKnigiContext != null ?
                         View(await ProdavnicaZaKnigiContext.ToListAsync()) :
                         Problem("Entity set 'ProdavnicaZaKnigiContext.UserBooks'  is null.");
        }

        // GET: UserBooks/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddBookBought(int? bookid)
        {
            if (bookid == null)
            {
                return NotFound();
            }
            var ProdavnicaZaKnigiUserContext = _context.UserBooks.Where(r => r.BookId == bookid);
                
            var user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                UserBooks userbook = new UserBooks();
                
                userbook.AppUser = user.UserName;
                userbook.BookId = (int)bookid;
                _context.UserBooks.Add(userbook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyBooksList));
            }
            return ProdavnicaZaKnigiUserContext != null ?
                          View(await ProdavnicaZaKnigiUserContext.ToListAsync()) :
                          Problem("Entity set 'ProdavnicaZaKnigiContext.UserBooks'  is null.");
        }
        [Authorize(Roles = "User")]

        public async Task<IActionResult> MyBooksList()
        {
            var user = await GetCurrentUserAsync();
            var UserBooksContext = _context.UserBooks.AsQueryable().Where(r => r.AppUser == user.UserName).Include(r => r.Book).ThenInclude(p => p.Author);
            var products_ofcurrentuser = _context.Book.AsQueryable(); ;
            products_ofcurrentuser = UserBooksContext.Select(p => p.Book);
            return UserBooksContext != null ?
                          View("~/Views/UserBooks/KupeniKnigi.cshtml", await products_ofcurrentuser.ToListAsync()) :
                          Problem("Entity set 'ProdavnicaZaKnigiContext.UserBooks'  is null.");
        }

        // GET: UserBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            return View();
        }

        // POST: UserBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppUser,BookId")] UserBooks userBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", userBooks.BookId);
            return View(userBooks);
        }

        // GET: UserBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks.FindAsync(id);
            if (userBooks == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", userBooks.BookId);
            return View(userBooks);
        }

        // POST: UserBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,AppUser,BookId")] UserBooks userBooks)
        {
            if (id != userBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBooksExists(userBooks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", userBooks.BookId);
            return View(userBooks);
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteOwnedProduct(int? productid)
        {
            if (productid == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            var userProduct = await _context.UserBooks.Include(p => p.Book).AsQueryable().FirstOrDefaultAsync(m => m.AppUser == user.UserName && m.BookId == productid);
            if (userProduct == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/Delete.cshtml", userProduct);
        }

        // GET: UserBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBooks == null)
            {
                return NotFound();
            }

            return View(userBooks);
        }

        // POST: UserBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var userBooks = await _context.UserBooks.FindAsync(id);
            if (userBooks != null)
            {
                _context.UserBooks.Remove(userBooks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBooksExists(int? id)
        {
            return _context.UserBooks.Any(e => e.Id == id);
        }
    }
}
