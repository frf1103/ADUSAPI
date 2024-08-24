using FarmPlannerAPI.Services;
using FarmPlannerAPICore.Models.Maquinas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace FarmPlannerAPI.Controllers
{
    [Route("api/modeloparametro")]
    [ApiController]
    public class ModeloParametroController : ControllerBase
    {
        private readonly ModeloParametroService _ModeloParametroservice;

        public ModeloParametroController(ModeloParametroService ModeloParametroservice)
        {
            _ModeloParametroservice = ModeloParametroservice;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarModeloParametro(ModeloParametroViewModel dados)
        {
            var ModeloParametro = await _ModeloParametroservice.AdicionarModeloParametro(dados);
            return Ok(ModeloParametro);
        }

        [HttpPut("{id}/{idconta}")]
        public async Task<IActionResult>? EditarModeloParametro(int id, string idconta, ModeloParametroViewModel dados)
        {
            var ModeloParametro = await _ModeloParametroservice.SalvarModeloParametro(id, idconta, dados);
            return Ok(ModeloParametro);
        }

        [HttpDelete("{id}/{idconta}")]
        public async Task<IActionResult>? ExcluirModeloParametro(int id, string idconta)
        {
            var ModeloParametro = await _ModeloParametroservice.ExcluirModeloParametro(id, idconta);
            return Ok(ModeloParametro);
        }

        [HttpGet("Listar/{idcultura}/{idmodelo}/{idoperacao}/{idconta}")]
        public async Task<IActionResult> ListarModeloParametro(int idcultura, int idmodelo, int idoperacao, string idconta)
        {
            var ModeloParametro = await _ModeloParametroservice.ListarModeloParametroByModelo(idmodelo, idcultura, idoperacao, idconta);
            return Ok(ModeloParametro);
        }

        [HttpGet("{id}/{idconta}")]
        public async Task<IActionResult> ListarModeloParametroById(int id, string idconta)
        {
            var ModeloParametro = await _ModeloParametroservice.ListarModeloParametroById(id, idconta);
            return Ok(ModeloParametro);
        }
    }
}