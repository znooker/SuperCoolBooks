namespace SuperCoolBooks.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public string? ImagePath { get; set; } //Needed to make it nullable otherwise validation didnt work

        //Navigation Props
        public virtual ICollection<Book> BooksBooks { get; } = new List<Book>();
    }
}
