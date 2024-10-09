using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.TipoOperacao;
using FarmPlannerAPICore.Models.TipoOperacao;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class TipoOperacaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly TipoOperacaoValidator _adicionarTipoOperacaoValidator;
        private readonly ExcluirTipoOperacaoValidator _excluirTipoOperacaoValidator;

        public TipoOperacaoService(FarmPlannerContext context, TipoOperacaoValidator adicionarTipoOperacaoValidator, ExcluirTipoOperacaoValidator excluirTipoOperacaoValidator)
        {
            _context = context;
            _adicionarTipoOperacaoValidator = adicionarTipoOperacaoValidator;
            _excluirTipoOperacaoValidator = excluirTipoOperacaoValidator;
        }

        public async Task<TipoOperacaoViewModel> AdicionarTipoOperacao(TipoOperacaoViewModel dados)
        {
            _adicionarTipoOperacaoValidator.ValidateAndThrow(dados);
            var TipoOperacao = new TipoOperacao();
            TipoOperacao.Descricao = dados.Descricao;
            TipoOperacao.plantio = dados.plantio;
            await _context.AddAsync(TipoOperacao);
            await _context.SaveChangesAsync();
            return new TipoOperacaoViewModel
            {
                Descricao = TipoOperacao.Descricao,
                plantio = TipoOperacao.plantio,
                Id = TipoOperacao.Id,
            };
        }

        public async Task<TipoOperacaoViewModel>? SalvarTipoOperacao(int id, TipoOperacaoViewModel dados)
        {
            var TipoOperacao = _context.tiposoperacao.Find(id);
            if (TipoOperacao != null)
            {
                TipoOperacao.Descricao = dados.Descricao;
                TipoOperacao.plantio = dados.plantio;
                _context.Update(TipoOperacao);
                await _context.SaveChangesAsync();
                return new TipoOperacaoViewModel
                {
                    Descricao = TipoOperacao.Descricao,
                    plantio = TipoOperacao.plantio,
                    Id = TipoOperacao.Id
                };
            }
            else return null;
        }

        public async Task<TipoOperacaoViewModel>? ExcluirTipoOperacao(int id)
        {
            var TipoOperacao = _context.tiposoperacao.Find(id);
            if (TipoOperacao != null)
            {
                TipoOperacaoViewModel dados = new TipoOperacaoViewModel
                {
                    Id = TipoOperacao.Id,
                    plantio = TipoOperacao.plantio,
                    Descricao = TipoOperacao.Descricao
                };
                _excluirTipoOperacaoValidator.ValidateAndThrow(dados);
                _context.tiposoperacao.Remove(TipoOperacao);
                await _context.SaveChangesAsync();
                return new TipoOperacaoViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<TipoOperacaoViewModel>? ListarTipoOperacaoById(int id)
        {
            var TipoOperacao = _context.tiposoperacao.Find(id);
            if (TipoOperacao != null)
            {
                return new TipoOperacaoViewModel
                {
                    Descricao = TipoOperacao.Descricao,
                    plantio = TipoOperacao.plantio,
                    Id = TipoOperacao.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<TipoOperacaoViewModel>> ListarTipoOperacao(string? filtro)
        {
            var condicao = (TipoOperacao m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.tiposoperacao.AsQueryable();
            var TipoOperacaos = query.Where(condicao)
                .Select(c => new TipoOperacaoViewModel
                {
                    Id = c.Id,
                    plantio = c.plantio,
                    Descricao = c.Descricao
                }
                ).ToList();
            return (TipoOperacaos);
        }
    }
}