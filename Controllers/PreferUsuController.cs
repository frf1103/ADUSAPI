using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/preferusu")]
    [ApiController]
    public class PreferUsuController : ControllerBase
    {
        private readonly PreferUsuService _PreferUsuservice;

        public PreferUsuController(PreferUsuService PreferUsuservice)
        {
            _PreferUsuservice = PreferUsuservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPreferUsu(PreferUsuViewModel dados)
        {
            var PreferUsu = await _PreferUsuservice.AdicionarPreferUsu(dados);
            return Ok(PreferUsu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarPreferUsu(string id, PreferUsuViewModel dados)
        {
            var PreferUsu = await _PreferUsuservice.SalvarPreferUsu(id, dados);
            return Ok(PreferUsu);
        }

        [HttpGet]
        public async Task<IActionResult> ListarPreferUsu(string uid)
        {
            var PreferUsu = await _PreferUsuservice.ListarPreferUsu(uid);
            if (PreferUsu == null)
            {
                return NoContent();
            }
            return Ok(PreferUsu);
        }
    }
}