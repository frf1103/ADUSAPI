using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.PedidoCompra;

using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/ProdutoCompra")]
    [ApiController]
    public class ProdutoCompraController : ControllerBase
    {
        private readonly ProdutoCompraService _ProdutoCompraservice;

        public ProdutoCompraController(ProdutoCompraService ProdutoCompraservice)
        {
            _ProdutoCompraservice = ProdutoCompraservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProdutoCompra(ProdutoCompraViewModel dados)
        {
            var conta = await _ProdutoCompraservice.AdicionarProdutoCompra(dados);
            return Ok(conta);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarProdutoCompra(int id, string idconta, ProdutoCompraViewModel dados)
        {
            var conta = await _ProdutoCompraservice.SalvarProdutoCompra(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirProdutoCompra(int id, string idconta, string uid)
        {
            var conta = await _ProdutoCompraservice.ExcluirProdutoCompra(id, idconta, uid);
            return Ok(conta);
        }

        [HttpDelete("Entrega/{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirEntregaById(int id, string idconta, string uid)
        {
            var conta = await _ProdutoCompraservice.ExcluirEntregaCompraById(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idpedido}/{idproduto}/{idconta}")]
        public async Task<IActionResult> ListarProdutoCompra(int idpedido, int idproduto, string idconta)
        {
            //var conta = await _ProdutoCompraservice.ListarProdutoCompraByPedido(idpedido, idproduto, idconta);
            var conta = await _ProdutoCompraservice.ListarItensEntrega(idpedido, idconta, idproduto, 0);
            return Ok(conta);
        }

        [HttpGet("ItensEntrega/{idpedido}/{idconta}/{idproduto}")]
        public async Task<IActionResult> ListarItensEntrega(int idpedido, string idconta, int idproduto)
        {
            var conta = await _ProdutoCompraservice.ListarItensEntrega(idpedido, idconta, idproduto);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarProdutoCompraById(int id, string idconta)
        {
            var conta = await _ProdutoCompraservice.ListarProdutoCompraById(id, idconta);
            return Ok(conta);
        }

        [HttpPost("Entrega")]
        public async Task<IActionResult> AdicionarProdutoCompra(EntregaCompraViewModel dados)
        {
            var conta = await _ProdutoCompraservice.AdicionarEntrega(dados);
            return Ok(conta);
        }

        [HttpGet("EntregaByItem/{id}/{idconta}")]
        public async Task<IActionResult> ListarEntregByProduto(int id, string idconta)
        {
            var conta = await _ProdutoCompraservice.ListarEntregasByProduto(id, idconta);
            return Ok(conta);
        }
    }
}