namespace Cinephila.DataAccess.Entities
{
    public class ImageEntity : BaseEntity
    {
        public string Path { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

        public int ParticipantID { get; set; }

        public virtual ParticipantEntity Participant { get; set; }
    }
}
