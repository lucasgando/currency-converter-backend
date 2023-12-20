using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto.UserDtos
{
    public class UserForDeletionDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
