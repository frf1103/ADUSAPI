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

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarCadastroConta(int id, string idconta, CadastroContaViewModel dados)
        {
            var CadastroConta = await _CadastroContaservice.SalvarCadastroConta(id, idconta, dados);
            return Ok(CadastroConta);
        }

        [HttpDelete("{id}/{idconta}/{uid}")]
        public async Task<IActionResult>? ExcluirCadastroConta(int id, string idconta, string uid)
        {
            var CadastroConta = await _CadastroContaservice.ExcluirCadastroConta(id, idconta, uid);
            return Ok(CadastroConta);
        }

        [HttpGet("Listar/{idorganizacao}/{idclasse}/{idgrupo}/{idconta}")]
        public async Task<IActionResult> ListarCadastroConta(int idorganizacao, int idclasse, int idgrupo, string idconta, string? filtro)
        {
            var CadastroConta = await _CadastroContaservice.ListarCadastroContaByOrg(idorganizacao, idclasse, idgrupo, idconta, filtro);
            return Ok(CadastroConta);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarCadastroContaById(int id, string idconta)
        {
            var CadastroConta = await _CadastroContaservice.ListarCadastroContaById(id, idconta);
            return Ok(CadastroConta);
        }
    }
}