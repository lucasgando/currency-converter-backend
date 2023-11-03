using currency_converter.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto
{
    public class UserForCreationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public RoleEnum Role { get; set; } = RoleEnum.User;
    }
}
