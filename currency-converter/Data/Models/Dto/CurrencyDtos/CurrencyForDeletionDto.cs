using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto.CurrencyDtos
{
    public class CurrencyForDeletionDto
    {
        [Required]
        public int Id { get; set; }
    }
}
