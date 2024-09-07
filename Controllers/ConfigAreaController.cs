using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.ConfigArea;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/configarea")]
    [ApiController]
    public class ConfigAreaController : ControllerBase
    {
        private readonly ConfigAreaService _ConfigAreaservice;

        public ConfigAreaController(ConfigAreaService ConfigAreaservice)
        {
            _ConfigAreaservice = ConfigAreaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConfigArea(ConfigAreaViewModel dados)
        {
            var (conta, erros) = await _ConfigAreaservice.AdicionarConfigArea(dados);
            if (erros == null)
            {
                return Ok(conta);
            }
            else
            {
                return BadRequest(new { success = false, erros });
            }
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarConfigArea(int id, string idconta, ConfigAreaViewModel dados)
        {
            var conta = await _ConfigAreaservice.SalvarConfigArea(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirConfigArea(int id, string idconta, string uid)
        {
            var conta = await _ConfigAreaservice.ExcluirConfigArea(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idano}/{idfazenda}/{idtalhao}/{idvariedade}/{idsafra}/{idconta}/{idorganizacao}")]
        public async Task<IActionResult> ListarConfigArea(string idconta, int idano, int idfazenda, int idtalhao, int idvariedade, int idsafra, int idorganizacao)
        {
            var conta = await _ConfigAreaservice.ListarConfigArea(idconta, idano, idfazenda, idtalhao, idvariedade, idsafra, idorganizacao);

            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarConfigAreaById(int id, string idconta)
        {
            var conta = await _ConfigAreaservice.ListarConfigAreaById(id, idconta);
            return Ok(conta);
        }

        [HttpPost("assistente")]
        public async Task<IActionResult> GravarAssistente(List<ConfigAreaViewModel> dados)
        {
            var (success, listerros) = await _ConfigAreaservice.AdicionarConfigAreaAssistente(dados);
            if (success) { return Ok(); }
            else { return BadRequest(new { success = false, listerros }); }
        }
    }
}