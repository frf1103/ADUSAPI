using ADUSAPI.Services;
using ADUSAPICore.Models.Parcela;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/Parcela")]
    [ApiController]
    public class ParcelaController : ControllerBase
    {
        private readonly ParcelaService _Parcelaservice;

        public ParcelaController(ParcelaService Parcelaservice)
        {
            _Parcelaservice = Parcelaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarParcela(ParcelaViewModel dados)
        {
            var Parcela = await _Parcelaservice.AdicionarParcela(dados);
            return Ok(Parcela);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarParcela(string id, ParcelaViewModel dados)
        {
            var Parcela = await _Parcelaservice.SalvarParcela(id, dados);
            return Ok(Parcela);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirParcela(string id)
        {
            var Parcela = await _Parcelaservice.ExcluirParcela(id);
            return Ok(Parcela);
        }

        [HttpGet("listar/{ini}/{fim}/{status}/{idparceiro}/{forma}/{tipodata}/{idassinatura}")]
        public async Task<IActionResult> ListarParcela(DateTime ini, DateTime fim, int tipodata, int status, string idparceiro, int forma, string idassinatura, string? filtro)
        {
            var Parcela = await _Parcelaservice.ListarParcela(ini, fim, tipodata, idparceiro, forma, filtro, status, idassinatura);
            return Ok(Parcela);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarParcelaById(string id)
        {
            var Parcela = await _Parcelaservice.ListarParcelaById(id);
            return Ok(Parcela);
        }

        [HttpGet("byAssinatura")]
        public async Task<IActionResult> ListarParcelaByAssinatura(string id, int parcela)
        {
            var Parcela = await _Parcelaservice.ListarParcelaByAssinatura(id, parcela);
            return Ok(Parcela);
        }

        [HttpGet("checkout/{id}")]
        public async Task<IActionResult> ListarParcelaByIdCheckout(string id)
        {
            var Parcela = await _Parcelaservice.ListarParcelaByIdCheckout(id);
            return Ok(Parcela);
        }

        [HttpGet("visaogeral/{idparceiro}/{ini}/{fim}")]
        public async Task<IActionResult> VisaoGeral(string idparceiro, DateTime ini, DateTime fim)
        {
            var Parcela = await _Parcelaservice.visaogeralcarteira(ini, fim, idparceiro);
            return Ok(Parcela);
        }
    }
}