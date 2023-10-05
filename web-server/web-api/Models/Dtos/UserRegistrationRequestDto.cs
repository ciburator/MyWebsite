using System.ComponentModel.DataAnnotations;

namespace Home_API.Models.Dtos;

public class UserRegistrationRequestDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    [Required]
    public string Nickname { get; set; }
    [Required]
    public string Password { get; set; }
}