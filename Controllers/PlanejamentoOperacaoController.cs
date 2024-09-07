using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.PlanejamentoOperacao;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/planejamentooperacao")]
    [ApiController]
    public class PlanejamentoOperacaoController : ControllerBase
    {
        private readonly PlanejamentoOperacaoService _PlanejamentoOperacaoservice;

        public PlanejamentoOperacaoController(PlanejamentoOperacaoService PlanejamentoOperacaoservice)
        {
            _PlanejamentoOperacaoservice = PlanejamentoOperacaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPlanejamentoOperacao(PlanejamentoOperacaoViewModel dados)
        {
            var (conta, erros) = await _PlanejamentoOperacaoservice.AdicionarPlanejamentoOperacao(dados);
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
        public async Task<IActionResult>? EditarPlanejamentoOperacao(int id, string idconta, PlanejamentoOperacaoViewModel dados)
        {
            var conta = await _PlanejamentoOperacaoservice.SalvarPlanejamentoOperacao(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirPlanejamentoOperacao(int id, string idconta, string uid)
        {
            var conta = await _PlanejamentoOperacaoservice.ExcluirPlanejamentoOperacao(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idorganizacao}/{idfazenda}/{idano}/{idsafra}/{idtalhao}/{idoperacao}/{idconta}/{idvariedade}/{ini}/{fim}")]
        public async Task<IActionResult> ListarPlanejamentoOperacao(int idorganizacao, int idfazenda, int idano, int idsafra, int idtalhao, int idoperacao, string idconta, int idvariedade, DateTime ini, DateTime fim)
        {
            var conta = await _PlanejamentoOperacaoservice.ListarPlanejamentoOperacao(idorganizacao, idsafra, idano, idfazenda,idoperacao,  idtalhao, idconta, idvariedade, ini, fim);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarPlanejamentoOperacaoById(int id, string idconta)
        {
            var conta = await _PlanejamentoOperacaoservice.ListarPlanejamentoOperacaoById(id, idconta);
            return Ok(conta);
        }

        [HttpPost("assistente/{idconta}/{uid}")]
        public async Task<IActionResult> GravarAssistente(string idconta, string uid, List<AssistentePlanejOperViewModel> dados)
        {
            var (success, listerros) = await _PlanejamentoOperacaoservice.AdicionarPlanejOperAreaAssistente(idconta, uid, dados);
            if (success) { return Ok(); }
            else { return BadRequest(new { success = false, listerros }); }
        }
    }
}