using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Admin.Book
{
    public class CreateModel : PageModel
    {
        private readonly SuperCoolBooksContext _context;

        public CreateModel(SuperCoolBooksContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            ViewData["GenerId"] = new SelectList(_context.Genres, "GenreId", "GenreId");

            return Page();
        }
        [BindProperty]
        public Models.Book Book { get; set; }
        [BindProperty]
        public List <Models.Genre> Genre { get; set; }
        [BindProperty]
        public List <Models.Author> Author { get; set; }
       
        public async Task<IActionResult> OnPostAsync()
        {
                //Genre = await _context.Genres.FindAsync(Genre.GenreId);
                //Author = await _context.Authors.FindAsync(Author.AuthorId);
                List<Models.Book> Book = await _context.Books.Include(b => b.AuthorBooks)
                .Include(c => c.BookGenres)
                .ToListAsync();
                //.FirstOrDefaultAsync();
                //.FirstOrDefaultAsync();

                //Book.Author = new Models.Author();

                if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Added;
            //_context.Books.Add(Book);
            await _context.SaveChangesAsync();
            /*
            foreach (var genre in Book.Genres)
            {
                if (_context.Entry(Book).Collection(b => b.Genres).Query().FirstOrDefault(g => g.GenreId == genre.GenreId) == null)
                {
                    _context.Add(genre);
                }
            }
            */
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}