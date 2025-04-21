using Microsoft.EntityFrameworkCore;
using ADUSAPI.Entities;
using ADUSAPICore.Models.Transacao;
using ADUSAPI.Context;

namespace ADUSAPI.Services
{
    public class TransacaoService
    {
        private readonly ADUSContext _context;

        public TransacaoService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<List<TransacaoViewModel>> ListarAsync(string? filtro = null)
        {
            var query = _context.transacoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
                query = query.Where(t => t.Descricao.Contains(filtro));

            return await query
                .OrderBy(t => t.Descricao)
                .Select(t => new TransacaoViewModel
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Sinal = t.Sinal,
                    Contrapartida = t.Contrapartida,
                    desccontra=t.Contrapartida.ToString()
                }).ToListAsync();
        }

        public async Task<TransacaoViewModel?> BuscarPorIdAsync(int id)
        {
            var t = await _context.transacoes.FindAsync(id);
            if (t == null) return null;

            return new TransacaoViewModel
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Sinal = t.Sinal,
                Contrapartida = t.Contrapartida
            };
        }

        public async Task<TransacaoViewModel> AdicionarAsync(TransacaoViewModel vm)
        {
            var model = new Transacao
            {
                Descricao = vm.Descricao,
                Sinal = vm.Sinal,
                Contrapartida = vm.Contrapartida
            };

            _context.transacoes.Add(model);
            await _context.SaveChangesAsync();

            vm.Id = model.Id;
            return vm;
        }

        public async Task<TransacaoViewModel?> AtualizarAsync(int id, TransacaoViewModel vm)
        {
            var existente = await _context.transacoes.FindAsync(id);
            if (existente == null) return null;

            existente.Descricao = vm.Descricao;
            existente.Sinal = vm.Sinal;
            existente.Contrapartida = vm.Contrapartida;

            _context.transacoes.Update(existente);
            await _context.SaveChangesAsync();

            return vm;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var model = await _context.transacoes.FindAsync(id);
            if (model == null) return false;

            _context.transacoes.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}