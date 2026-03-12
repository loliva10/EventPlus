using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O nome fantasia é obrigatório")]
    public string NomeFantasia { get; set; }

    [Required(ErrorMessage = "O CNPJ é obrigatório")]
    public string Cnpj { get; set; }

    [Required(ErrorMessage = "O endereço é obrigatório")]
    public string Endereco { get; set; }
}
