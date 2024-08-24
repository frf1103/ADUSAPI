using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Tecnologia;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/tecnologia")]
    [ApiController]
    public class TecnologiaController : ControllerBase
    {
        private readonly TecnologiaService _Tecnologiaservice;

        public TecnologiaController(TecnologiaService Tecnologiaservice)
        {
            _Tecnologiaservice = Tecnologiaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTecnologia(TecnologiaViewModel dados)
        {
            var conta = await _Tecnologiaservice.AdicionarTecnologia(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarTecnologia(int id, TecnologiaViewModel dados)
        {
            var conta = await _Tecnologiaservice.SalvarTecnologia(id, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirTecnologia(int id)
        {
            var conta = await _Tecnologiaservice.ExcluirTecnologia(id);
            return Ok(conta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTecnologia(string? filtro)
        {
            var conta = await _Tecnologiaservice.ListarTecnologia(filtro);
            return Ok(conta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarTecnologiaById(int id)
        {
            var conta = await _Tecnologiaservice.ListarTecnologiaById(id);
            return Ok(conta);
        }
    }
}