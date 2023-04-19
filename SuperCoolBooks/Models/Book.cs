namespace SuperCoolBooks.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }
        public bool isDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime ReleaseDate { get; set; }

        //Navigation Props

        public virtual ICollection<Author> Authors { get; } = new List<Author>();
        public virtual ICollection<Genre> GenresGenres { get; } = new List<Genre>();
        public virtual ICollection<Review> Reviews { get; } = new List<Review>();
        public virtual AspNetUser? User { get; set; }//Id of the user so we know what user made the review, and what users can modify it
    }
}
