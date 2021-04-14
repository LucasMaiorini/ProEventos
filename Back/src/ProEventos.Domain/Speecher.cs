using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Speecher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
        public IEnumerable<SpeecherEvent> SpeecherEvents { get; set; }



    }
}