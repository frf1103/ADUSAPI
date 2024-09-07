using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Produto;
using FarmPlannerAPICore.Models.ProdutoPrincipio;
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

        [HttpPut("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? EditarProduto(int id, string idconta, string uid, ProdutoViewModel dados)
        {
            var Produto = await _Produtoservice.SalvarProduto(id, idconta, uid, dados);
            return Ok(Produto);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirProduto(int id, string idconta, string uid, ProdutoViewModel dados)
        {
            var Produto = await _Produtoservice.ExcluirProduto(id, idconta, uid);
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

        /* Principio do produto */

        [HttpPost("principio")]
        public async Task<IActionResult> AdicionarProdutoPrincipio(ProdutoPrincipioViewModel dados)
        {
            var Produto = await _Produtoservice.AdicionarProdutoPrincipio(dados);
            return Ok(Produto);
        }

        [HttpPut("principio/{idproduto}/{idprincipio}/{idconta}/{uid}")]
        public async Task<IActionResult>? EditarProdutoPrincipio(int idproduto, int idprincipio, string idconta, string uid, ProdutoPrincipioViewModel dados)
        {
            var Produto = await _Produtoservice.SalvarProdutoPrincipio(idproduto, idprincipio, idconta, dados);
            return Ok(Produto);
        }

        [HttpDelete("principio/{idproduto}/{idprincipio}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirProduto(int idproduto, int idprincipio, string idconta, string uid)
        {
            var Produto = await _Produtoservice.ExcluirProdutoPrincipio(idproduto, idprincipio, idconta, uid);
            return Ok(Produto);
        }

        [HttpGet("Listarprincipio/{idprincipio}/{idproduto}/{idconta}")]
        public async Task<IActionResult> ListarProdutoPrincipio(string idconta, int idprincipio, int idproduto)
        {
            var Produto = await _Produtoservice.ListarProdutoPrincipio(idconta, idprincipio, idproduto);
            return Ok(Produto);
        }

        [HttpGet("principiobyid/{idproduto}/{idprincipio}/{idconta}")]
        public async Task<IActionResult> ListarProdutoPrincipioById(int idproduto, int idprincipio, string idconta)
        {
            var Produto = await _Produtoservice.ListarProdutoPrincipioById(idprincipio, idproduto, idconta);
            return Ok(Produto);
        }
    }
}