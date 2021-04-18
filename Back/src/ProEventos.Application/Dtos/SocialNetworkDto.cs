namespace ProEventos.Application.Dtos
{
    public class SocialNetworkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? EventId { get; set; }
        public EventDto Event { get; set; }
        public int? SpeecherId { get; set; }
        public SpeecherDto Speecher { get; set; }
    }
}