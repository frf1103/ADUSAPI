using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Comercializacao;

using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/entregacontrato")]
    [ApiController]
    public class EntregaContratoController : ControllerBase
    {
        private readonly EntregaContratoService _EntregaContratoservice;

        public EntregaContratoController(EntregaContratoService EntregaContratoservice)
        {
            _EntregaContratoservice = EntregaContratoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEntregaContrato(EntregaContratoViewModel dados)
        {
            var EntregaContrato = await _EntregaContratoservice.AdicionarEntregaContrato(dados);
            return Ok(EntregaContrato);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarEntregaContrato(int id, string idconta, EntregaContratoViewModel dados)
        {
            var EntregaContrato = await _EntregaContratoservice.SalvarEntregaContrato(id, idconta, dados);
            return Ok(EntregaContrato);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirEntregaContrato(int id, string idconta, string uid)
        {
            var EntregaContrato = await _EntregaContratoservice.ExcluirEntregaContrato(id, idconta, uid);
            return Ok(EntregaContrato);
        }

        [HttpGet("ListaByCom/{id}/{idconta}")]
        public async Task<IActionResult> ListarEntregaContrato(int id, string idconta, string? filtro)
        {
            var EntregaContrato = await _EntregaContratoservice.ListarEntregaContratoByCom(id, idconta, filtro);
            return Ok(EntregaContrato);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarEntregaContratoById(int id, string idconta)
        {
            var EntregaContrato = await _EntregaContratoservice.ListarEntregaContratoById(id, idconta);
            return Ok(EntregaContrato);
        }
    }
}