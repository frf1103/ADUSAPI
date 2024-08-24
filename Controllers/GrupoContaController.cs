using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.CustosIndiretos;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/gripoconta")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarGrupoConta(int id, GrupoContaViewModel dados)
        {
            var GrupoConta = await _GrupoContaservice.SalvarGrupoConta(id, dados);
            return Ok(GrupoConta);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirGrupoConta(int id, GrupoContaViewModel dados)
        {
            var GrupoConta = await _GrupoContaservice.ExcluirGrupoConta(id, dados);
            return Ok(GrupoConta);
        }

        [HttpGet("Listar/{idclasse}/{idorganizaco}")]
        public async Task<IActionResult> ListarGrupoConta(int idclasse, int idorganizacao, string? filtro)
        {
            var GrupoConta = await _GrupoContaservice.ListarGrupoConta(idorganizacao, idclasse, filtro);

            return Ok(GrupoConta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarGrupoContaById(int id)
        {
            var GrupoConta = await _GrupoContaservice.ListarGrupoContaById(id);
            return Ok(GrupoConta);
        }
    }
}