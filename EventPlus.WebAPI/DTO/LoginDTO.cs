using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email de usuário é obrigatório!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha de usuário é obrigatória!")]
    public string? Senha {  get; set; }

    //[Required(ErrorMessage = "O titulo de usuário é obrigatório!")]
    //public string? Titulo { get; set; }
}
