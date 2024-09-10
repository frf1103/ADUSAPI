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
            var (conta, erros) = await _MaquinaPlanejadaservice.AdicionarMaquinaPlanejada(dados);
            if (erros == null) { return Ok(conta); }
            else { return BadRequest(new { success = false, erros }); }
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarMaquinaPlanejada(int id, string idconta, MaquinaPlanejadaViewModel dados)
        {
            var conta = await _MaquinaPlanejadaservice.SalvarMaquinaPlanejada(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirMaquinaPlanejada(int id, string idconta, string uid)
        {
            var conta = await _MaquinaPlanejadaservice.ExcluirMaquinaPlanejada(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idplanejamento}/{idconta}")]
        public async Task<IActionResult> ListarMaquinaPlanejada(int idplanejamento, string idconta)
        {
            var conta = await _MaquinaPlanejadaservice.ListarMaquinaPlanejadaByPlanejamento(idplanejamento, idconta);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarMaquinaPlanejadaById(int id, string idconta)
        {
            var conta = await _MaquinaPlanejadaservice.ListarMaquinaPlanejadaById(id, idconta);
            return Ok(conta);
        }
    }
}