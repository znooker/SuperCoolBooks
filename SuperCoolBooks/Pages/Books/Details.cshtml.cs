﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SuperCoolBooks.Data;
using SuperCoolBooks.Models;

namespace SuperCoolBooks.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly SuperCoolBooks.Data.SuperCoolBooksContext _context;

        public DetailsModel(SuperCoolBooks.Data.SuperCoolBooksContext context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        [BindProperty]
        public Review Review { get; set; }
        public int BookId { get; set; }

        //[BindProperty] binding the property messes up leaving a review.
        public ReviewFeedback ReviewFeedback { get; set; }

        public List<Review> Reviews { get; set; }
        public List <ReviewFeedback> ReviewFeedbacks { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            //var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            var book = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.AuthorBooks)
                .ThenInclude(b=>b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(b=>b.GenresGenre)

                .FirstOrDefaultAsync(m => m.BookId == id);

            //var reviews = await _context.Reviews.Where(r => r.BookId == id);

            //var reviews = await _context.Reviews;
            var reviews = _context.Reviews.Where(r => r.BookId == id && r.IsDeleted !=true);
            if (reviews.Any())
            {
                var averageRating = reviews.Average(r => r.Rating);
                //This will only take the rating to the nearest decimal
                var formattedRating = averageRating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                ViewData["AverageRating"] = formattedRating;
            }
            else
            {
                ViewData["AverageRating"] = "No rating yet";
            }
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
                Review = new Review { BookId = book.BookId };
                ViewData["Book"] = book;
                Reviews = reviews.ToList();
            }
            
            
            
            return Page();
        }

        //Posting a Review
        public async Task<IActionResult> OnPostAsyncReview()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            //Redirect to the same page, Return Page(); would not have any data remaining after submitting review
            return RedirectToPage("/Books/Details", new { id =Review.BookId });
        }


        public async Task<IActionResult> OnPostDeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Books/Details", new { id = review.BookId });
        }

        public async Task<IActionResult> OnPostReviewFeedbackAsync(int id, bool? isHelpful, bool? hasFlagged)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // Get the review for which the user is submitting feedback
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            // Get the existing review feedback for this review and user, if it exists
            var reviewFeedback = await _context.ReviewFeedBacks
                .SingleOrDefaultAsync(rf => rf.ReviewId == id && rf.UserId == "1"); // Hardcoded for now

            if (reviewFeedback == null)
            {
                // If no review feedback exists, create a new one
                reviewFeedback = new ReviewFeedback
                {
                    ReviewId = id,
                    UserId = "1", // Hardcoded for now
                    IsHelpful = isHelpful,
                    HasFlagged = hasFlagged
                };

                // Add the new review feedback to the context
                _context.ReviewFeedBacks.Add(reviewFeedback);

                // Update the Upvotes and Downvotes properties
                if (isHelpful == true)
                {
                    review.Upvotes++;
                }
                else
                {
                    review.Downvotes++;
                }
            }
            else
            {
                // If review feedback already exists, update its IsHelpful property
                bool isHelpfulBefore = reviewFeedback.IsHelpful ?? false;
                reviewFeedback.IsHelpful = isHelpful;
                reviewFeedback.HasFlagged = hasFlagged ?? false; //default to null

                // Update the Upvotes and Downvotes properties
                if (isHelpful == true && isHelpfulBefore == false)
                {
                    review.Upvotes++;
                    review.Downvotes--;
                }
                else if (isHelpful == false && isHelpfulBefore == true)
                {
                    review.Downvotes++;
                    review.Upvotes--;
                }
            }

            // Calculate the total number of likes
            review.Likes = review.Upvotes - review.Downvotes;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("/Books/Details", new { id = review.BookId });
        }
        public async Task<IActionResult> OnPostEditReviewAsync(int id)
        {
            Review.ReviewId = id;
            return RedirectToPage("/Books/EditReview", new { id =Review.ReviewId });
        }
    }
}

