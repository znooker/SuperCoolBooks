﻿namespace SuperCoolBooks.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
