using ADUSAPI.Services;
using ADUSAPICore.Models.Banco;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("api/transacbanco")]
    public class TransacBancoController : ControllerBase
    {
        private readonly TransacBancoService _service;

        public TransacBancoController(TransacBancoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _service.ObterTodosAsync();
            return Ok(lista);
        }

        [HttpGet("{id}/{idbanco}")]
        public async Task<IActionResult> Get(string id, int idbanco)
        {
            var item = await _service.ObterPorIdAsync(id, idbanco);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransacBancoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.AdicionarAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TransacBancoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _service.AtualizarAsync(model);
            return Ok();
        }

        [HttpDelete("{idtbc}/{idbanco}")]
        public async Task<IActionResult> Delete(string idtbc, int idbanco)
        {
            await _service.RemoverAsync(idtbc, idbanco);
            return Ok();
        }
    }
}