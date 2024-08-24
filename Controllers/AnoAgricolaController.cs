using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.AnoAgricola;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/anoagricola")]
    [ApiController]
    public class AnoAgricolaController : ControllerBase
    {
        private readonly AnoAgricolaService _AnoAgricolaservice;

        public AnoAgricolaController(AnoAgricolaService AnoAgricolaservice)
        {
            _AnoAgricolaservice = AnoAgricolaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAnoAgricola(AdicionarAnoAgricolaViewModel dados)
        {
            var AnoAgricola = await _AnoAgricolaservice.AdicionarAnoAgricola(dados);
            return Ok(AnoAgricola);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarAnoAgricola(int id, string idconta, AdicionarAnoAgricolaViewModel dados)
        {
            var AnoAgricola = await _AnoAgricolaservice.SalvarAnoAgricola(id, idconta, dados);
            return Ok(AnoAgricola);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirAnoAgricola(int id, string idconta)
        {
            var AnoAgricola = await _AnoAgricolaservice.ExcluirAnoAgricola(id, idconta);
            return Ok(AnoAgricola);
        }

        [HttpGet("Listar/{idorganizacao}/{idconta}")]
        public async Task<IActionResult> ListarAnoAgricola(int idorganizacao, string idconta, string? filtro)
        {
            var AnoAgricola = await _AnoAgricolaservice.ListarAnoAgricola(idorganizacao, idconta, filtro);
            return Ok(AnoAgricola);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarAnoAgricolaById(int id, string idconta)
        {
            var AnoAgricola = await _AnoAgricolaservice.ListarAnoAgricolaById(idconta, id);
            return Ok(AnoAgricola);
        }
    }
}