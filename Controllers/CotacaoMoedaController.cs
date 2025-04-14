using ADUSAPI.Services;

using ADUSAPICore.Models.Moeda;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/cotacaomoeda")]
    [ApiController]
    public class CotacaoMoedaController : ControllerBase
    {
        private readonly CotacaoMoedaService _CotacaoMoedaservice;

        public CotacaoMoedaController(CotacaoMoedaService CotacaoMoedaservice)
        {
            _CotacaoMoedaservice = CotacaoMoedaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCotacaoMoeda(CotacaoMoedaViewModel dados)
        {
            var CotacaoMoeda = await _CotacaoMoedaservice.AdicionarCotacaoMoeda(dados);
            return Ok(CotacaoMoeda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarCotacaoMoeda(int id, CotacaoMoedaViewModel dados)
        {
            var CotacaoMoeda = await _CotacaoMoedaservice.SalvarCotacaoMoeda(id, dados);
            return Ok(CotacaoMoeda);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirCotacaoMoeda(int id)
        {
            var CotacaoMoeda = await _CotacaoMoedaservice.ExcluirCotacaoMoeda(id);
            return Ok(CotacaoMoeda);
        }

        [HttpGet("Listar/{idmoeda}")]
        public async Task<IActionResult> ListarCotacaoMoeda(int idmoeda, DateTime? ini, DateTime? fim)
        {
            var CotacaoMoeda = await _CotacaoMoedaservice.ListarCotacaoMoedaByMoeda(idmoeda, ini, fim);
            return Ok(CotacaoMoeda);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarCotacaoMoedaById(int id)
        {
            var CotacaoMoeda = await _CotacaoMoedaservice.ListarCotacaoMoedaById(id);
            return Ok(CotacaoMoeda);
        }
    }
}