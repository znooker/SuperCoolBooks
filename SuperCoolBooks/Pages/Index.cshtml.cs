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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SuperCoolBooks.Pages
{
    public class IndexModel : PageModel
    {



        // med hjälp av books-copy
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public IndexModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        public IList<Models.Book> Books { get; set; } = default!;
        public Book RandomBook { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {

                Books = await _context.Books
                  .Include(e => e.AuthorBooks)
                    .ThenInclude(e => e.Author)
                .ToListAsync();

                var random = new Random();
                var count = _context.Books.Count(); //count the number of books i the database
                var index = random.Next(count); //generate a random number in the intervall of number of books 
                RandomBook = _context.Books
                .Include(e => e.AuthorBooks)
                        .ThenInclude(e => e.Author)
                    .Skip(index)
                    .FirstOrDefault();

            }

        }

        
    }

}






