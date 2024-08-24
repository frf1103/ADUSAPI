using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.CustosIndiretos;

using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/orcamentocustoindireto")]
    [ApiController]
    public class OrcamentoCustoIndiretoController : ControllerBase
    {
        private readonly OrcamentoCustoIndiretoService _OrcamentoCustoIndiretoservice;

        public OrcamentoCustoIndiretoController(OrcamentoCustoIndiretoService OrcamentoCustoIndiretoservice)
        {
            _OrcamentoCustoIndiretoservice = OrcamentoCustoIndiretoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarOrcamentoCustoIndireto(OrcamentoCustoIndiretoViewModel dados)
        {
            var OrcamentoCustoIndireto = await _OrcamentoCustoIndiretoservice.AdicionarOrcamentoCustoIndireto(dados);
            return Ok(OrcamentoCustoIndireto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarOrcamentoCustoIndireto(int id, OrcamentoCustoIndiretoViewModel dados)
        {
            var OrcamentoCustoIndireto = await _OrcamentoCustoIndiretoservice.SalvarOrcamentoCustoIndireto(id, dados);
            return Ok(OrcamentoCustoIndireto);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirOrcamentoCustoIndireto(int id, OrcamentoCustoIndiretoViewModel dados)
        {
            var OrcamentoCustoIndireto = await _OrcamentoCustoIndiretoservice.ExcluirOrcamentoCustoIndireto(id, dados);
            return Ok(OrcamentoCustoIndireto);
        }

        [HttpGet("filtrolista")]
        public async Task<IActionResult> ListarOrcamentoCustoIndireto(string idconta, int idcontacad, int idsafra, DateTime dini, DateTime dfim)
        {
            var OrcamentoCustoIndireto = await _OrcamentoCustoIndiretoservice.ListarOrcamentoCustoIndireto(idcontacad, idconta, idsafra, dini, dfim);
            return Ok(OrcamentoCustoIndireto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarOrcamentoCustoIndiretoById(int id)
        {
            var OrcamentoCustoIndireto = await _OrcamentoCustoIndiretoservice.ListarOrcamentoCustoIndiretoById(id);
            return Ok(OrcamentoCustoIndireto);
        }
    }
}