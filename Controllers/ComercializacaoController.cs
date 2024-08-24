using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Comercializacao;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/comercializacao")]
    [ApiController]
    public class ComercializacaoController : ControllerBase
    {
        private readonly ComercializacaoService _Comercializacaoservice;

        public ComercializacaoController(ComercializacaoService Comercializacaoservice)
        {
            _Comercializacaoservice = Comercializacaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarComercializacao(ComercializacaoViewModel dados)
        {
            var Comercializacao = await _Comercializacaoservice.AdicionarComercializacao(dados);
            return Ok(Comercializacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarComercializacao(int id, ComercializacaoViewModel dados)
        {
            var Comercializacao = await _Comercializacaoservice.SalvarComercializacao(id, dados);
            return Ok(Comercializacao);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirComercializacao(int id, ComercializacaoViewModel dados)
        {
            var Comercializacao = await _Comercializacaoservice.ExcluirComercializacao(id, dados);
            return Ok(Comercializacao);
        }

        [HttpGet("Listar/{idconta}/{idano}/{idsafra}/{idparceiro}/{idmoeda}")]
        public async Task<IActionResult> ListarComercializacao(string idconta, int idano, int idsafra, int idparceiro, int idmoeda)
        {
            var Comercializacao = await _Comercializacaoservice.ListarComercializacao(idconta, idano, idsafra, idparceiro, idmoeda);
            return Ok(Comercializacao);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarComercializacaoById(int id)
        {
            var Comercializacao = await _Comercializacaoservice.ListarComercializacaoById(id);
            return Ok(Comercializacao);
        }
    }
}