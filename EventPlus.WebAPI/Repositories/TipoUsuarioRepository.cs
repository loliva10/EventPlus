using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly UsuarioContext _context;

    public TipoUsuarioRepository(UsuarioContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca um tipo de usuario por email
    /// </summary>
    /// <param name="Email">email do tipo usuario a ser buscado</param>
    /// <param name="Senha">senha do tipo usuario a ser buscado</param>
    /// <returns>Objeto do tipoUsuario com as informações do tipo de usuario buscado</returns>
    public Usuario BuscarPorEmail(string Email, string Senha)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email == Email && u.Senha == Senha)!;
    }

    /// <summary>
    /// Busca um tipo de usuario por id
    /// </summary>
    /// <param name="id">id do tipo usuario a ser buscado</param>
    /// <returns>Objeto do tipoUsuario com as informações do tipo de usuario buscado</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de usuario
    /// </summary>
    /// <param name="usuario">tipo de usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
}
