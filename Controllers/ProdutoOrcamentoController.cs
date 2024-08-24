using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.OrcamentoProduto;

using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/produtoorcamento")]
    [ApiController]
    public class ProdutoOrcamentoController : ControllerBase
    {
        private readonly ProdutoOrcamentoService _ProdutoOrcamentoservice;

        public ProdutoOrcamentoController(ProdutoOrcamentoService ProdutoOrcamentoservice)
        {
            _ProdutoOrcamentoservice = ProdutoOrcamentoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProdutoOrcamento(ProdutoOrcamentoViewModel dados)
        {
            var conta = await _ProdutoOrcamentoservice.AdicionarProdutoOrcamento(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarProdutoOrcamento(int id, ProdutoOrcamentoViewModel dados)
        {
            var conta = await _ProdutoOrcamentoservice.SalvarProdutoOrcamento(id, dados);
            return Ok(conta);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirProdutoOrcamento(int id, ProdutoOrcamentoViewModel dados)
        {
            var conta = await _ProdutoOrcamentoservice.ExcluirProdutoOrcamento(id, dados);
            return Ok(conta);
        }

        [HttpGet("Listar/{idorcamento}/{idprincativo}/{idproduto}")]
        public async Task<IActionResult> ListarProdutoOrcamento(int idorcamento, int idprincativo, int idproduto)
        {
            var conta = await _ProdutoOrcamentoservice.ListarProdutoOrcamentoByOrcamento(idorcamento, idprincativo, idproduto);
            return Ok(conta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarProdutoOrcamentoById(int id)
        {
            var conta = await _ProdutoOrcamentoservice.ListarProdutoOrcamentoById(id);
            return Ok(conta);
        }
    }
}