using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Parceiro;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
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

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarParceiro(int id, string idconta, ParceiroViewModel dados)
        {
            var Parceiro = await _Parceiroservice.SalvarParceiro(id, idconta, dados);
            return Ok(Parceiro);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirParceiro(int id, string idconta)
        {
            var Parceiro = await _Parceiroservice.ExcluirParceiro(id, idconta);
            return Ok(Parceiro);
        }

        [HttpGet("{idconta}")]
        public async Task<IActionResult> ListarParceiro(string idconta, string? filtro)
        {
            var Parceiro = await _Parceiroservice.ListarParceiro(idconta, filtro);
            return Ok(Parceiro);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarParceiroById(string idconta, int id)
        {
            var Parceiro = await _Parceiroservice.ListarParceiroById(id, idconta);
            return Ok(Parceiro);
        }
    }
}