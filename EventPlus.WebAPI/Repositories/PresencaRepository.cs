using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository (EventContext eventContext)
    {
        _eventContext = eventContext; 
    }

    public void Atualizar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao; 

            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="Id">id da presença a ser buscado</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid Id)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == Id)!;
    }

    /// <summary>
    /// Deleta uma presença
    /// </summary>
    /// <param name="id">id da presença a ser deletada</param>
    public void Deletar(Guid id)
    {
        var presenca = _eventContext.Presencas.Find(id);

        if (presenca != null)
        {
            _eventContext.Presencas.Remove(presenca);
            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Inscreve uma nova presença
    /// </summary>
    /// <param name="Inscricao">dados da presença a ser cadastrada</param>
    public void Inscrever(Presenca Inscricao)
    {
        Inscricao.IdPresenca = Guid.NewGuid();

        _eventContext.Presencas.Add(Inscricao);
        _eventContext.SaveChanges();
    }

    /// <summary>
    /// Busca a lista de presença cadastrada
    /// </summary>
    /// <returns>uma lista de presenças</returns>
    public List<Presenca> Listar()
    {
        return _eventContext.Presencas.OrderBy(presenca => presenca.Situacao).ToList();
    }

    /// <summary>
    /// Lista as presenças e o usuário expecifico
    /// </summary>
    /// <param name="IdUsuario">id od usuário para filtragem</param>
    /// <returns>uma lista de presenças de um usuário especifico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
    }
}
