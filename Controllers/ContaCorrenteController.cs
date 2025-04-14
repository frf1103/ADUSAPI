using Microsoft.AspNetCore.Mvc;
using ADUSAPI.Services;

using ADUSAPICore.Models.Banco;

namespace ADUSAPI.Controllers
{
    [Route("api/contacorrente")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ContaCorrenteService _service;

        public ContaCorrenteController(ContaCorrenteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ContaCorrenteViewModel dados)
        {
            var resultado = await _service.Adicionar(dados);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Salvar(string id, ContaCorrenteViewModel dados)
        {
            var resultado = await _service.Salvar(id, dados);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var resultado = await _service.Excluir(id);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(string id)
        {
            var resultado = await _service.GetById(id);
            return Ok(resultado);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetTodos([FromQuery] string? filtro, [FromQuery] int? bancoId)
        {
            var resultado = await _service.Listar(filtro, bancoId);
            return Ok(resultado);
        }
    }
}