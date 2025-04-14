using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Validators.Moeda;
using ADUSAPICore.Models.Moeda;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Services
{
    public class CotacaoMoedaService
    {
        private readonly ADUSContext _context;
        private readonly CotacaoMoedaValidator _adicionarCotacaoMoedaValidator;
        private readonly ExcluirCotacaoMoedaValidator _excluirCotacaoMoedaValidator;

        public CotacaoMoedaService(ADUSContext context, CotacaoMoedaValidator adicionarCotacaoMoedaValidator, ExcluirCotacaoMoedaValidator excluirCotacaoMoedaValidator)
        {
            _context = context;
            _adicionarCotacaoMoedaValidator = adicionarCotacaoMoedaValidator;
            _excluirCotacaoMoedaValidator = excluirCotacaoMoedaValidator;
        }

        public async Task<CotacaoMoedaViewModel> AdicionarCotacaoMoeda(CotacaoMoedaViewModel dados)
        {
            _adicionarCotacaoMoedaValidator.ValidateAndThrow(dados);
            var CotacaoMoeda = new CotacaoMoeda();
            CotacaoMoeda.IdMoeda = dados.IdMoeda;
            CotacaoMoeda.CotacaoData = dados.CotacaoData;
            CotacaoMoeda.CotacaoValor = dados.CotacaoValor;

            await _context.AddAsync(CotacaoMoeda);
            await _context.SaveChangesAsync();
            return new CotacaoMoedaViewModel
            {
                IdMoeda = CotacaoMoeda.IdMoeda,
                CotacaoData = CotacaoMoeda.CotacaoData,
                CotacaoValor = CotacaoMoeda.CotacaoValor,

                Id = CotacaoMoeda.Id,
            };
        }

        public async Task<CotacaoMoedaViewModel>? SalvarCotacaoMoeda(int id, CotacaoMoedaViewModel dados)
        {
            var CotacaoMoeda = _context.cotacoesmoedas.Find(id);
            if (CotacaoMoeda != null)
            {
                CotacaoMoeda.IdMoeda = dados.IdMoeda;
                CotacaoMoeda.CotacaoData = dados.CotacaoData;
                CotacaoMoeda.CotacaoValor = dados.CotacaoValor;
                _context.Update(CotacaoMoeda);
                await _context.SaveChangesAsync();
                return new CotacaoMoedaViewModel
                {
                    IdMoeda = CotacaoMoeda.IdMoeda,
                    CotacaoData = CotacaoMoeda.CotacaoData,
                    CotacaoValor = CotacaoMoeda.CotacaoValor,

                    Id = CotacaoMoeda.Id,
                };
            }
            else return null;
        }

        public async Task<CotacaoMoedaViewModel>? ExcluirCotacaoMoeda(int id)
        {
            var CotacaoMoeda = _context.cotacoesmoedas.Find(id);
            if (CotacaoMoeda != null)
            {
                CotacaoMoedaViewModel dados = new CotacaoMoedaViewModel
                {
                    IdMoeda = CotacaoMoeda.Id,
                    CotacaoData = CotacaoMoeda.CotacaoData,
                    CotacaoValor = CotacaoMoeda.CotacaoValor,
                    Id = CotacaoMoeda.Id
                };

                _excluirCotacaoMoedaValidator.ValidateAndThrow(dados);
                _context.cotacoesmoedas.Remove(CotacaoMoeda);
                await _context.SaveChangesAsync();
                return new CotacaoMoedaViewModel
                {
                    IdMoeda = CotacaoMoeda.IdMoeda,
                    CotacaoData = CotacaoMoeda.CotacaoData,
                    CotacaoValor = CotacaoMoeda.CotacaoValor,

                    Id = CotacaoMoeda.Id,
                };
            }
            else return null;
        }

        public async Task<CotacaoMoedaViewModel>? ListarCotacaoMoedaById(int id)
        {
            var CotacaoMoeda = _context.cotacoesmoedas.Where(c => c.Id == id).Include(c => c.moeda).FirstOrDefault();
            if (CotacaoMoeda != null)
            {
                return new CotacaoMoedaViewModel
                {
                    IdMoeda = CotacaoMoeda.IdMoeda,
                    CotacaoData = CotacaoMoeda.CotacaoData,
                    CotacaoValor = CotacaoMoeda.CotacaoValor,
                    descmoeda = CotacaoMoeda.moeda.Descricao,
                    Id = CotacaoMoeda.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<CotacaoMoedaViewModel>> ListarCotacaoMoedaByMoeda(int idmoeda, DateTime? ini, DateTime? fim)
        {
            //var condicao = (CotacaoMoeda m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.cotacoesmoedas.AsQueryable();
            var CotacaoMoedas = query.Include(c => c.moeda).Where(c => c.IdMoeda == idmoeda && (c.CotacaoData >= ini && c.CotacaoData <= fim))
                .Select(c => new CotacaoMoedaViewModel
                {
                    IdMoeda = c.IdMoeda,
                    CotacaoData = c.CotacaoData,
                    CotacaoValor = c.CotacaoValor,
                    Id = c.Id,
                    descmoeda = c.moeda.Descricao
                }
                ).ToList();
            return (CotacaoMoedas);
        }
    }
}