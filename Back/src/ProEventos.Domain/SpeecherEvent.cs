namespace ProEventos.Domain
{
    public class SpeecherEvent
    {
        public int SpeecherId { get; set; }
        public Speecher Speecher { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
