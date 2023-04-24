using System.ComponentModel.DataAnnotations.Schema;

namespace SuperCoolBooks.Models
{
    public class ReviewFeedback
    {
        public int ReviewFeedBackId { get; set; }
        public bool? IsHelpful { get; set; }
        public bool? HasFlagged { get; set; }

        //Foreign keys
        public string UserId { get; set; }
        public int ReviewId { get; set; }

        //Navigation props
        public virtual AspNetUser? User { get; set; }
        public virtual Review? Review { get; set; }
    }
}
