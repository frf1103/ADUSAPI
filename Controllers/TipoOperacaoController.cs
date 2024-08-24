using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.TipoOperacao;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/tipooperacao")]
    [ApiController]
    public class TipoOperacaoController : ControllerBase
    {
        private readonly TipoOperacaoService _TipoOperacaoservice;

        public TipoOperacaoController(TipoOperacaoService TipoOperacaoservice)
        {
            _TipoOperacaoservice = TipoOperacaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTipoOperacao(TipoOperacaoViewModel dados)
        {
            var TipoOperacao = await _TipoOperacaoservice.AdicionarTipoOperacao(dados);
            return Ok(TipoOperacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarTipoOperacao(int id, TipoOperacaoViewModel dados)
        {
            var TipoOperacao = await _TipoOperacaoservice.SalvarTipoOperacao(id, dados);
            return Ok(TipoOperacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirTipoOperacao(int id)
        {
            var TipoOperacao = await _TipoOperacaoservice.ExcluirTipoOperacao(id);
            return Ok(TipoOperacao);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTipoOperacao(string? filtro)
        {
            var TipoOperacao = await _TipoOperacaoservice.ListarTipoOperacao(filtro);
            return Ok(TipoOperacao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarTipoOperacaoById(int id)
        {
            var TipoOperacao = await _TipoOperacaoservice.ListarTipoOperacaoById(id);
            return Ok(TipoOperacao);
        }
    }
}