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

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarPrincipioAtivo(int id, string idconta, PrincipioAtivoViewModel dados)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.SalvarPrincipioAtivo(id, idconta, dados);
            return Ok(PrincipioAtivo);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirPrincipioAtivo(int id, string idconta)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.ExcluirPrincipioAtivo(id, idconta);
            return Ok(PrincipioAtivo);
        }

        [HttpGet("{idconta}")]
        public async Task<IActionResult> ListarPrincipioAtivo(string idconta, string? filtro)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.ListarPrincipioAtivo(idconta, filtro);
            return Ok(PrincipioAtivo);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarPrincipioAtivoById(string idconta, int id)
        {
            var PrincipioAtivo = await _PrincipioAtivoservice.ListarPrincipioAtivoById(id, idconta);
            return Ok(PrincipioAtivo);
        }
    }
}