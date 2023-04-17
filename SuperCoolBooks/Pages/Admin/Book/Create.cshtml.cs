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

<<<<<<< Updated upstream
        public IActionResult Index()
        {
            var books = _context.Books.ToList();

            return View(books);
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            ViewData["GenerId"] = new SelectList(_context.Genres, "GenreId", "GenreId");

            return Page();
        }
        [BindProperty]
        //public Models.Book Book { get; set; } = default!;
        public List<Models.Book> Book { get; set; } = new List<Models.Book>();
        [BindProperty]
        public Models.Genre Genre { get; set; }
        [BindProperty]
        public Models.Author Author { get; set; }
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Genre = await _context.Genres.FindAsync(Genre.GenreId);
            Author = await _context.Authors.FindAsync(Author.AuthorId);
            List <Models.Book> Book = await _context.Books.Include(b => b.Author)
            .Include(c => c.Genres)
            .ToListAsync();
             //.FirstOrDefaultAsync();

            //Book.Author = new Models.Author();

=======
        [BindProperty]
        public Models.Book Book { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
>>>>>>> Stashed changes
            if (!ModelState.IsValid)
            {
                return Page();
            }

<<<<<<< Updated upstream
            _context.Attach(Book).State = EntityState.Added;
=======
            // Add the book to the database
            _context.Books.Add(Book);
>>>>>>> Stashed changes
            await _context.SaveChangesAsync();

            // Add the genres and authors to the book
            foreach (var genre in Book.Genres)
            {
                if (_context.Entry(Book).Collection(b => b.Genres).Query().FirstOrDefault(g => g.GenreId == genre.GenreId) == null)
                {
                    _context.Add(genre);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}