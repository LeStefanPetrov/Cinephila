using System.Runtime.Serialization;

namespace Cinephila.Domain.DTOs.ApiDTOs
{
    [DataContract]
    public class ParticipantDto
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
