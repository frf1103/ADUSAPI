using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Localidade;
using FarmPlannerAPI.Validators.Unidade;
using FarmPlannerAPICore.Models.Unidade;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class UnidadeService
    {
        private readonly FarmPlannerContext _context;
        private readonly UnidadeValidator _adicionarUnidadeValidator;
        private readonly ExcluirUnidadeValidator _excluirUnidadeValidator;

        public UnidadeService(FarmPlannerContext context, UnidadeValidator adicionarUnidadeValidator, ExcluirUnidadeValidator excluirUnidadeValidator)
        {
            _context = context;
            _adicionarUnidadeValidator = adicionarUnidadeValidator;
            _excluirUnidadeValidator = excluirUnidadeValidator;
        }

        public async Task<UnidadeViewModel> AdicionarUnidade(UnidadeViewModel dados)
        {
            _adicionarUnidadeValidator.ValidateAndThrow(dados);
            var conta = new Unidade();
            conta.descricao = dados.descricao;
            conta.multiplo = dados.multiplo;

            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new UnidadeViewModel
            {
                descricao = conta.descricao,
                multiplo = conta.multiplo,
                id = conta.id
            };
        }

        public async Task<UnidadeViewModel>? SalvarUnidade(int id, UnidadeViewModel dados)
        {
            var conta = _context.unidades.Find(id);
            if (conta != null)
            {
                conta.descricao = dados.descricao;
                conta.multiplo = dados.multiplo;
                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new UnidadeViewModel
                {
                    descricao = conta.descricao,
                    multiplo = conta.multiplo,
                    id = conta.id
                };
            }
            else return null;
        }

        public async Task<UnidadeViewModel>? ExcluirUnidade(int id)
        {
            var conta = _context.unidades.Find(id);
            if (conta != null)
            {
                UnidadeViewModel dados = new UnidadeViewModel
                {
                    id = conta.id,
                    multiplo = conta.multiplo,
                    descricao = conta.descricao
                };
                _excluirUnidadeValidator.ValidateAndThrow(dados);
                _context.unidades.Remove(conta);
                await _context.SaveChangesAsync();
                return new UnidadeViewModel
                {
                    descricao = dados.descricao,
                    multiplo = dados.multiplo,

                    id = dados.id
                };
            }
            else return null;
        }

        public async Task<UnidadeViewModel>? ListarUnidadeById(int id)
        {
            var conta = _context.unidades.Find(id);
            if (conta != null)
            {
                return new UnidadeViewModel
                {
                    descricao = conta.descricao,
                    multiplo = conta.multiplo,
                    id = conta.id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<UnidadeViewModel>> Listar(string? filtro)
        {
            var condicao = (Unidade m) => (String.IsNullOrWhiteSpace(filtro) || m.descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.unidades.AsQueryable();
            var registros = query.Where(condicao).OrderBy(c => c.descricao)
                .Select(c => new UnidadeViewModel
                {
                    descricao = c.descricao,
                    multiplo = c.multiplo,
                    id = c.id
                }
                ).ToList();
            return (registros);
        }
    }
}