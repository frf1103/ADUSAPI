using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.PrincipioAtivo;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/principioativo")]
    [ApiController]
    public class PrincipioAtivoController : ControllerBase
    {
        private readonly PrincipioAtivoService _PrincipioAtivoservice;

        public PrincipioAtivoController(PrincipioAtivoService PrincipioAtivoservice)
        {
            _PrincipioAtivoservice = PrincipioAtivoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPrincipioAtivo(PrincipioAtivoViewModel dados)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.AdicionarPrincipioAtivo(dados);
            return Ok(PrincipioAtivo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarPrincipioAtivo(int id, string idconta, PrincipioAtivoViewModel dados)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.SalvarPrincipioAtivo(id, dados);
            return Ok(PrincipioAtivo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirPrincipioAtivo(int id, string idconta)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.ExcluirPrincipioAtivo(id);
            return Ok(PrincipioAtivo);
        }

        [HttpGet]
        public async Task<IActionResult> ListarPrincipioAtivo(string? filtro)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.ListarPrincipioAtivo(filtro);
            return Ok(PrincipioAtivo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPrincipioAtivoById(int id)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.ListarPrincipioAtivoById(id);
            return Ok(PrincipioAtivo);
        }
    }
}