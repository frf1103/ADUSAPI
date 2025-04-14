using ADUSAPI.Services;
using ADUSAPICore.Models.Parceiro;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/parceiro")]
    [ApiController]
    public class ParceiroController : ControllerBase
    {
        private readonly ParceiroService _Parceiroservice;

        public ParceiroController(ParceiroService Parceiroservice)
        {
            _Parceiroservice = Parceiroservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarParceiro(ParceiroViewModel dados)
        {
            var Parceiro = await _Parceiroservice.AdicionarParceiro(dados);
            return Ok(Parceiro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarParceiro(string id, ParceiroViewModel dados)
        {
            var Parceiro = await _Parceiroservice.SalvarParceiro(id, dados);
            return Ok(Parceiro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirParceiro(string id)
        {
            var Parceiro = await _Parceiroservice.ExcluirParceiro(id);
            return Ok(Parceiro);
        }

        [HttpGet]
        public async Task<IActionResult> ListarParceiro(string? filtro)
        {
            var Parceiro = await _Parceiroservice.ListarParceiro(filtro);
            return Ok(Parceiro);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarParceiroById(string id)
        {
            var Parceiro = await _Parceiroservice.ListarParceiroById(id);
            return Ok(Parceiro);
        }
    }
}