﻿namespace SuperCoolBooks.Models
{
    public partial class Genre
    {
        public int GenreId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; } = new List<BookGenre>();
    }
}

