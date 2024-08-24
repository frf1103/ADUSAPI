using FarmPlannerAPI.Services;

using FarmPlannerAPICore.Models.CustosIndiretos;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/cadastroconta")]
    [ApiController]
    public class CadastroContaController : ControllerBase
    {
        private readonly CadastroContaService _CadastroContaservice;

        public CadastroContaController(CadastroContaService CadastroContaservice)
        {
            _CadastroContaservice = CadastroContaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCadastroConta(CadastroContaViewModel dados)
        {
            var CadastroConta = await _CadastroContaservice.AdicionarCadastroConta(dados);
            return Ok(CadastroConta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarCadastroConta(int id, CadastroContaViewModel dados)
        {
            var CadastroConta = await _CadastroContaservice.SalvarCadastroConta(id, dados);
            return Ok(CadastroConta);
        }

        [HttpDelete("id")]
        public async Task<IActionResult>? ExcluirCadastroConta(int id, CadastroContaViewModel dados)
        {
            var CadastroConta = await _CadastroContaservice.ExcluirCadastroConta(id, dados);
            return Ok(CadastroConta);
        }

        [HttpGet("Listar/{idorganizacao}")]
        public async Task<IActionResult> ListarCadastroConta(int idorganizacao, string? filtro)
        {
            var CadastroConta = await _CadastroContaservice.ListarCadastroContaByOrg(idorganizacao, filtro);
            return Ok(CadastroConta);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarCadastroContaById(int id)
        {
            var CadastroConta = await _CadastroContaservice.ListarCadastroContaById(id);
            return Ok(CadastroConta);
        }
    }
}