using System.Collections.Generic;

namespace ApplicationCore.Models.Response
{
    public class ReviewResponseModel
    {
        public int UserId { get; set; }
        public List<ReviewMovieResponseModel> MovieReviews { get; set; }
    }
}