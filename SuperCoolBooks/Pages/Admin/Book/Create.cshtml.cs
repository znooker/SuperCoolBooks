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
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public CreateModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
