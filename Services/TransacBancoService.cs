using ADUSAPI.Entities;
using ADUSAPICore.Models.Banco;
using ADUSAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Services
{
    public class TransacBancoService
    {
        private readonly ADUSContext _context;

        public TransacBancoService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<List<TransacBancoViewModel>> ObterTodosAsync()
        {
            return await _context.transacsbco
                .Include(tb => tb.banco)
                .Include(tb => tb.transacao)
                .Select(tb => new TransacBancoViewModel
                {
                    IdTransacBanco = tb.idtransacbanco,
                    IdBanco = tb.idbanco,
                    IdTransacao = tb.idtransacao,
                    NomeBanco = tb.banco.Descricao,
                    NomeTransacao = tb.transacao.Descricao,
                    idcategoria = tb.idcategoria,
                    idcentrocusto = tb.idcentrocusto
                }).ToListAsync();
        }

        public async Task<TransacBancoViewModel> ObterPorIdAsync(string idtbc, int idbanco)
        {
            var entity = await _context.transacsbco
                .Include(tb => tb.banco)
                .Where(tb => tb.idtransacbanco == idtbc && tb.idbanco == idbanco).FirstOrDefaultAsync();

            if (entity == null) return null;

            return new TransacBancoViewModel
            {
                IdTransacBanco = entity.idtransacbanco,
                IdBanco = entity.idbanco,
                IdTransacao = entity.idtransacao,
                idcategoria = entity.idcategoria,
                NomeBanco = entity.banco?.Descricao,
                NomeTransacao = entity.transacao?.Descricao,
                idcentrocusto = entity.idcentrocusto
            };
        }

        public async Task AdicionarAsync(TransacBancoViewModel model)
        {
            var entity = new TransacBanco
            {
                idtransacbanco = model.IdTransacBanco,
                idbanco = model.IdBanco,
                idtransacao = model.IdTransacao,
                idcategoria = model.idcategoria,
                idcentrocusto = model.idcentrocusto
            };

            _context.transacsbco.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(TransacBancoViewModel model)
        {
            var entity = await _context.transacsbco.Where(tb => tb.idtransacbanco == model.IdTransacBanco && tb.idbanco == model.IdBanco).FirstAsync();
            if (entity == null) return;

            entity.idbanco = model.IdBanco;
            entity.idtransacao = model.IdTransacao;
            entity.idtransacbanco = model.IdTransacBanco;
            entity.idcategoria = model.idcategoria;
            entity.idcentrocusto = model.idcentrocusto;
            _context.transacsbco.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(string idtbc, int idbanco)
        {
            var entity = await _context.transacsbco.Where(tb => tb.idtransacbanco == idtbc && tb.idbanco == idbanco).FirstAsync();
            if (entity != null)
            {
                _context.transacsbco.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}