using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;

using FarmPlannerAPI.Validators.CustosIndiretos;

using FarmPlannerAPICore.Models.CustosIndiretos;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class ClasseContaService
    {
        private readonly FarmPlannerContext _context;
        private readonly ClasseContaValidator _adicionarClasseContaValidator;
        private readonly ExcluirClasseContaValidator _excluirClasseContaValidator;

        public ClasseContaService(FarmPlannerContext context, ClasseContaValidator adicionarClasseContaValidator, ExcluirClasseContaValidator excluirClasseContaValidator)
        {
            _context = context;
            _adicionarClasseContaValidator = adicionarClasseContaValidator;
            _excluirClasseContaValidator = excluirClasseContaValidator;
        }

        public async Task<ClasseContaViewModel> AdicionarClasseConta(ClasseContaViewModel dados)
        {
            _adicionarClasseContaValidator.ValidateAndThrow(dados);
            var ClasseConta = new ClasseConta();
            ClasseConta.Descricao = dados.Descricao;
            ClasseConta.TipoClasseConta = (int)dados.TipoClasseConta;

            await _context.AddAsync(ClasseConta);
            await _context.SaveChangesAsync();
            return new ClasseContaViewModel
            {
                Descricao = ClasseConta.Descricao,

                Id = ClasseConta.Id,
                TipoClasseConta = ClasseConta.TipoClasseConta
            };
        }

        public async Task<ClasseContaViewModel>? SalvarClasseConta(int id, ClasseContaViewModel dados)
        {
            var ClasseConta = _context.classescontas.Find(id);
            if (ClasseConta != null)
            {
                ClasseConta.Descricao = dados.Descricao;
                ClasseConta.TipoClasseConta = dados.TipoClasseConta;

                _context.Update(ClasseConta);
                await _context.SaveChangesAsync();
                return new ClasseContaViewModel
                {
                    Descricao = ClasseConta.Descricao,
                    Id = ClasseConta.Id,
                    TipoClasseConta = ClasseConta.TipoClasseConta
                };
            }
            else return null;
        }

        public async Task<ClasseContaViewModel>? ExcluirClasseConta(int id)
        {
            var ClasseConta = _context.classescontas.Find(id);
            if (ClasseConta != null)
            {
                ClasseContaViewModel dados = new ClasseContaViewModel
                {
                    Id = ClasseConta.Id,
                    Descricao = ClasseConta.Descricao,
                    TipoClasseConta = ClasseConta.TipoClasseConta
                };
                _excluirClasseContaValidator.ValidateAndThrow(dados);
                _context.classescontas.Remove(ClasseConta);
                await _context.SaveChangesAsync();
                return new ClasseContaViewModel
                {
                    Descricao = ClasseConta.Descricao,
                    Id = ClasseConta.Id,
                    TipoClasseConta = ClasseConta.TipoClasseConta
                };
            }
            else return null;
        }

        public async Task<ClasseContaViewModel>? ListarClasseContaById(int id)
        {
            var ClasseConta = _context.classescontas.Find(id);
            if (ClasseConta != null)
            {
                return new ClasseContaViewModel
                {
                    Descricao = ClasseConta.Descricao,
                    Id = ClasseConta.Id,
                    TipoClasseConta = ClasseConta.TipoClasseConta
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListClasseContaViewModel>> ListarClasseConta(string? filtro)
        {
            var condicao = (ClasseConta m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.classescontas.AsQueryable();
            var ClasseContas = query.Where(condicao)
                .Select(c => new ListClasseContaViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    TipoClasseConta = c.TipoClasseConta,
                    DescTipo = c.TipoClasseConta.ToString()
                }
                ).ToList();
            return (ClasseContas);
        }
    }
}