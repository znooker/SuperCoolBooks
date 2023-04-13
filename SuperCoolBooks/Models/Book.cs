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

        //public ICollection<Author> Author { get; set; }
        //public ICollection<Genre> Genres { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public AspNetUser? User { get; set; }//Id of the user so we know what user made the review, and what users can modify it
    }
}
