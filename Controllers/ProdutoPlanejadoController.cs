using FarmPlannerAPI.Entities;
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
            var (conta, erros) = await _ProdutoPlanejadoservice.AdicionarProdutoPlanejado(dados);
            if (erros == null) { return Ok(conta); }
            else { return BadRequest(new { success = false, erros }); }
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarProdutoPlanejado(int id, string idconta, ProdutoPlanejadoViewModel dados)
        {
            var conta = await _ProdutoPlanejadoservice.SalvarProdutoPlanejado(id, idconta, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirProdutoPlanejado(int id, string idconta, string uid)
        {
            var conta = await _ProdutoPlanejadoservice.ExcluirProdutoPlanejado(id, uid, idconta);
            return Ok(conta);
        }

        [HttpGet("Listar/{idplanejamento}/{idconta}")]
        public async Task<IActionResult> ListarProdutoPlanejado(int idplanejamento, string idconta)
        {
            var conta = await _ProdutoPlanejadoservice.ListarProdutoPlanejadoByPlanejamento(idplanejamento, idconta);
            return Ok(conta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarProdutoPlanejadoById(int id, string idconta)
        {
            var conta = await _ProdutoPlanejadoservice.ListarProdutoPlanejadoById(id, idconta);
            return Ok(conta);
        }
    }
}