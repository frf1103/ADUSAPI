using FarmPlannerAPI.Services;

using FarmPlannerAPICore.Models.Localidades;
using FarmPlannerAPICore.Models.Unidade;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/Unidade")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {
        private readonly UnidadeService _Unidadeservice;

        public UnidadeController(UnidadeService Unidadeservice)
        {
            _Unidadeservice = Unidadeservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUnidade(UnidadeViewModel dados)
        {
            var conta = await _Unidadeservice.AdicionarUnidade(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarUnidade(int id, UnidadeViewModel dados)
        {
            var conta = await _Unidadeservice.SalvarUnidade(id, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirUnidade(int id)
        {
            var conta = await _Unidadeservice.ExcluirUnidade(id);
            return Ok(conta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarUnidade(string? filtro)
        {
            var reg = await _Unidadeservice.Listar(filtro);
            return Ok(reg);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarUnidadeById(int id)
        {
            var reg = await _Unidadeservice.ListarUnidadeById(id);
            return Ok(reg);
        }
    }
}