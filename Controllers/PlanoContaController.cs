using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ADUSAPI.Services;
using ADUSAPI.Entities;
using ADUSAPICore.Models.PlanoConta;

namespace ADUSAPI.Controllers
{
    [Route("api/planoconta")]
    [ApiController]
    public class PlanoContaController : ControllerBase
    {
        private readonly PlanoContaService _service;

        public PlanoContaController(PlanoContaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlanoConta>>> Get(string? filtro)
        {
            var planos = await _service.ListarAsync(filtro);
            return Ok(planos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoConta>> Get(int id)
        {
            var plano = await _service.BuscarPorIdAsync(id);
            if (plano == null) return NotFound();
            return Ok(plano);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PlanoContaViewModel plano)
        {
            await _service.AdicionarAsync(plano);
            return CreatedAtAction(nameof(Get), new { id = plano.Id }, plano);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, PlanoContaViewModel plano)
        {
            if (id != plano.Id) return BadRequest();
            await _service.AtualizarAsync(plano);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }
    }
}