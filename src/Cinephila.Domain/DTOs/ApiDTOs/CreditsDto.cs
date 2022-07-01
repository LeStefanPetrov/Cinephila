using System.Runtime.Serialization;

namespace Cinephila.Domain.DTOs.ApiDTOs
{
    [DataContract]
    public  class CreditsDto
    {
        [DataMember(Name = "cast")]
        public ParticipantDto[] Participants { get; set; }
    }
}
