using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Variedade;
using FarmPlannerAPICore.Models.Variedade;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static FarmPlannerAPI.Validators.Variedade.VariedadeValidator;

namespace FarmPlannerAPI.Services
{
    public class VariedadeService
    {
        private readonly FarmPlannerContext _context;
        private readonly VariedadeValidator _adicionarVariedadeValidator;
        private readonly ExcluirVariedadeValidator _excluirVariedadeValidator;

        public VariedadeService(FarmPlannerContext context, VariedadeValidator adicionarVariedadeValidator, ExcluirVariedadeValidator excluirVariedadeValidator)
        {
            _context = context;
            _adicionarVariedadeValidator = adicionarVariedadeValidator;
            _excluirVariedadeValidator = excluirVariedadeValidator;
        }

        public async Task<VariedadeViewModel> AdicionarVariedade(VariedadeViewModel dados)
        {
            _adicionarVariedadeValidator.ValidateAndThrow(dados);
            var Variedade = new Variedade();
            Variedade.Descricao = dados.Descricao;
            Variedade.Ciclo = dados.Ciclo;
            Variedade.CodigoExterno = dados.CodigoExterno;
            Variedade.IdCultura = dados.IdCultura;
            Variedade.IdTecnologia = dados.IdTecnologia;

            await _context.AddAsync(Variedade);
            await _context.SaveChangesAsync();
            return new VariedadeViewModel
            {
                Descricao = Variedade.Descricao,
                IdCultura = Variedade.IdCultura,
                Ciclo = Variedade.Ciclo,
                IdTecnologia = Variedade.IdTecnologia,
                CodigoExterno = Variedade.CodigoExterno,
                Id = Variedade.Id,
                desccultura = dados.desccultura
            };
        }

        public async Task<VariedadeViewModel>? SalvarVariedade(int id, VariedadeViewModel dados)
        {
            var Variedade = _context.variedades.Find(id);
            if (Variedade != null)
            {
                Variedade.Descricao = dados.Descricao;

                Variedade.CodigoExterno = dados.CodigoExterno;
                Variedade.Ciclo = dados.Ciclo;
                Variedade.CodigoExterno = dados.CodigoExterno;
                Variedade.IdTecnologia = dados.IdTecnologia;
                _context.Update(Variedade);
                await _context.SaveChangesAsync();
                return new VariedadeViewModel
                {
                    Descricao = Variedade.Descricao,
                    IdCultura = Variedade.IdCultura,
                    Ciclo = Variedade.Ciclo,
                    IdTecnologia = Variedade.IdTecnologia,
                    CodigoExterno = Variedade.CodigoExterno,
                    Id = Variedade.Id
                };
            }
            else return null;
        }

        public async Task<VariedadeViewModel>? ExcluirVariedade(int id)
        {
            var Variedade = _context.variedades.Find(id);
            VariedadeViewModel dados = new VariedadeViewModel
            {
                Descricao = Variedade.Descricao,
                IdCultura = Variedade.IdCultura,
                Ciclo = Variedade.Ciclo,
                IdTecnologia = Variedade.IdTecnologia,
                CodigoExterno = Variedade.CodigoExterno,
                Id = Variedade.Id
            };

            _excluirVariedadeValidator.ValidateAndThrow(dados);
            //var Variedade = _context.variedades.Find(id);
            if (Variedade != null)
            {
                _context.variedades.Remove(Variedade);
                await _context.SaveChangesAsync();
                return new VariedadeViewModel
                {
                    Descricao = dados.Descricao,
                    IdCultura = dados.IdCultura,
                    Ciclo = dados.Ciclo,
                    IdTecnologia = dados.IdTecnologia,
                    CodigoExterno = dados.CodigoExterno,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<VariedadeViewModel>? ListarVariedadeById(int id)
        {
            var Variedade = _context.variedades.Include(v => v.cultura).Where(v => v.Id == id).FirstOrDefault();
            if (Variedade != null)
            {
                return new VariedadeViewModel
                {
                    Descricao = Variedade.Descricao,
                    IdCultura = Variedade.IdCultura,
                    Ciclo = Variedade.Ciclo,
                    IdTecnologia = Variedade.IdTecnologia,
                    CodigoExterno = Variedade.CodigoExterno,
                    Id = Variedade.Id,
                    desccultura = Variedade.cultura.Descricao
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListVariedadeViewModel>> ListarVariedade(int idcultura, string? filtro)
        {
            var Variedades = _context.variedades.Where(m => m.IdCultura == idcultura && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Include(c => c.tecnologia).Include(c => c.cultura)
                .Select(c => new ListVariedadeViewModel
                {
                    Descricao = c.Descricao,
                    IdCultura = c.IdCultura,
                    Ciclo = c.Ciclo,
                    IdTecnologia = c.IdTecnologia,
                    CodigoExterno = c.CodigoExterno,
                    Id = c.Id,
                    desccultura = c.cultura.Descricao,
                    desctecnologia = c.tecnologia.Descricao
                }
                ).ToList();
            return (Variedades);
        }
    }
}