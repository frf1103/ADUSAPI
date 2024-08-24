using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Fazenda;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/fazenda")]
    [ApiController]
    public class FazendaController : ControllerBase
    {
        private readonly FazendaService _Fazendaservice;

        public FazendaController(FazendaService Fazendaservice)
        {
            _Fazendaservice = Fazendaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarFazenda(EditarFazendaViewModel dados)
        {
            var Fazenda = await _Fazendaservice.AdicionarFazenda(dados);
            return Ok(Fazenda);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarFazenda(int id, string idconta, EditarFazendaViewModel dados)
        {
            var Fazenda = await _Fazendaservice.SalvarFazenda(id, idconta, dados);
            return Ok(Fazenda);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirFazenda(int id, string idconta)
        {
            var Fazenda = await _Fazendaservice.ExcluirFazenda(id, idconta);
            return Ok(Fazenda);
        }

        [HttpGet("Listar/{idorganizacao}/{idconta}/{idregiao}")]
        public async Task<IActionResult> ListarFazenda(int idorganizacao, string idconta, int idregiao, string? filtro)
        {
            var Fazenda = await _Fazendaservice.ListarFazenda(idorganizacao, idregiao, idconta, filtro);
            return Ok(Fazenda);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarFazendaById(int id, string idconta)
        {
            var Fazenda = await _Fazendaservice.ListarFazendaById(idconta, id);
            return Ok(Fazenda);
        }
    }
}