using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto.CurrencyDtos
{
    public class CurrencyForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public float ConversionIndex { get; set; }
        public string Flag { get; set; }
    }
}
