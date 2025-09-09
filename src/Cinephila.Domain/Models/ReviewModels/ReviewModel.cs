using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.Models.ReviewModels
{
    public class ReviewModel : IReview
    {
        public int ProductionID { get; set; }

        public int UserID { get; set; }

        public string UserReview { get; set; }

        public int Rating { get; set; }
    }
}
