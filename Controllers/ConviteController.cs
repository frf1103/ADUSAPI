using ADUSAPI.Services;
using ADUSAPICore.Models.Convite;
using ADUSClient.Convite;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("api/convite")]
    public class ConvitesController : ControllerBase
    {
        private readonly ConviteService _service;

        public ConvitesController(ConviteService service)
        {
            _service = service;
        }

        [HttpGet("listar/{idcoprodutor}/{idafiliado}/{status}/{expirados}/{titular}")]
        public async Task<IActionResult> Get(string? idcoprodutor, string? idafiliado, int? status, int? expirados, string? titular)
        {
            var convites = await _service.ListarConvitesAsync(idcoprodutor, idafiliado, status, expirados, titular);
            return Ok(convites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var convite = await _service.ObterPorId(id);

            return (convite == null) ? NotFound() : Ok(convite);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ADUSAPICore.Models.Convite.ConviteViewModel vm)
        {
            await _service.Adicionar(vm);
            return CreatedAtAction(nameof(Get), new { id = vm.IdConvite }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ADUSAPICore.Models.Convite.ConviteViewModel vm)
        {
            if (id != vm.IdConvite)
                return BadRequest("ID do convite não confere.");

            await _service.Atualizar(vm);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Remover(id);
            return NoContent();
        }
    }
}