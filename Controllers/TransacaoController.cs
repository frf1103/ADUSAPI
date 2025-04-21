using ADUSAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ADUSAPICore.Models.Transacao;

namespace ADUSAPI.Controllers
{
    [Route("api/transacao")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _service;

        public TransacaoController(TransacaoService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<TransacaoViewModel>>> Listar([FromQuery] string? filtro)
        {
            var result = await _service.ListarAsync(filtro);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransacaoViewModel>> GetById(int id)
        {
            var item = await _service.BuscarPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TransacaoViewModel>> Post([FromBody] TransacaoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var criado = await _service.AdicionarAsync(model);
            return Ok(criado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransacaoViewModel>> Put(int id, TransacaoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _service.AtualizarAsync(id, model);
            if (atualizado == null) return NotFound();
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.ExcluirAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}