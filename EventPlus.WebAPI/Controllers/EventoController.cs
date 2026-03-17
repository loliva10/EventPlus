using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController (IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar eventos filtrando pelo id do usuário
    /// </summary>
    /// <param name="IdUsuario">id do usuário para filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método listar para os próximos eventos
    /// </summary>
    /// <returns>Status code 200 e a lista dos próximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para listar os eventos
    /// </summary>
    /// <returns>Status code 200 e a lista dos eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para um método cadastrar um evento
    /// </summary>
    /// <param name="evento">evento a ser cadastrado</param>
    /// <returns>Status code 201 e o evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(Evento evento)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = evento.Nome,
                Descricao = evento.Descricao,
                DataEvento = evento.DataEvento,
                IdTipoEvento = evento.IdTipoEvento,
                IdInstituicao = evento.IdInstituicao    
            };

            _eventoRepository.Cadastrar(novoEvento);
            return StatusCode(201, novoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para um método atualizar um evento
    /// </summary>
    /// <param name="id">id do evento a ser atualizado</param>
    /// <param name="evento">evento com os dados atualizados</param>
    /// <returns>Status code 204 e o evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, Evento evento)
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = evento.Nome,
                Descricao = evento.Descricao,
                DataEvento = evento.DataEvento,
                IdTipoEvento = evento.IdTipoEvento,
                IdInstituicao = evento.IdInstituicao
            };

            _eventoRepository.Atualizar(id, eventoAtualizado);
            return StatusCode(204, evento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message); 
        }
    }
}
