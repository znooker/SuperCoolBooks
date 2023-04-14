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

        public async Task<IActionResult> OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            ViewData["GenerId"] = new SelectList(_context.Genres, "GenreId", "GenreId");
                 MyBook = await _context.Books
                .Include(a=> a.Author)
                .Include(e => e.Genres)
                .AsNoTracking()
                .ToListAsync();
            return Page();
        }
        public List<Models.Book> MyBook { get; set; }
        [BindProperty]
        public Models.Book Book { get; set; } = default!;
        [BindProperty]
        public Models.Author Author { get; set; } = default!;
        [BindProperty]
        public Models.Genre Genre { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Books == null || Book == null)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
