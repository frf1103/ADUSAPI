using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.PlanejamentoCompra;
using FarmPlannerAPICore.Models.TipoOperacao;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/planejamentocompra")]
    [ApiController]
    public class PlanejamentoCompraController : ControllerBase
    {
        private readonly PlanejamentoCompraService _PlanejamentoCompraservice;

        public PlanejamentoCompraController(PlanejamentoCompraService PlanejamentoCompraservice)
        {
            _PlanejamentoCompraservice = PlanejamentoCompraservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPlanejamentoCompra(PlanejamentoCompraViewModel dados)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.AdicionarPlanejamentoCompra(dados);
            return Ok(PlanejamentoCompra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarPlanejamentoCompra(int id, PlanejamentoCompraViewModel dados)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.SalvarPlanejamentoCompra(id, dados);
            return Ok(PlanejamentoCompra);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirPlanejamentoCompra(int id, PlanejamentoCompraViewModel dados)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.ExcluirPlanejamentoCompra(id, dados);
            return Ok(PlanejamentoCompra);
        }

        [HttpGet("Listar/{idconta}/{idano},{idorganizacao}/{idsafra}/{idproduto}")]
        public async Task<IActionResult> ListarPlanejamentoCompra(string idconta, int idano, int idorganizacao, int idsafra, int idproduto)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.ListarPlanejamento(idconta, idano, idorganizacao, idproduto, idsafra);
            return Ok(PlanejamentoCompra);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarPlanejamentoCompraById(int id)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.ListarPlanejamentoCompraById(id);
            return Ok(PlanejamentoCompra);
        }
    }
}