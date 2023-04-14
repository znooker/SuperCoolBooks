using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Admin.Genre
{
    public class DeleteModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public DeleteModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Genre Genre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(m => m.GenreID == id);

            if (genre == null)
            {
                return NotFound();
            }
            else 
            {
                Genre = genre;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }
            var genre = await _context.Genres.FindAsync(id);

            if (genre != null)
            {
                Genre = genre;
                _context.Genres.Remove(Genre);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
