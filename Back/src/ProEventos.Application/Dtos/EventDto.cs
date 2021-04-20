using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Theme { get; set; }
        [Range(1,120000)]
        public int NumberOfPeople { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$")]
        public string ImageURL { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        public IEnumerable<BatchDto> Batches { get; set; }
        public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
        public IEnumerable<SpeecherDto> Speechers { get; set; }
    }
}