using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto
{
    public class CredentialsDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
