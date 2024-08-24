using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Maquinas;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/maquina")]
    [ApiController]
    public class MaquinaController : ControllerBase
    {
        private readonly MaquinaService _Maquinaservice;

        public MaquinaController(MaquinaService Maquinaservice)
        {
            _Maquinaservice = Maquinaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarMaquina(MaquinaViewModel dados)
        {
            var Maquina = await _Maquinaservice.AdicionarMaquina(dados);
            return Ok(Maquina);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarMaquina(int id, string idconta, MaquinaViewModel dados)
        {
            var Maquina = await _Maquinaservice.SalvarMaquina(id, idconta, dados);
            return Ok(Maquina);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirMaquina(int id, string idconta)
        {
            var Maquina = await _Maquinaservice.ExcluirMaquina(id, idconta);
            return Ok(Maquina);
        }

        [HttpGet("Listar/{idconta}/{idmodelo}/{idorganizacao}")]
        public async Task<IActionResult> ListarMaquina(string idconta, int idmodelo, int idorganizacao, string? filtro)
        {
            var Maquina = await _Maquinaservice.ListarMaquina(idconta, idmodelo, idorganizacao, filtro);
            return Ok(Maquina);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarMaquinaById(int id, string idconta)
        {
            var Maquina = await _Maquinaservice.ListarMaquinaById(id, idconta);
            return Ok(Maquina);
        }
    }
}