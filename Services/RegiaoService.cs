using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Localidade;
using FarmPlannerAPICore.Models.Localidades;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class RegiaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly RegiaoValidator _adicionarRegiaoValidator;
        private readonly ExcluirRegiaoValidator _excluirRegiaoValidator;

        public RegiaoService(FarmPlannerContext context, RegiaoValidator adicionarRegiaoValidator, ExcluirRegiaoValidator excluirRegiaoValidator)
        {
            _context = context;
            _adicionarRegiaoValidator = adicionarRegiaoValidator;
            _excluirRegiaoValidator = excluirRegiaoValidator;
        }

        public async Task<RegiaoViewModel> AdicionarRegiao(RegiaoViewModel dados)
        {
            _adicionarRegiaoValidator.ValidateAndThrow(dados);
            var conta = new Regiao();
            conta.Nome = dados.Nome;
            conta.Mascara = dados.Mascara;

            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new RegiaoViewModel
            {
                Nome = conta.Nome,
                Mascara = conta.Mascara,
                Id = conta.Id
            };
        }

        public async Task<RegiaoViewModel>? SalvarRegiao(int id, RegiaoViewModel dados)
        {
            var conta = _context.regioes.Find(id);
            if (conta != null)
            {
                conta.Mascara = dados.Mascara;
                conta.Nome = dados.Nome;
                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new RegiaoViewModel
                {
                    Nome = conta.Nome,
                    Mascara = conta.Mascara,
                    Id = conta.Id
                };
            }
            else return null;
        }

        public async Task<RegiaoViewModel>? ExcluirRegiao(int id)
        {
            var conta = _context.regioes.Find(id);
            if (conta != null)
            {
                RegiaoViewModel dados = new RegiaoViewModel
                {
                    Id = conta.Id,
                    Mascara = conta.Mascara,
                    Nome = conta.Nome
                };
                _excluirRegiaoValidator.ValidateAndThrow(dados);
                _context.regioes.Remove(conta);
                await _context.SaveChangesAsync();
                return new RegiaoViewModel
                {
                    Nome = dados.Nome,
                    Mascara = dados.Mascara,

                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<RegiaoViewModel>? ListarRegiaoById(int id)
        {
            var conta = _context.regioes.Find(id);
            if (conta != null)
            {
                return new RegiaoViewModel
                {
                    Nome = conta.Nome,
                    Mascara = conta.Mascara,
                    Id = conta.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<RegiaoViewModel>> ListarRegioes(string? filtro)
        {
            var condicao = (Regiao m) => (String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.regioes.AsQueryable();
            var registros = query.Where(condicao).OrderBy(c => c.Nome)
                .Select(c => new RegiaoViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Mascara = c.Mascara,
                }
                ).ToList();
            return (registros);
        }
    }
}