using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Conta;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ContaService _contaservice;

        public ContaController(ContaService contaservice)
        {
            _contaservice = contaservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(AdicionarUsuarioConta dados)
        {
            var conta = await _contaservice.AdicionarConta(dados);
            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarConta(string id, EditarContaViewModel dados)
        {
            var conta = await _contaservice.SalvarConta(id, dados);
            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirConta(string id, EditarContaViewModel dados)
        {
            var conta = await _contaservice.ExcluirConta(id, dados);
            return Ok(conta);
        }

        [HttpGet]
        public async Task<IActionResult> ListarConta(string? filtro)
        {
            var conta = await _contaservice.ListarConta(filtro);
            return Ok(conta);
        }

        [HttpGet("ByRep/{repid}")]
        public async Task<IActionResult> ListarContaByRep(string repid, string? filtro)
        {
            var conta = await _contaservice.ListarContaByRep(filtro, repid);
            return Ok(conta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarContaById(string id)
        {
            var conta = await _contaservice.ListarContaById(id);
            return Ok(conta);
        }

        [HttpGet("UID/{uid}")]
        public async Task<IActionResult> ListarContaByUId(string uid)
        {
            var conta = await _contaservice.ListarContaByUId(uid);
            if (conta == null) return NotFound();
            return Ok(conta);
        }

        /*
                [HttpGet("ContaUID/{uid}")]
                public async Task<IActionResult> ListarContaByGUId(string uid)
                {
                    var conta = await _contaservice.ListarContaByGUId(uid);
                    return Ok(conta);
                }
        */

        [HttpGet("UsuarioConta/{uid}")]
        public async Task<IActionResult> GetContaByUid(string uid)
        {
            var conta = await _contaservice.GetContaByUid(uid);
            if (conta == null)
                return NotFound();
            return Ok(conta);
        }

        [HttpGet("Usuarios/{contaguid}")]
        public async Task<IActionResult> ListUsuariosByConta(string contaguid)
        {
            var conta = await _contaservice.ListUsuariosByConta(contaguid);
            return Ok(conta);
        }

        [HttpGet("financeirobyconta/{contaguid}")]
        public async Task<IActionResult> ListFinanceiroByConta(string contaguid)
        {
            var conta = await _contaservice.ListFinanceiroByConta(contaguid);
            return Ok(conta);
        }

        [HttpGet("financeirobyid/{id}")]
        public async Task<IActionResult> ListFinanceiroByConta(int id)
        {
            var conta = await _contaservice.ListFinanceiroById(id);
            return Ok(conta);
        }

        [HttpPost("UsuarioConta")]
        public async Task<IActionResult> AddUsuarioConta(UsuarioContaViewModel dados)
        {
            var conta = await _contaservice.AdicionarUsuarioConta(dados);
            return Ok(conta);
        }

        [HttpPost("financeiroconta")]
        public async Task<IActionResult> AddFinanceiroConta(FinanceiroContaViewModel dados)
        {
            var conta = await _contaservice.AdicionarFinanceiroConta(dados);
            return Ok(conta);
        }

        [HttpPut("financeiroconta")]
        public async Task<IActionResult> SalvarFinanceiroConta(int id, FinanceiroContaViewModel dados)
        {
            var conta = await _contaservice.SalvarFinanceiroConta(id, dados);
            return Ok(conta);
        }

        [HttpPost("assinaturaconta")]
        public async Task<IActionResult> AddAssinaturaConta(AssinaturaContaViewModel dados)
        {
            var conta = await _contaservice.AdicionarAssinaturaConta(dados);
            return Ok(conta);
        }

        [HttpPut("assinaturaconta/{id}")]
        public async Task<IActionResult> SalvarAssinaturaConta(int id, AssinaturaContaViewModel dados)
        {
            var conta = await _contaservice.SalvarAssinaturaConta(id, dados);
            return Ok(conta);
        }

        [HttpGet("assinaturabyconta/{contaguid}")]
        public async Task<IActionResult> ListAssinturaByConta(string contaguid)
        {
            var conta = await _contaservice.ListAssinaturaByConta(contaguid);
            return Ok(conta);
        }

        [HttpGet("assinaturabyid/{id}")]
        public async Task<IActionResult> ListAssinturaByConta(int id)
        {
            var conta = await _contaservice.ListAssinaturaById(id);
            return Ok(conta);
        }
    }
}