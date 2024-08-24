using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Produto;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _Produtoservice;

        public ProdutoController(ProdutoService Produtoservice)
        {
            _Produtoservice = Produtoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(ProdutoViewModel dados)
        {
            var Produto = await _Produtoservice.AdicionarProduto(dados);
            return Ok(Produto);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarProduto(int id, string idconta, ProdutoViewModel dados)
        {
            var Produto = await _Produtoservice.SalvarProduto(id, idconta, dados);
            return Ok(Produto);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirProduto(int id, string idconta, ProdutoViewModel dados)
        {
            var Produto = await _Produtoservice.ExcluirProduto(id, idconta);
            return Ok(Produto);
        }

        [HttpGet("Listar/{idgrupo}/{idfab}/{idprincipio}/{idconta}")]
        public async Task<IActionResult> ListarProduto(string idconta, int idgrupo, int idfab, int idprincipio, string? filtro)
        {
            var Produto = await _Produtoservice.ListarProduto(filtro, idconta, idgrupo, idfab, idprincipio);
            return Ok(Produto);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarProdutoById(int id, string idconta)
        {
            var Produto = await _Produtoservice.ListarProdutoById(id, idconta);
            return Ok(Produto);
        }
    }
}