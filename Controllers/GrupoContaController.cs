using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.CustosIndiretos;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/grupoconta")]
    [ApiController]
    public class GrupoContaController : ControllerBase
    {
        private readonly GrupoContaService _GrupoContaservice;

        public GrupoContaController(GrupoContaService GrupoContaservice)
        {
            _GrupoContaservice = GrupoContaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarGrupoConta(GrupoContaViewModel dados)
        {
            var GrupoConta = await _GrupoContaservice.AdicionarGrupoConta(dados);
            return Ok(GrupoConta);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarGrupoConta(int id, string idconta, GrupoContaViewModel dados)
        {
            var GrupoConta = await _GrupoContaservice.SalvarGrupoConta(id, idconta, dados);
            return Ok(GrupoConta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirGrupoConta(int id, string idconta, string uid)
        {
            var GrupoConta = await _GrupoContaservice.ExcluirGrupoConta(id, idconta, uid);
            return Ok(GrupoConta);
        }

        [HttpGet("Listar/{idclasse}/{idorganizacao}/{idconta}")]
        public async Task<IActionResult> ListarGrupoConta(int idclasse, int idorganizacao, string idconta, string? filtro)
        {
            var GrupoConta = await _GrupoContaservice.ListarGrupoConta(idorganizacao, idclasse, idconta, filtro);

            return Ok(GrupoConta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarGrupoContaById(int id, string idconta)
        {
            var GrupoConta = await _GrupoContaservice.ListarGrupoContaById(id, idconta);
            return Ok(GrupoConta);
        }
    }
}