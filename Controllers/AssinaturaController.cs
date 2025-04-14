using ADUSAPI.Services;
using ADUSAPICore.Models.Assinatura;
using Microsoft.AspNetCore.Mvc;

namespace ADUSAPI.Controllers
{
    [Route("api/Assinatura")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly AssinaturaService _Assinaturaservice;

        public AssinaturaController(AssinaturaService Assinaturaservice)
        {
            _Assinaturaservice = Assinaturaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAssinatura(AssinaturaViewModel dados)
        {
            var Assinatura = await _Assinaturaservice.AdicionarAssinatura(dados);
            return Ok(Assinatura);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarAssinatura(string id, AssinaturaViewModel dados)
        {
            var Assinatura = await _Assinaturaservice.SalvarAssinatura(id, dados);
            return Ok(Assinatura);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirAssinatura(string id)
        {
            var Assinatura = await _Assinaturaservice.ExcluirAssinatura(id);
            return Ok(Assinatura);
        }

        [HttpGet("listar/{ini}/{fim}/{status}/{idparceiro}/{forma}")]
        public async Task<IActionResult> ListarAssinatura(DateTime ini, DateTime fim, int status, string idparceiro, int forma, string? filtro)
        {
            var Assinatura = await _Assinaturaservice.ListarAssinatura(ini, fim, idparceiro, status, forma, filtro);
            return Ok(Assinatura);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarAssinaturaById(string id)
        {
            var Assinatura = await _Assinaturaservice.ListarAssinaturaById(id);
            return Ok(Assinatura);
        }
    }
}