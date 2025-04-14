using ADUSAPI.Services;
using ADUSAPICore.Models.Moeda;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/moeda")]
    [ApiController]
    public class MoedaController : ControllerBase
    {
        private readonly MoedaService _Moedaservice;

        public MoedaController(MoedaService Moedaservice)
        {
            _Moedaservice = Moedaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarMoeda(MoedaViewModel dados)
        {
            var Moeda = await _Moedaservice.AdicionarMoeda(dados);
            return Ok(Moeda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarMoeda(int id, MoedaViewModel dados)
        {
            var Moeda = await _Moedaservice.SalvarMoeda(id, dados);
            return Ok(Moeda);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirMoeda(int id)
        {
            var Moeda = await _Moedaservice.ExcluirMoeda(id);
            return Ok(Moeda);
        }

        [HttpGet]
        public async Task<IActionResult> ListarMoeda(string? filtro)
        {
            var Moeda = await _Moedaservice.ListarMoeda(filtro);
            return Ok(Moeda);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarMoedaById(int id)
        {
            var Moeda = await _Moedaservice.ListarMoedaById(id);
            return Ok(Moeda);
        }
    }
}