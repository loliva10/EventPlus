using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }

    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.ToList();
    }

    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = String.IsNullOrWhiteSpace(tipoUsuario.Titulo) ? tipoUsuario.Titulo : tipoUsuarioBuscado.Titulo;

            _context.SaveChanges();
        }
    }

    public void Deletar(Guid id)
    {
        TipoUsuario tipoUsuarioBuscado = _context.TipoUsuarios.Find(id)!;

        _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
        _context.SaveChanges();
    }
}