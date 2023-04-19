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
    public class LabCreateModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public LabCreateModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Book Book { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> Authors { get; set; }

        [BindProperty]
        public List<int> GenreId { get; set; }
        [BindProperty]
        public int AuthorId { get; set; }

        [BindProperty]
        public List<SelectListItem> Genres { get; set; }


        public async Task<IActionResult> OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
        
            Authors = await _context.Authors.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = $"{a.FirstName} {a.LastName}"
            }).ToListAsync();

            Genres = await _context.Genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = $"{g.Title}"
            }).ToListAsync();


            return Page();

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Models.Book book)
        {
            var test = Request.Form["testAId"];

          if (!ModelState.IsValid || _context.Books == null || Book == null)
            {
                ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
                return Page();
            }
                   

            _context.Books.Add(Book);

            await _context.SaveChangesAsync();

            //Add connection between book and authors in the JoinTable AuthorBook
            AuthorBook authorBook = new AuthorBook() {AuthorId = AuthorId, BooksBookId = Book.BookId};
            _context.AuthorBooks.Add(authorBook);

            List<BookGenre> glist = new List<BookGenre>();
            foreach (var id in GenreId)
            {
                glist.Add(new BookGenre { BooksBookId = Book.BookId, GenresGenreId = id });
            }
            
            _context.BookGenres.AddRange(glist);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
