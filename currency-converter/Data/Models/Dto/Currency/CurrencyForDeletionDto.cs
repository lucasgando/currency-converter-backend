using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto.Currency
{
    public class CurrencyForDeletionDto
    {
        [Required]
        public int Id { get; set; }
    }
}
