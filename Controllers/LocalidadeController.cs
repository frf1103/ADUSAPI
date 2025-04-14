using ADUSAPI.Services;
using ADUSAPICore.Models.Localidades;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
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

        [HttpGet("Ufs/{ibge}")]
        public async Task<IActionResult> ListarUFIBGE(string ibge)
        {
            var reg = await _localidadeservice.GetUFByIBGE(ibge);
            return Ok(reg);
        }

        [HttpGet("cidadesibge/{ibge}")]
        public async Task<IActionResult> ListarCidadesIBGE(string ibge)
        {
            var reg = await _localidadeservice.GetCidadeByIBGE(ibge);
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