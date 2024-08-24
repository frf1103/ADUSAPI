using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Organizacao;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/organizacao")]
    [ApiController]
    public class OrganizacaoController : ControllerBase
    {
        private readonly OrganizacaoService _Organizacaoservice;

        public OrganizacaoController(OrganizacaoService Organizacaoservice)
        {
            _Organizacaoservice = Organizacaoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarOrganizacao(AdicionarOrganizacaoViewModel dados)
        {
            var Organizacao = await _Organizacaoservice.AdicionarOrganizacao(dados);
            return Ok(Organizacao);
        }

        [HttpPut("{idconta}/{id}")]
        public async Task<IActionResult>? EditarOrganizacao(string idconta, int id, EditarOrganizacaoViewModel dados)
        {
            var Organizacao = await _Organizacaoservice.SalvarOrganizacao(idconta, id, dados);
            return Ok(Organizacao);
        }

        [HttpDelete("{idconta}/{id}")]
        public async Task<IActionResult>? ExcluirOrganizacao(string idconta, int id)
        {
            var Organizacao = await _Organizacaoservice.ExcluirOrganizacao(id, idconta);
            return Ok(Organizacao);
        }

        [HttpGet]
        public async Task<IActionResult> ListarOrganizacao(string? filtro)
        {
            var Organizacao = await _Organizacaoservice.ListarOrganizacao(filtro);
            return Ok(Organizacao);
        }

        [HttpGet("byid/{idconta}/{id}")]
        public async Task<IActionResult> ListarOrganizacaoById(string idconta, int id)
        {
            var Organizacao = await _Organizacaoservice.ListarOrganizacaoById(idconta, id);
            return Ok(Organizacao);
        }

        [HttpGet("listar/{idconta}")]
        public async Task<IActionResult> ListarOrganizacaoByConta(string idconta, string? filtro)
        {
            var Organizacao = await _Organizacaoservice.ListarOrganizacaoByConta(idconta, filtro);
            return Ok(Organizacao);
        }

        [HttpGet("usuariobyorg/{idorg}")]
        public async Task<IActionResult> ListarUsuariosByOrg(int idorg)
        {
            var Organizacao = await _Organizacaoservice.ListarUsuariosByOrg(idorg);
            return Ok(Organizacao);
        }

        [HttpGet("orgbyuid/{uid}")]
        public async Task<IActionResult> ListarOrganizacaoByUid(string uid)
        {
            var Organizacao = await _Organizacaoservice.ListarOrganizacaoByUid(uid);
            return Ok(Organizacao);
        }

        [HttpPost("usuario")]
        public async Task<IActionResult> AdicionarOrganizacaoUsuario(EditarOrganizacaoUsuarioViewModel dados)
        {
            var Organizacao = await _Organizacaoservice.AdicionarOrganizacaoUsuario(dados);
            return Ok(Organizacao);
        }

        [HttpPut("usuario/{id}")]
        public async Task<IActionResult>? EditarOrganizacaoUsuario(int id, EditarOrganizacaoUsuarioViewModel dados)
        {
            var Organizacao = await _Organizacaoservice.SalvarOrganizacaoUsuario(id, dados);
            return Ok(Organizacao);
        }

        [HttpDelete("usuario/{id}")]
        public async Task<IActionResult>? ExcluirOrganizacaoUsuario(int id)
        {
            var Organizacao = await _Organizacaoservice.ExcluirOrganizacaoUsuario(id);
            return Ok(Organizacao);
        }
    }
}