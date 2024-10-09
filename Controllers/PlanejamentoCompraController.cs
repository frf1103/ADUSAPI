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

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarPlanejamentoCompra(int id, string idconta, PlanejamentoCompraViewModel dados)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.SalvarPlanejamentoCompra(id, idconta, dados);
            return Ok(PlanejamentoCompra);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirPlanejamentoCompra(int id, string idconta, string uid)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.ExcluirPlanejamentoCompra(id, idconta, uid);
            return Ok(PlanejamentoCompra);
        }

        [HttpGet("listar/{idconta}/{idano}/{idorganizacao}/{idsafra}/{idprincipio}/{idfazenda}/{idproduto}")]
        public async Task<IActionResult> ListarPlanejamentoCompra(string idconta, int idano, int idorganizacao, int idsafra, int idprincipio, int idfazenda, int idproduto)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.ListarPlanejamento(idconta, idano, idorganizacao, idprincipio, idsafra, idfazenda, idproduto);

            return Ok(PlanejamentoCompra);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarPlanejamentoCompraById(int id, string idconta)
        {
            var PlanejamentoCompra = await _PlanejamentoCompraservice.ListarPlanejamentoCompraById(id, idconta);
            return Ok(PlanejamentoCompra);
        }
    }
}