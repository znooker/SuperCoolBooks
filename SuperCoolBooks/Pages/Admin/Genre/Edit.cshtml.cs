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

namespace SuperCoolBooks.Pages.Admin.Genre
{
    public class EditModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public EditModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
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

            var genre =  await _context.Genres.FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }
            Genre = genre;
            return Page();
        }
        /// <summary>
        /// Needs fixing, can't update title correctly
        /// </summary>
        /// <returns></returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // check if a genre with the same title already exists in the database
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Title == Genre.Title);

            //check if the genre beeing edited has the same id as the one found in the database
            if (existingGenre != null && Genre.GenreId == existingGenre.GenreId)
            {

                //update the existing genre record
                existingGenre.Title = Genre.Title;
                existingGenre.Description = Genre.Description;

                // save changes to the database
                await _context.SaveChangesAsync();

                // redirect to the genre list page
                return RedirectToPage("./Index");
            }

            //if no matching title found, Create that genre
            else if (existingGenre is null)
            {
                // Create a new genre record
                var newGenre = new Models.Genre {Title = Genre.Title, Description = Genre.Description };

                // Add the new genre to the context
                _context.Add(newGenre);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect to the genre list page
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError("Genre.Title", "A genre with that title already exists!");
                return Page();
            }

        }



    }
}
