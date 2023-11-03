using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto
{
    public class UserForDeletionDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
