namespace SuperCoolBooks.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public string ImagePath { get; set; }

        //Navigation Props
        public ICollection<Book> Books { get; set; }
    }
}
