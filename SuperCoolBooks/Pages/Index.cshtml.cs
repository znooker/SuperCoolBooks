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



        // med hjälp av books-copy
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public IndexModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        public IList<Models.Book> Book { get; set; } = default!;
        public Book BookR { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.User).ToListAsync();
                var random = new Random();
                var count = _context.Books.Count();
                var index = random.Next(count);
                BookR = _context.Books.Skip(index).FirstOrDefault();
            }

        }

   
    }




}

