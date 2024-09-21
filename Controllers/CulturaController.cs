using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Conta;
using FarmPlannerAPICore.Models.Cultura;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/culturas")]
    [ApiController]
    public class CulturaController : ControllerBase
    {
        private readonly CulturaService _culturaservice;

        public CulturaController(CulturaService culturaservice)
        {
            _culturaservice = culturaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCultura(CulturaViewModel dados)
        {
            var conta = await _culturaservice.AdicionarCultura(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarCultura(int id, CulturaViewModel dados)
        {
            var conta = await _culturaservice.SalvarCultura(id, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirCultura(int id)
        {
            var conta = await _culturaservice.ExcluirCultura(id);
            return Ok(conta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCultura(string? filtro)
        {
            var conta = await _culturaservice.ListarCultura(filtro);
            return Ok(conta);
        }

        [HttpGet("variedade")]
        public async Task<IActionResult> ListarCulturaVariedade(string? filtro)
        {
            var conta = await _culturaservice.ListarCulturaVariedade(filtro);
            return Ok(conta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarCulturaById(int id)
        {
            var conta = await _culturaservice.ListarCulturaById(id);
            return Ok(conta);
        }
    }
}