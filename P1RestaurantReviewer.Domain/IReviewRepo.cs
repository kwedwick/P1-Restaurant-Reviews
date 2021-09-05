using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
    public interface IReviewRepo
    {
        List<Review> GetAllReviews();

        Review CreateReview(Review review);

        List<Review> GetReviewsbyRestaurantId(int id);

        List<Review> GetMyReviews(string id);

        Review UpdateReview(Review review);

        Review GetReviewById(int id);
    }
}
