namespace ADUSAPI.Services
{
    using ADUSAPI.Context;
    using ADUSAPI.Entities;
    using ADUSAPICore.Models.PlanoConta;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PlanoContaService
    {
        private readonly ADUSContext _context;

        public PlanoContaService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<List<PlanoConta>> ListarAsync(string? filtro)
        {
            return await _context.PlanoContas.Where(b => string.IsNullOrWhiteSpace(filtro) || b.Descricao.ToUpper().Contains(filtro.ToUpper()))
                .ToListAsync();
        }

        public async Task<PlanoConta> BuscarPorIdAsync(int id)
        {
            return await _context.PlanoContas
                .Include(p => p.Mae)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AdicionarAsync(PlanoContaViewModel plano)
        {
            await _context.PlanoContas.AddAsync(new PlanoConta
            {
                Descricao = plano.Descricao,
                Id = plano.Id,
                Sinal = plano.Sinal,
                IdMae = plano.IdMae
            });
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(PlanoContaViewModel plano)
        {
            PlanoConta plc=_context.PlanoContas.Find(plano.Id);
            plc.Sinal = plano.Sinal;
            plc.Descricao = plano.Descricao;
            _context.PlanoContas.Update(plc);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var plano = await BuscarPorIdAsync(id);
            if (plano != null)
            {
                _context.PlanoContas.Remove(plano);
                await _context.SaveChangesAsync();
            }
        }
    }
}