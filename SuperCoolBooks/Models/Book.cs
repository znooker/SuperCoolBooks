﻿namespace SuperCoolBooks.Models
{
    public partial class Book
    {
        public int BookId { get; set; }

        public string? UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

        public string? ImagePath { get; set; }

        public bool? isDeleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; } = new List<AuthorBook>();

        public virtual ICollection<BookGenre> BookGenres { get; } = new List<BookGenre>();

        public virtual ICollection<Review> Reviews { get; } = new List<Review>();

        public virtual AspNetUser? User { get; set; }
    }
}
