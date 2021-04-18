using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class BatchDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int Quantity { get; set; }
        public int EventId { get; set; }
        public EventDto Event { get; set; }
    }
}