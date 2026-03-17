using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    [Required(ErrorMessage = "O Nome do evento é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A Descrição do evento é obritória!")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A Data do Evento é obrigatória!")]
    public DateTime DataEvento { get; set; }
}
