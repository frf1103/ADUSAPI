using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPICore.Models.CentroCusto;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Services
{
    public class CentroCustoService
    {
        private readonly ADUSContext _context;

        public CentroCustoService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<List<CentroCustoViewModel>> ListarAsync(string? filtro = null)
        {
            var query = _context.centroCustos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
                query = query.Where(cc => cc.Descricao.Contains(filtro));

            return await query
                .OrderBy(cc => cc.Descricao)
                .Select(cc => new CentroCustoViewModel
                {
                    Id = cc.Id,
                    Descricao = cc.Descricao
                }).ToListAsync();
        }

        public async Task<CentroCustoViewModel?> ObterPorIdAsync(int id)
        {
            var cc = await _context.centroCustos.FindAsync(id);
            if (cc == null) return null;

            return new CentroCustoViewModel
            {
                Id = cc.Id,
                Descricao = cc.Descricao
            };
        }

        public async Task AdicionarAsync(CentroCustoViewModel model)
        {
            var cc = new CentroCusto
            {
                Descricao = model.Descricao
            };

            _context.centroCustos.Add(cc);
            await _context.SaveChangesAsync();
            model.Id = cc.Id;
        }

        public async Task AtualizarAsync(int id, CentroCustoViewModel model)
        {
            var cc = await _context.centroCustos.FindAsync(id);
            if (cc == null) throw new Exception("Centro de Custo não encontrado.");

            cc.Descricao = model.Descricao;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var cc = await _context.centroCustos.FindAsync(id);
            if (cc != null)
            {
                _context.centroCustos.Remove(cc);
                await _context.SaveChangesAsync();
            }
        }
    }
}