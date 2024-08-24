using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Safra;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/safra")]
    [ApiController]
    public class SafraController : ControllerBase
    {
        private readonly SafraService _Safraservice;

        public SafraController(SafraService Safraservice)
        {
            _Safraservice = Safraservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarSafra(SafraViewModel dados)
        {
            var Safra = await _Safraservice.AdicionarSafra(dados);
            return Ok(Safra);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarSafra(int id, string idconta, SafraViewModel dados)
        {
            var Safra = await _Safraservice.SalvarSafra(id, idconta, dados);
            return Ok(Safra);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirSafra(int id, string idconta)
        {
            var Safra = await _Safraservice.ExcluirSafra(id, idconta);
            return Ok(Safra);
        }

        [HttpGet("Listar/{idanoagricola}/{idcultura}/{idconta}")]
        public async Task<IActionResult> ListarSafra(int idanoagricola, string idconta, int idcultura, string? filtro)
        {
            var Safra = await _Safraservice.ListarSafra(idanoagricola, idconta, idcultura, filtro);
            return Ok(Safra);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarSafraById(int id, string idconta)
        {
            var Safra = await _Safraservice.ListarSafraById(id, idconta);
            return Ok(Safra);
        }
    }
}