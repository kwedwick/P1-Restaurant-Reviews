using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P1RestaurantReviewer.DataAccess.Entities;
using P1RestaurantReviewer.Domain;

namespace P1RestaurantReviewer.DataAccess
{
    public class ReviewRepo : IReviewRepo
    {
        private restaurantreviewerContext _context;

        public ReviewRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        public List<Domain.Review> GetAllReviews()
        {
            return _context.Reviews.Select(
                review => new Domain.Review(review.Id, review.TimeCreated, review.Title, review.Body, review.Rating)
                ).ToList();
        }

        public Domain.Review CreateReview(Domain.Review review)
        {
            var newEntity = new Entities.Review
            {
                Title = review.Title,
                Body = review.Body,
                Rating = review.Rating
            };
            _context.Reviews.Add(newEntity);
            _context.SaveChanges();
            review.Id = newEntity.Id;

            _context.ReviewJoins.Add(
                new Entities.ReviewJoin
                {
                    UserId = review.UserId,
                    RestaurantId = review.RestaurantId,
                    ReviewId = review.Id
                }
            );
            _context.SaveChanges();

            return review;
        }
        /// <summary>
        /// First filter ReviewJoins by Restaurant, then join in Review data pass info to second join and then add in username. Need to change. UserId to string so we can push in the user's username as string
        /// </summary>
        /// <param name="id"></param>
        /// <returns>restuarantReviews</returns>
        public List<Domain.Review> GetReviewsbyRestaurantId(int id)
        {

            List<Domain.Review> restuarantReviews = _context.ReviewJoins
            .Where(reviewJoin => reviewJoin.RestaurantId == id)
            .Join(
                _context.Reviews,
                reviewJoin => reviewJoin.ReviewId,
                review => review.Id,
                (reviewJoin, review) => new Domain.Review
                {
                    Id = review.Id,
                    Title = review.Title,
                    Body = review.Body,
                    Rating = review.Rating,
                    UserId = reviewJoin.UserId
                }
            )
            .Join(
                _context.Users,
                reviewJoin => reviewJoin.UserId,
                userJoin => userJoin.Id,
                (reviewJoin, userJoin) => new Domain.Review
                {
                    Id = reviewJoin.Id,
                    Title = reviewJoin.Title,
                    Body = reviewJoin.Body,
                    Rating = reviewJoin.Rating,
                    Username = userJoin.Username
                }
            )
            .ToList();

            if (restuarantReviews != null)
            {
                return restuarantReviews;
            }
            return new List<Domain.Review>();

        }
        /// <summary>
        /// Gets logged in users reviews
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Domain.Review> GetMyReviews(int id)
        {
            List<Domain.Review> restuarantReviews = _context.ReviewJoins
           .Where(userReviews => userReviews.UserId == id)
           .Join(
               _context.Reviews,
               reviewJoin => reviewJoin.ReviewId,
               review => review.Id,
               (reviewJoin, review) => new Domain.Review
               {
                   Id = review.Id,
                   Title = review.Title,
                   Body = review.Body,
                   Rating = review.Rating,
                   RestaurantId = reviewJoin.RestaurantId
               }
           )
           .Join(
               _context.Restaurants,
               reviewJoin => reviewJoin.RestaurantId,
               restaurantJoin => restaurantJoin.Id,
               (reviewJoin, restaurantJoin) => new Domain.Review
               {
                   Id = reviewJoin.Id,
                   Title = reviewJoin.Title,
                   Body = reviewJoin.Body,
                   Rating = reviewJoin.Rating,
                   RestaurantName = restaurantJoin.Name
               }
           )
           .ToList();

            if (restuarantReviews != null)
            {
                return restuarantReviews;
            }
            return new List<Domain.Review>();
        }
    }
}
