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

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarProdutoOrcamento(int id, string idconta, ProdutoOrcamentoViewModel dados)
        {
            var conta = await _ProdutoOrcamentoservice.SalvarProdutoOrcamento(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirProdutoOrcamento(int id, string idconta, string uid)
        {
            var conta = await _ProdutoOrcamentoservice.ExcluirProdutoOrcamento(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idorcamento}/{idprincativo}/{idproduto}/{idconta}")]
        public async Task<IActionResult> ListarProdutoOrcamento(int idorcamento, int idprincativo, int idproduto, string idconta)
        {
            var conta = await _ProdutoOrcamentoservice.ListarProdutoOrcamentoByOrcamento(idorcamento, idprincativo, idproduto, idconta);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarProdutoOrcamentoById(int id, string idconta)
        {
            var conta = await _ProdutoOrcamentoservice.ListarProdutoOrcamentoById(id, idconta);
            return Ok(conta);
        }
    }
}