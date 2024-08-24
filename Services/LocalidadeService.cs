using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;

using FarmPlannerAPI.Validators.Localidade;

using FarmPlannerAPICore.Models.Localidades;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class LocalidadeService
    {
        private readonly FarmPlannerContext _context;
        private readonly RegiaoValidator _adicionarRegiaoValidator;
        private readonly ExcluirRegiaoValidator _excluirRegiaoValidator;

        public LocalidadeService(FarmPlannerContext context, RegiaoValidator adicionarRegiaoValidator, ExcluirRegiaoValidator excluirRegiaoValidator)
        {
            _context = context;
            _adicionarRegiaoValidator = adicionarRegiaoValidator;
            _excluirRegiaoValidator = excluirRegiaoValidator;
        }





        public async Task<IEnumerable<UFViewModel>> ListarUF(string? filtro)
        {
            var condicao = (UF m) => (String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.ufs.AsQueryable();
            var registros = query.Where(condicao).OrderBy(c => c.Nome)
                .Select(c => new UFViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Sigla = c.Sigla,
                    CodigoIBGE = c.CodigoIBGE                    
                }
                ).ToList();
            return (registros);

        }


        public async Task<IEnumerable<MunicipioViewModel>> ListarMunicipioByUF(int iduf,string? filtro)
        {
            var condicao = (Municipio m) => m.IdUF==iduf && (String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.municipios.AsQueryable();
            var registros = query.Where(condicao).OrderBy(c => c.Nome)
                .Select(c => new MunicipioViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,                  
                    CodigoIBGE = c.CodigoIBGE

                }
                ).ToList();
            return (registros);

        }


    }
}