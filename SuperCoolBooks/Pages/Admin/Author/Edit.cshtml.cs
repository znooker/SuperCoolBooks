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

namespace SuperCoolBooks.Pages.Admin.Author
{
    public class EditModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public EditModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            //Check that the uploaded file is a image(png, jpg or jpeg)
            var allowedFileExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(Author.ImagePath); //Gets the file extension of the file
            if (!allowedFileExtensions.Contains(fileExtension)) //Checks if the file does not contain a correct file extension
            {
                ModelState.AddModelError("Author.ImagePath", "File needs to be of type .png, .jpg or .jpeg");
                return Page();
            }

            var author =  await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            Author = author;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.AuthorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AuthorExists(int id)
        {
          return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
