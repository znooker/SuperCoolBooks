namespace SuperCoolBooks.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public int Rating { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public int BookId { get; set; }

        public string ReviewText { get; set; }

        public virtual Book Book { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
