using FarmPlannerAPI.Services;

using FarmPlannerAPICore.Models.Localidades;

using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly RegiaoService _Regiaoservice;

        public RegiaoController(RegiaoService Regiaoservice)
        {
            _Regiaoservice = Regiaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarRegiao(RegiaoViewModel dados)
        {
            var conta = await _Regiaoservice.AdicionarRegiao(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarRegiao(int id, RegiaoViewModel dados)
        {
            var conta = await _Regiaoservice.SalvarRegiao(id, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirRegiao(int id)
        {
            var conta = await _Regiaoservice.ExcluirRegiao(id);
            return Ok(conta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarRegiao(string? filtro)
        {
            var reg = await _Regiaoservice.ListarRegioes(filtro);
            return Ok(reg);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarRegiaoById(int id)
        {
            var reg = await _Regiaoservice.ListarRegiaoById(id);
            return Ok(reg);
        }
    }
}