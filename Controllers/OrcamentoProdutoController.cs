using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.OrcamentoProduto;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/orcamentoproduto")]
    [ApiController]
    public class OrcamentoProdutoController : ControllerBase
    {
        private readonly OrcamentoProdutoService _OrcamentoProdutoservice;

        public OrcamentoProdutoController(OrcamentoProdutoService OrcamentoProdutoservice)
        {
            _OrcamentoProdutoservice = OrcamentoProdutoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarOrcamentoProduto(OrcamentoProdutoViewModel dados)
        {
            var conta = await _OrcamentoProdutoservice.AdicionarOrcamentoProduto(dados);
            return Ok(conta);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarOrcamentoProduto(int id, string idconta, OrcamentoProdutoViewModel dados)
        {
            var conta = await _OrcamentoProdutoservice.SalvarOrcamentoProduto(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirOrcamentoProduto(int id, string uid, string idconta)
        {
            var conta = await _OrcamentoProdutoservice.ExcluirOrcamentoProduto(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idfazenda}/{idsafra}/{idconta}/{idprincipio}/{idproduto}")]
        public async Task<IActionResult> ListarOrcamentoProduto(int idfazenda, int idsafra, string idconta, int idprincipio, int idproduto, string? filtro)
        {
            var conta = await _OrcamentoProdutoservice.ListarOrcamentoProduto(idfazenda, idsafra, idconta, idprincipio, idproduto, filtro);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarOrcamentoProdutoById(int id, string idconta)
        {
            var conta = await _OrcamentoProdutoservice.ListarOrcamentoProdutoById(id, idconta);
            return Ok(conta);
        }
    }
}