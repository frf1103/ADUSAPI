using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.GrupoProduto;
using Microsoft.AspNetCore.Mvc;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/grupoproduto")]
    [ApiController]
    public class GrupoProdutoController : ControllerBase
    {
        private readonly GrupoProdutoService _GrupoProdutoservice;

        public GrupoProdutoController(GrupoProdutoService GrupoProdutoservice)
        {
            _GrupoProdutoservice = GrupoProdutoservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarGrupoProduto(GrupoProdutoViewModel dados)
        {
            var GrupoProduto = await _GrupoProdutoservice.AdicionarGrupoProduto(dados);
            return Ok(GrupoProduto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>? EditarGrupoProduto(int id, GrupoProdutoViewModel dados)
        {
            var GrupoProduto = await _GrupoProdutoservice.SalvarGrupoProduto(id, dados);
            return Ok(GrupoProduto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>? ExcluirGrupoProduto(int id)
        {
            var GrupoProduto = await _GrupoProdutoservice.ExcluirGrupoProduto(id);
            return Ok(GrupoProduto);
        }

        [HttpGet]
        public async Task<IActionResult> ListarGrupoProduto(string? filtro)
        {
            var GrupoProduto = await _GrupoProdutoservice.ListarGrupoProduto(filtro);
            return Ok(GrupoProduto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarGrupoProdutoById(int id)
        {
            var GrupoProduto = await _GrupoProdutoservice.ListarGrupoProdutoById(id);
            return Ok(GrupoProduto);
        }
    }
}