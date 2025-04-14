using ADUSAPI.Services;
using ADUSAPICore.Models.Banco;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/banco")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly BancoService _bancoService;

        public BancoController(BancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarBanco(BancoViewModel dados)
        {
            var banco = await _bancoService.AdicionarBanco(dados);
            return Ok(banco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarBanco(int id, BancoViewModel dados)
        {
            var banco = await _bancoService.SalvarBanco(id, dados);
            return Ok(banco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirBanco(int id)
        {
            var banco = await _bancoService.ExcluirBanco(id);
            return Ok(banco);
        }

        [HttpGet]
        public async Task<IActionResult> ListarBanco(string? filtro)
        {
            var bancos = await _bancoService.ListarBanco(filtro);
            return Ok(bancos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarBancoById(int id)
        {
            var banco = await _bancoService.ListarBancoById(id);
            return Ok(banco);
        }
    }
}