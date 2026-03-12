using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        /// <summary>
        /// EndPoint da API que faz a chamada do método listar as instituições
        /// </summary>
        /// <returns>Status code 200 e a lista das instituições</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_instituicaoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// EndPoint da API que faz a chamada do método buscar uma instituição específica
        /// </summary>
        /// <param name="id">id da instituição buscada</param>
        /// <returns>Status code 200 e instituição buscada</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// EndPoint da API que faz a chamada para o método cadastrar uma instituição
        /// </summary>
        /// <param name="instituicao">instituição a ser cadastrada</param>
        /// <returns>Status code 201 e a instituição cadastrada</returns>
        [HttpPost]
        public IActionResult Cadastrar(InstituicaoDTO instituicao)
        {
            try
            {
                var novaInstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!
                };

                _instituicaoRepository.Cadastrar(novaInstituicao);

                return StatusCode(201, novaInstituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// EndPoint da API que faz a chamada para o método atualizar uma instituição
        /// </summary>
        /// <param name="id">id da instituição a ser atualizada</param>
        /// <param name="instituicao">instituição com os dados atualizados</param>
        /// <returns>Status code 204 e a instituição atualizada</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var instituicaoAtualizado = new Instituicao
                {

                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!,
                    NomeFantasia = instituicao.NomeFantasia!
                };

                _instituicaoRepository.Atualizar(id, instituicaoAtualizado);
                return StatusCode(204, instituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// EndPoint da API que faz a chamada para o método deletar uma instituição
        /// </summary>
        /// <param name="id">id da instituição a ser deletada</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _instituicaoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
