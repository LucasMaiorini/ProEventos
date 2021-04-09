namespace ProEventos.API.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Location { get; set; }
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int NumberOfPeople { get; set; }
        public string Batch { get; set; }
        public string ImageURL { get; set; }
        
    }
}