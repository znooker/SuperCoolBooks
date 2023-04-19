using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public DetailsModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        [BindProperty]
        public Review Review { get; set; }
        public int BookId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
                Review = new Review { BookId = book.BookId };
                ViewData["Book"] = book;
            }
            
            
            return Page();
        }

        //Code to be added under here
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Books/Details", new { id =Review.BookId });
        }
    }
}
