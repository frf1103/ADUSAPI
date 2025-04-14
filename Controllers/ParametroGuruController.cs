using ADUSAPI.Services;
using ADUSAPICore.Models;

using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/ParametroGuru")]
    [ApiController]
    public class ParametroGuruController : ControllerBase
    {
        private readonly ParametrosGuruService _ParametroGuruservice;

        public ParametroGuruController(ParametrosGuruService ParametroGuruservice)
        {
            _ParametroGuruservice = ParametroGuruservice;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarParametroGuru(int id, ParametrosGuruViewModel dados)
        {
            var ParametroGuru = await _ParametroGuruservice.SalvarParametrosGuru(id, dados);
            return Ok(ParametroGuru);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarParametroGuruById(int id)
        {
            var ParametroGuru = await _ParametroGuruservice.ListarParametrosGuruById(id);
            return Ok(ParametroGuru);
        }
    }
}