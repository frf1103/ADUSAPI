using ADUSAPI.Services;
using ADUSAPICore.Models.Checkout;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ADUSAPI.Controllers
{
    [Route("api/logcheckout")]
    [ApiController]
    [Authorize]
    public class LogCheckoutController : ControllerBase
    {
        private readonly LogCheckoutService _service;

        public LogCheckoutController(LogCheckoutService service)
        {
            _service = service;
        }

        [HttpGet("listar/{ini}/{fim}/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> Listar(DateTime ini, DateTime fim, string? filtro, int pageIndex, int pageSize)
        {
            var logs = await _service.ListarLogsAsync(ini, fim, filtro, pageIndex, pageSize);
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var log = await _service.GetById(id);
            if (log == null)
                return NotFound("Log não encontrado.");

            return Ok(log);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] LogCheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.Adicionar(model);
                return Ok("Log registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar log: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _service.Excluir(id);
                return Ok("Log excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir log: {ex.Message}");
            }
        }
    }
}