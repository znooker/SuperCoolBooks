using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Books
{
    public class EditReviewModel : PageModel
    {
        private readonly SuperCoolBooksContext _context;
        
        public EditReviewModel(SuperCoolBooksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);

            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Review.ReviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return RedirectToPage("Books/Details", new { id = Review.BookId });
            return RedirectToPage("/Books/Details", new { id = Review.BookId });
        }
        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.ReviewId == id);
        }
    }
}
