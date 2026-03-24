using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// EndPoint da API que cadastra e modera um comentário
    /// </summary>
    /// <param name="comentarioEvento">comentário a ser moderado</param>
    /// <returns>Statis Code 201 e o comentário criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio.");
            }

            // Criar objeto de análise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            // Chamar a api do azure content safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            // Verificar se o texto tem alguma severidade maior que 0
            bool temConteudoImpropio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentarioEvento = DateTime.Now,

                // Define se o comentário vai ser exibido
                Exibe = !temConteudoImpropio,
            };

            // Cadastro o comentário
            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
