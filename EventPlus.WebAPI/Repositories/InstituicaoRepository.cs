using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly UsuarioContext _context;

    public InstituicaoRepository(UsuarioContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza uma instituicao usando o rastramento automático
    /// </summary>
    /// <param name="id">id da instituicao a ser atualizado</param>
    /// <param name="instituicao">novos dados da instituicao</param>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de instituicao por id
    /// </summary>
    /// <param name="id">id do evento a ser buscado</param>
    /// <returns>Objeto da instituicao com as informações da instituicao buscado</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// Cadastra uma nova instituicao
    /// </summary>
    /// <param name="instituicao">dados da instituicao a ser cadastrado</param>
    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta uma instituicao
    /// </summary>
    /// <param name="id">id da instituicao a ser deletado</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Deletar(Guid id)
    {
        var instituicao = _context.Instituicaos.Find(id);

        if (instituicao != null)
        {
            _context.Instituicaos.Remove(instituicao);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de instituicao cadastrada
    /// </summary>
    /// <returns>Uma lista de instituicao</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(instituicao => instituicao.NomeFantasia).ToList();
    }
}
