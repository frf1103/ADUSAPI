using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.CustosIndiretos;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/classeconta")]
    [ApiController]
    public class ClasseContaController : ControllerBase
    {
        private readonly ClasseContaService _ClasseContaservice;

        public ClasseContaController(ClasseContaService ClasseContaservice)
        {
            _ClasseContaservice = ClasseContaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarClasseConta(ClasseContaViewModel dados)
        {
            var ClasseConta = await _ClasseContaservice.AdicionarClasseConta(dados);
            return Ok(ClasseConta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarClasseConta(int id, ClasseContaViewModel dados)
        {
            var ClasseConta = await _ClasseContaservice.SalvarClasseConta(id, dados);
            return Ok(ClasseConta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirClasseConta(int id)
        {
            var ClasseConta = await _ClasseContaservice.ExcluirClasseConta(id);
            return Ok(ClasseConta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarClasseConta(string? filtro)
        {
            var ClasseConta = await _ClasseContaservice.ListarClasseConta(filtro);
            return Ok(ClasseConta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarClasseContaById(int id)
        {
            var ClasseConta = await _ClasseContaservice.ListarClasseContaById(id);
            return Ok(ClasseConta);
        }
    }
}