using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages
{
    public class IndexModel : PageModel
    {



    
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public IndexModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        public IList<Models.Book> Books { get; set; } = default!;
        public Book RandomBook { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                if (_context.Books != null)
                {
                    Books = await _context.Books
                    .Include(e => e.AuthorBooks)
                    .ThenInclude(e => e.Author)
                    .ToListAsync();

                    var random = new Random();
                    var count = _context.Books.Count();
                    if (count >= 1)
                    {
                        var index = random.Next(count);
                        RandomBook = _context.Books
                            .Include(e => e.AuthorBooks)
                            .ThenInclude(e => e.Author)
                             .Include(e => e.BookGenres)
                            .ThenInclude(e => e.GenresGenre)
                            .Skip(index)
                            .FirstOrDefault();

                    }
                }
            }
            else 
            {
                Books = await _context.Books
                   .Where(e => e.Title.Contains(SearchString) ||
                            e.AuthorBooks.Any(e => e.Author.FirstName.Contains(SearchString)
                            || e.Author.LastName.Contains(SearchString))
                            ||e.BookGenres.Any(e => e.GenresGenre.Title.Contains(SearchString)))
                  .Include(e => e.BookGenres)
                  .ThenInclude(e => e.GenresGenre)
                  .Include(e => e.AuthorBooks)
                  .ThenInclude(e => e.Author)
                  .ToListAsync();

            
            }

        }

   
    }




}

