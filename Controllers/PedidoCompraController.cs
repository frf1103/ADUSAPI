using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.PedidoCompra;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/PedidoCompra")]
    [ApiController]
    public class PedidoCompraController : ControllerBase
    {
        private readonly PedidoCompraService _PedidoCompraservice;

        public PedidoCompraController(PedidoCompraService PedidoCompraservice)
        {
            _PedidoCompraservice = PedidoCompraservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPedidoCompra(PedidoCompraViewModel dados)
        {
            var conta = await _PedidoCompraservice.AdicionarPedidoCompra(dados);
            return Ok(conta);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarPedidoCompra(int id, string idconta, PedidoCompraViewModel dados)
        {
            var conta = await _PedidoCompraservice.SalvarPedidoCompra(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirPedidoCompra(int id, string idconta, string uid)
        {
            var conta = await _PedidoCompraservice.ExcluirPedidoCompra(id, idconta, uid);
            return Ok(conta);
        }

        [HttpGet("Listar/{idorganizacao}/{idano}/{idfazenda}/{idsafra}/{idmoeda}/{idproduto}/{idconta}")]
        public async Task<IActionResult> ListarPedidoCompra(int idorganizacao, int idfazenda, int idano, int idsafra, string idconta, int idmoeda, int idproduto, string? filtro)
        {
            var conta = await _PedidoCompraservice.ListarPedidoCompra(idorganizacao, idano, idfazenda, idsafra, idconta, idproduto, idmoeda, filtro);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarPedidoCompraById(int id, string idconta)
        {
            var conta = await _PedidoCompraservice.ListarPedidoCompraById(id, idconta);
            return Ok(conta);
        }
    }
}