using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.ProdutoPlanejado;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/ProdutoPlanejado")]
    [ApiController]
    public class ProdutoPlanejadoController : ControllerBase
    {
        private readonly ProdutoPlanejadoService _ProdutoPlanejadoservice;

        public ProdutoPlanejadoController(ProdutoPlanejadoService ProdutoPlanejadoservice)
        {
            _ProdutoPlanejadoservice = ProdutoPlanejadoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProdutoPlanejado(ProdutoPlanejadoViewModel dados)
        {
            var conta = await _ProdutoPlanejadoservice.AdicionarProdutoPlanejado(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarProdutoPlanejado(int id, ProdutoPlanejadoViewModel dados)
        {
            var conta = await _ProdutoPlanejadoservice.SalvarProdutoPlanejado(id, dados);
            return Ok(conta);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirProdutoPlanejado(int id, ProdutoPlanejadoViewModel dados)
        {
            var conta = await _ProdutoPlanejadoservice.ExcluirProdutoPlanejado(id, dados);
            return Ok(conta);
        }

        [HttpGet("Listar/{idplanejamento}")]
        public async Task<IActionResult> ListarProdutoPlanejado(int idplanejamento)
        {
            var conta = await _ProdutoPlanejadoservice.ListarProdutoPlanejadoByPlanejamento(idplanejamento);
            return Ok(conta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarProdutoPlanejadoById(int id)
        {
            var conta = await _ProdutoPlanejadoservice.ListarProdutoPlanejadoById(id);
            return Ok(conta);
        }
    }
}