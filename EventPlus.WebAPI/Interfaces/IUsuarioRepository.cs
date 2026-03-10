using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface ITipoUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmail(string Email, string Senha);
}
