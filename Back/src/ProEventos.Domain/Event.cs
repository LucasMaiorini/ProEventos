using System;
using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime? EventDate { get; set; }
        public string Theme { get; set; }
        public int NumberOfPeople { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<Batch> Batches { get; set; }
        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
        public IEnumerable<SpeecherEvent> SpeecherEvents { get; set; }

    }
}