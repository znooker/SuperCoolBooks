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

        public List<Review> Reviews { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            //var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            var book = await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(m => m.BookId == id);

            //var reviews = await _context.Reviews.Where(r => r.BookId == id);

            //var reviews = await _context.Reviews;
            var reviews = _context.Reviews.Where(r => r.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
                Review = new Review { BookId = book.BookId };
                ViewData["Book"] = book;

                //Reviews = reviews.ToList();
                Reviews = reviews.Where(r => r.BookId == Book.BookId && r.IsDeleted != true).ToList();
            }
            
            
            
            return Page();
        }

        //Posting a Review
        public async Task<IActionResult> OnPostAsyncReview()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            //Redirect to the same page, Return Page(); would not have any data remaining after submitting review
            return RedirectToPage("/Books/Details", new { id =Review.BookId });
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Books/Details", new { id = review.BookId });
        }
    }
}
