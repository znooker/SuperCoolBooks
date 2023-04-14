using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Admin.Author
{
    public class CreateModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public CreateModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Author Author { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Authors == null || Author == null)
            {
                return Page();
            }

            _context.Authors.Add(Author);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
