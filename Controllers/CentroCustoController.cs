using ADUSAPI.Services;
using ADUSAPICore.Models.CentroCusto;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("api/centrocusto")]
    public class CentroCustoController : ControllerBase
    {
        private readonly CentroCustoService _service;

        public CentroCustoController(CentroCustoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CentroCustoViewModel>>> Listar(string? filtro)
        {
            var lista = await _service.ListarAsync(filtro);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CentroCustoViewModel>> Obter(int id)
        {
            var item = await _service.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CentroCustoViewModel model)
        {
            await _service.AdicionarAsync(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CentroCustoViewModel model)
        {
            await _service.AtualizarAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _service.ExcluirAsync(id);
            return NoContent();
        }
    }
}