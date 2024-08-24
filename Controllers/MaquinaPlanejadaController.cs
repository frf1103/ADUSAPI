using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.MaquinaPlanejada;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/maquinaplanejada")]
    [ApiController]
    public class MaquinaPlanejadaController : ControllerBase
    {
        private readonly MaquinaPlanejadaService _MaquinaPlanejadaservice;

        public MaquinaPlanejadaController(MaquinaPlanejadaService MaquinaPlanejadaservice)
        {
            _MaquinaPlanejadaservice = MaquinaPlanejadaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarMaquinaPlanejada(MaquinaPlanejadaViewModel dados)
        {
            var conta = await _MaquinaPlanejadaservice.AdicionarMaquinaPlanejada(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarMaquinaPlanejada(int id, MaquinaPlanejadaViewModel dados)
        {
            var conta = await _MaquinaPlanejadaservice.SalvarMaquinaPlanejada(id, dados);
            return Ok(conta);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirMaquinaPlanejada(int id, MaquinaPlanejadaViewModel dados)
        {
            var conta = await _MaquinaPlanejadaservice.ExcluirMaquinaPlanejada(id, dados);
            return Ok(conta);
        }

        [HttpGet("Listar/{idplanejamento}")]
        public async Task<IActionResult> ListarMaquinaPlanejada(int idplanejamento)
        {
            var conta = await _MaquinaPlanejadaservice.ListarMaquinaPlanejadaByPlanejamento(idplanejamento);
            return Ok(conta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarMaquinaPlanejadaById(int id)
        {
            var conta = await _MaquinaPlanejadaservice.ListarMaquinaPlanejadaById(id);
            return Ok(conta);
        }
    }
}