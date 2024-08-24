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

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarEntregaContrato(int id, EntregaContratoViewModel dados)
        {
            var EntregaContrato = await _EntregaContratoservice.SalvarEntregaContrato(id, dados);
            return Ok(EntregaContrato);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirEntregaContrato(int id, EntregaContratoViewModel dados)
        {
            var EntregaContrato = await _EntregaContratoservice.ExcluirEntregaContrato(id, dados);
            return Ok(EntregaContrato);
        }

        [HttpGet("ListaByCom")]
        public async Task<IActionResult> ListarEntregaContrato(int idcomercializacao, string? filtro)
        {
            var EntregaContrato = await _EntregaContratoservice.ListarEntregaContratoByCom(idcomercializacao, filtro);
            return Ok(EntregaContrato);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarEntregaContratoById(int id)
        {
            var EntregaContrato = await _EntregaContratoservice.ListarEntregaContratoById(id);
            return Ok(EntregaContrato);
        }
    }
}