using ADUSAPI.Entities;
using ADUSAPI.Services;
using ADUSAPICore.Models.Assinatura;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("api/ccassina")]
    public class CartaoAssinaturaController : ControllerBase
    {
        private readonly CartaoAssinaturaService _service;

        public CartaoAssinaturaController(CartaoAssinaturaService service)
        {
            _service = service;
        }

        [HttpGet("assinatura/{idAssinatura}")]
        public async Task<IActionResult> ListarPorAssinatura(string idAssinatura)
        {
            var cartoes = await _service.ListarPorAssinaturaAsync(idAssinatura);
            return Ok(cartoes);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CartaoAssinaturaViewModel cartao)
        {
            await _service.AdicionarAsync(cartao);
            return CreatedAtAction(nameof(ObterPorId), new { id = cartao.Id }, cartao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var cartao = await _service.ObterPorIdAsync(id);
            if (cartao == null) return Ok();
            return Ok(cartao);
        }

        [HttpGet("token/{id}")]
        public async Task<IActionResult> ObterPorIdToken(string id)
        {
            var cartao = await _service.ObterPorTokenAsync(id);
            if (cartao == null) return NotFound();
            return Ok(cartao);
        }

        [HttpPost("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            await _service.AtivarCartaoAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/inativar")]
        public async Task<IActionResult> Inativar(int id)
        {
            await _service.InativarCartaoAsync(id);
            return NoContent();
        }
    }
}