using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _eventContext;

    public ComentarioEventoRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    /// <summary>
    /// Busca um comentário específico com base no usuário e evento.
    /// </summary>
    /// <param name="IdUsuario">ID do usuário</param>
    /// <param name="IdEvento">ID do evento</param>
    /// <returns>
    /// Retorna o comentário encontrado ou null caso não exista.
    /// </returns>
    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _eventContext.ComentarioEventos.FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento);
    }

    /// <summary>
    /// Cadastra um novo comentário
    /// </summary>
    /// <param name="comentarioEvento">dados do comentário a ser cadastrado</param>
    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        comentarioEvento.IdComentarioEvento = Guid.NewGuid();

        _eventContext.ComentarioEventos.Add(comentarioEvento);  
        _eventContext.SaveChanges();
    }

    /// <summary>
    /// Deleta um comentário
    /// </summary>
    /// <param name="id">id do comentário a ser deletado</param>
    public void Deletar(Guid id)
    {
        var comentarioEvento = _eventContext.ComentarioEventos.Find(id);

        if (comentarioEvento != null)
        {
            _eventContext.ComentarioEventos.Remove(comentarioEvento);
            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de comentários do evento
    /// </summary>
    /// <param name="IdEvento">id do comentário do evento</param>
    /// <returns>lista de comentários relacionados ao evento</returns>
    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _eventContext.ComentarioEventos.Where(c => c.IdEvento == IdEvento).OrderBy(c => c.Descricao).ToList();
    }

    /// <summary>
    /// Busca a lista dos comentários do evento com os dados do usuário
    /// </summary>
    /// <param name="IdEvento">id do comentário do evento</param>
    /// <returns>lista de comentários com informações do usuário</returns>
    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _eventContext.ComentarioEventos.Include(c => c.IdUsuarioNavigation).Where(c => c.IdEvento == IdEvento).ToList();
    }
}
