using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Operacao;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/operacao")]
    [ApiController]
    public class OperacaoController : ControllerBase
    {
        private readonly OperacaoService _Operacaoservice;

        public OperacaoController(OperacaoService Operacaoservice)
        {
            _Operacaoservice = Operacaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarOperacao(OperacaoViewModel dados)
        {
            var Operacao = await _Operacaoservice.AdicionarOperacao(dados);
            return Ok(Operacao);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarOperacao(int id, string idconta, OperacaoViewModel dados)
        {
            var Operacao = await _Operacaoservice.SalvarOperacao(id, idconta, dados);
            return Ok(Operacao);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirOperacao(int id, string idconta)
        {
            var Operacao = await _Operacaoservice.ExcluirOperacao(id, idconta);
            return Ok(Operacao);
        }

        [HttpGet("Listar/{idconta}/{idtipo}")]
        public async Task<IActionResult> ListarOperacao(string idconta, int idtipo, string? filtro)
        {
            var Operacao = await _Operacaoservice.ListarOperacao(filtro, idconta, idtipo);
            return Ok(Operacao);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarOperacaoById(int id, string idconta)
        {
            var Operacao = await _Operacaoservice.ListarOperacaoById(id, idconta);
            return Ok(Operacao);
        }
    }
}