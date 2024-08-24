using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Conta;
using FarmPlannerAPICore.Models.Localidades;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/Localidade")]
    [ApiController]
    public class LocalidadeController : ControllerBase
    {
        private readonly LocalidadeService _localidadeservice;

        public LocalidadeController(LocalidadeService localidadeservice)
        {
            _localidadeservice = localidadeservice;
        }

        [HttpGet("Ufs")]
        public async Task<IActionResult> ListarUF(string? filtro)
        {
            var reg = await _localidadeservice.ListarUF(filtro);
            return Ok(reg);
        }

        [HttpGet("cidades/{iduf}")]
        public async Task<IActionResult> ListarMunicipioByUF(int iduf, string? filtro)
        {
            var reg = await _localidadeservice.ListarMunicipioByUF(iduf, filtro);
            return Ok(reg);
        }
    }
}