using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Maquinas;

using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/marcamaquina")]
    [ApiController]
    public class MarcaMaquinaController : ControllerBase
    {
        private readonly MarcaMaquinaService _MarcaMaquinaservice;

        public MarcaMaquinaController(MarcaMaquinaService MarcaMaquinaservice)
        {
            _MarcaMaquinaservice = MarcaMaquinaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarMarcaMaquina(MarcaMaquinaViewModel dados)
        {
            var MarcaMaquina = await _MarcaMaquinaservice.AdicionarMarcaMaquina(dados);
            return Ok(MarcaMaquina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarMarcaMaquina(int id, MarcaMaquinaViewModel dados)
        {
            var MarcaMaquina = await _MarcaMaquinaservice.SalvarMarcaMaquina(id, dados);
            return Ok(MarcaMaquina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirMarcaMaquina(int id)
        {
            var MarcaMaquina = await _MarcaMaquinaservice.ExcluirMarcaMaquina(id);
            return Ok(MarcaMaquina);
        }

        [HttpGet]
        public async Task<IActionResult> ListarMarcaMaquina(string? filtro)
        {
            var MarcaMaquina = await _MarcaMaquinaservice.ListarMarcaMaquina(filtro);
            return Ok(MarcaMaquina);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarMarcaMaquinaById(int id)
        {
            var MarcaMaquina = await _MarcaMaquinaservice.ListarMarcaMaquinaById(id);
            return Ok(MarcaMaquina);
        }
    }
}