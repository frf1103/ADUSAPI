using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Maquinas;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/modelomaquina")]
    [ApiController]
    public class ModeloMaquinaController : ControllerBase
    {
        private readonly ModeloMaquinaService _ModeloMaquinaservice;

        public ModeloMaquinaController(ModeloMaquinaService ModeloMaquinaservice)
        {
            _ModeloMaquinaservice = ModeloMaquinaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarModeloMaquina(ModeloMaquinaViewModel dados)
        {
            var ModeloMaquina = await _ModeloMaquinaservice.AdicionarModeloMaquina(dados);
            return Ok(ModeloMaquina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarModeloMaquina(int id, ModeloMaquinaViewModel dados)
        {
            var ModeloMaquina = await _ModeloMaquinaservice.SalvarModeloMaquina(id, dados);
            return Ok(ModeloMaquina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirModeloMaquina(int id)
        {
            var ModeloMaquina = await _ModeloMaquinaservice.ExcluirModeloMaquina(id);
            return Ok(ModeloMaquina);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarModeloMaquina(int idmarca, string? filtro)
        {
            var ModeloMaquina = await _ModeloMaquinaservice.ListarModeloMaquina(filtro, idmarca);
            return Ok(ModeloMaquina);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarModeloMaquinaById(int id)
        {
            var ModeloMaquina = await _ModeloMaquinaservice.ListarModeloMaquinaById(id);
            return Ok(ModeloMaquina);
        }
    }
}