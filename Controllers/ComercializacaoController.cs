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

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarComercializacao(int id, string idconta, ComercializacaoViewModel dados)
        {
            var Comercializacao = await _Comercializacaoservice.SalvarComercializacao(id, idconta, dados);
            return Ok(Comercializacao);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirComercializacao(int id, string idconta, string uid)
        {
            var Comercializacao = await _Comercializacaoservice.ExcluirComercializacao(id, idconta, uid);
            return Ok(Comercializacao);
        }

        [HttpGet("Listar/{idconta}/{idorganizacao}/{idano}/{idfazenda}/{idsafra}/{idparceiro}/{idmoeda}/{ini}/{fim}")]
        public async Task<IActionResult> ListarComercializacao(string idconta, int idorganizacao, int idano, int idfazenda, int idsafra, int idparceiro, int idmoeda, DateTime ini, DateTime fim, string? filtro)
        {
            var Comercializacao = await _Comercializacaoservice.ListarComercializacao(idconta, idano, idorganizacao, idfazenda, idsafra, idparceiro, idmoeda, ini, fim, filtro);
            return Ok(Comercializacao);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarComercializacaoById(int id, string idconta)
        {
            var Comercializacao = await _Comercializacaoservice.ListarComercializacaoById(id, idconta);
            return Ok(Comercializacao);
        }

        [HttpGet("entregas/{id}/{idconta}/{cond}")]
        public async Task<IActionResult> ListarEntregas(int id, string idconta, int cond)
        {
            var Comercializacao = await _Comercializacaoservice.ListarItensEntrega(id, idconta, cond);
            return Ok(Comercializacao);
        }
    }
}