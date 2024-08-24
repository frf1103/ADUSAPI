using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Variedade;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/variedades")]
    [ApiController]
    public class VariedadeController : ControllerBase
    {
        private readonly VariedadeService _Variedadeservice;

        public VariedadeController(VariedadeService Variedadeservice)
        {
            _Variedadeservice = Variedadeservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarVariedade(VariedadeViewModel dados)
        {
            var conta = await _Variedadeservice.AdicionarVariedade(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarVariedade(int id, VariedadeViewModel dados)
        {
            var conta = await _Variedadeservice.SalvarVariedade(id, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirVariedade(int id)
        {
            var conta = await _Variedadeservice.ExcluirVariedade(id);
            return Ok(conta);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarVariedade(int idcultura, string? filtro)
        {
            var conta = await _Variedadeservice.ListarVariedade(idcultura, filtro);
            return Ok(conta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarVariedadeById(int id)
        {
            var conta = await _Variedadeservice.ListarVariedadeById(id);
            return Ok(conta);
        }
    }
}