using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Tecnologia;
using FarmPlannerAPICore.Models.Tecnologia;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class TecnologiaService
    {
        private readonly FarmPlannerContext _context;
        private readonly TecnologiaValidator _adicionarTecnologiaValidator;
        private readonly ExcluirTecnologiaValidator _excluirTecnologiaValidator;

        public TecnologiaService(FarmPlannerContext context, TecnologiaValidator adicionarTecnologiaValidator, ExcluirTecnologiaValidator excluirTecnologiaValidator)
        {
            _context = context;
            _adicionarTecnologiaValidator = adicionarTecnologiaValidator;
            _excluirTecnologiaValidator = excluirTecnologiaValidator;
        }

        public async Task<TecnologiaViewModel> AdicionarTecnologia(TecnologiaViewModel dados)
        {
            _adicionarTecnologiaValidator.ValidateAndThrow(dados);
            var Tecnologia = new Tecnologia();
            Tecnologia.Descricao = dados.Descricao;
            await _context.AddAsync(Tecnologia);
            await _context.SaveChangesAsync();
            return new TecnologiaViewModel
            {
                Descricao = Tecnologia.Descricao,
                Id = Tecnologia.Id
            };
        }

        public async Task<TecnologiaViewModel>? SalvarTecnologia(int id, TecnologiaViewModel dados)
        {
            var Tecnologia = _context.tecnologias.Find(id);
            if (Tecnologia != null)
            {
                Tecnologia.Descricao = dados.Descricao;
                _context.Update(Tecnologia);
                await _context.SaveChangesAsync();
                return new TecnologiaViewModel
                {
                    Descricao = Tecnologia.Descricao,
                    Id = Tecnologia.Id
                };
            }
            else return null;
        }

        public async Task<TecnologiaViewModel>? ExcluirTecnologia(int id)
        {
            var Tecnologia = _context.tecnologias.Find(id);
            TecnologiaViewModel dados = new TecnologiaViewModel
            {
                Descricao = Tecnologia.Descricao,
                Id = Tecnologia.Id
            };

            if (Tecnologia != null)
            {
                _excluirTecnologiaValidator.ValidateAndThrow(dados);
                _context.tecnologias.Remove(Tecnologia);
                await _context.SaveChangesAsync();
                return new TecnologiaViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<TecnologiaViewModel>? ListarTecnologiaById(int id)
        {
            var Tecnologia = _context.tecnologias.Find(id);
            if (Tecnologia != null)
            {
                return new TecnologiaViewModel
                {
                    Descricao = Tecnologia.Descricao,
                    Id = Tecnologia.Id,
                };
            }
            else return null;
        }

        public async Task<IEnumerable<TecnologiaViewModel>> ListarTecnologia(string? filtro)
        {
            var condicao = (Tecnologia m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.tecnologias.AsQueryable();
            var Tecnologias = query.Where(condicao)
                .Select(c => new TecnologiaViewModel
                {
                    Descricao = c.Descricao,
                    Id = c.Id
                }
                ).ToList();
            return (Tecnologias);
        }
    }
}