using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.DTOs.ReviewDTOs
{
    public class Review : IReview
    {
        public int ProductionID { get; set; }

        public int UserID { get; set; }

        public int Rating { get; set; }

        public string UserReview { get; set; }
    }
}
