using EventPlus.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presecaRepository;

    public PresencaController(IPresencaRepository presecaRepository)
    {
        _presecaRepository = presecaRepository;
    }

    /// <summary>
    /// EndPoint da API que retorna um usuário por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>Status code 200 e presença buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presecaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que retorna uma lista de presenças filtrada por usuário
    /// </summary>
    /// <param name="idUsuario">id do usuário para filtragem</param>
    /// <returns>uma lista de presenças filtrada pelo usuário</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presecaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
