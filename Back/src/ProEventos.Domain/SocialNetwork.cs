namespace ProEventos.Domain
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; }
        public int? SpeecherId { get; set; }
        public Speecher Speecher { get; set; }
    }
}