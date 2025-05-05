using ADUSAPI.Entities;
using ADUSAPI.Services;
using ADUSAPICore.Models.MovimentoCaixa;
using ADUSAPICore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("api/movimentocaixa")]
    public class MovimentoCaixaController : ControllerBase
    {
        private readonly MovimentoCaixaService _service;

        public MovimentoCaixaController(MovimentoCaixaService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<MovimentoCaixaViewModel>>> Listar(
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim,
            [FromQuery] int? idTransacao,
            [FromQuery] int? idCentroCusto,
            [FromQuery] string? idParceiro,
            [FromQuery] string? idContaCorrente,
            [FromQuery] int? idCategoria,
            [FromQuery] string? descricao,
            [FromQuery] string? idmovbanco)
        {
            var resultado = await _service.ListarAsync(dataInicio, dataFim, idTransacao, idCentroCusto, idParceiro, idContaCorrente, idCategoria, descricao, idmovbanco);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimentoCaixaViewModel>> Obter(int id)
        {
            var registro = await _service.ObterPorIdAsync(id);
            if (registro == null)
                return NotFound();

            return Ok(registro);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] MovimentoCaixaViewModel model)
        {
            var movc = await _service.AdicionarAsync(model);
            return Ok(movc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] MovimentoCaixaViewModel model)
        {
            if (id != model.Id)
                return BadRequest("ID da URL difere do corpo da requisição.");

            await _service.AtualizarAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("extrato/{ini}/{fim}/{conta}")]
        public async Task<ActionResult<ExtratoConta>> ExtratoConta(DateTime ini, DateTime fim, string conta)
        {
            var registro = await _service.ExtratoConta(ini, fim, conta);
            if (registro == null)
                return NotFound();

            return Ok(registro);
        }
    }
}