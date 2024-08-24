using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Maquinas;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/maquinaparametro")]
    [ApiController]
    public class MaquinaParametroController : ControllerBase
    {
        private readonly MaquinaParametroService _MaquinaParametroservice;

        public MaquinaParametroController(MaquinaParametroService MaquinaParametroservice)
        {
            _MaquinaParametroservice = MaquinaParametroservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarMaquinaParametro(MaquinaParametroViewModel dados)
        {
            var MaquinaParametro = await _MaquinaParametroservice.AdicionarMaquinaParametro(dados);
            return Ok(MaquinaParametro);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarMaquinaParametro(int id, string idconta, MaquinaParametroViewModel dados)
        {
            var MaquinaParametro = await _MaquinaParametroservice.SalvarMaquinaParametro(id, idconta, dados);
            return Ok(MaquinaParametro);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirMaquinaParametro(int id, string idconta)
        {
            var MaquinaParametro = await _MaquinaParametroservice.ExcluirMaquinaParametro(id, idconta);
            return Ok(MaquinaParametro);
        }

        [HttpGet("Listar/{idcultura}/{idmaquina}/{idoperacao}/{idconta}")]
        public async Task<IActionResult> ListarMaquinaParametro(int idcultura, int idmaquina, int idoperacao, string idconta)
        {
            var MaquinaParametro = await _MaquinaParametroservice.ListarMaquinaParametroByMaquina(idmaquina, idcultura, idoperacao, idconta);
            return Ok(MaquinaParametro);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarMaquinaParametroById(int id, string idconta)
        {
            var MaquinaParametro = await _MaquinaParametroservice.ListarMaquinaParametroById(id, idconta);
            return Ok(MaquinaParametro);
        }
    }
}