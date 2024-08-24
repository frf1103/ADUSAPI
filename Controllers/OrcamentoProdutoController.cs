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

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarOrcamentoProduto(int id, OrcamentoProdutoViewModel dados)
        {
            var conta = await _OrcamentoProdutoservice.SalvarOrcamentoProduto(id, dados);
            return Ok(conta);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirOrcamentoProduto(int id, OrcamentoProdutoViewModel dados)
        {
            var conta = await _OrcamentoProdutoservice.ExcluirOrcamentoProduto(id, dados);
            return Ok(conta);
        }

        [HttpGet("Listar/{idfazenda}/{idsafra}")]
        public async Task<IActionResult> ListarOrcamentoProduto(int idfazenda, int idsafra, string? filtro)
        {
            var conta = await _OrcamentoProdutoservice.ListarOrcamentoProduto(idfazenda, idsafra, filtro);
            return Ok(conta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarOrcamentoProdutoById(int id)
        {
            var conta = await _OrcamentoProdutoservice.ListarOrcamentoProdutoById(id);
            return Ok(conta);
        }
    }
}