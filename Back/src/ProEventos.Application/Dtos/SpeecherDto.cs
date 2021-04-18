using System.Collections.Generic;

namespace ProEventos.Application.Dtos
{
    public class SpeecherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
        public IEnumerable<EventDto> Events { get; set; }
    }
}