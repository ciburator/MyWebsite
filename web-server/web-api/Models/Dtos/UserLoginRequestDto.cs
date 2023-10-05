using System.ComponentModel.DataAnnotations;

namespace Home_API.Models.Dtos;

public class UserLoginRequestDto
{
    [Required]
    public string NickName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}