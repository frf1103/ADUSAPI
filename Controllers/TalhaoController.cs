using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Talhao;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/talhao")]
    [ApiController]
    public class TalhaoController : ControllerBase
    {
        private readonly TalhaoService _Talhaoservice;

        public TalhaoController(TalhaoService Talhaoservice)
        {
            _Talhaoservice = Talhaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTalhao(EditarTalhaoViewModel dados)
        {
            var Talhao = await _Talhaoservice.AdicionarTalhao(dados);
            return Ok(Talhao);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarTalhao(int id, string idconta, EditarTalhaoViewModel dados)
        {
            var Talhao = await _Talhaoservice.SalvarTalhao(id, idconta, dados);
            return Ok(Talhao);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirTalhao(int id, string idconta, string uid)
        {
            var Talhao = await _Talhaoservice.ExcluirTalhao(id, idconta, uid);
            return Ok(Talhao);
        }

        [HttpGet("Listar/{idfazenda}/{idconta}/{idano}")]
        public async Task<IActionResult> ListarTalhao(int idfazenda, string idconta, int idano, string? filtro)
        {
            var Talhao = await _Talhaoservice.ListarTalhaoByFazenda(idfazenda, idconta, idano, filtro);
            return Ok(Talhao);
        }

        [HttpGet("disponivel/{idfazenda}/{idconta}/{idano}")]
        public async Task<IActionResult> ListarTalhaoDisponivel(int idfazenda, string idconta, int idano)
        {
            var Talhao = await _Talhaoservice.ListarTalhaoDisponivel(idfazenda, idconta, idano);
            return Ok(Talhao);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarTalhaoById(int id, string idconta)
        {
            var Talhao = await _Talhaoservice.ListarTalhaoById(id, idconta);
            return Ok(Talhao);
        }
    }
}