using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Cultura;
using FarmPlannerAPICore.Models.Cultura;
using FarmPlannerAPICore.Models.Variedade;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class CulturaService
    {
        private readonly FarmPlannerContext _context;
        private readonly CulturaValidator _adicionarCulturaValidator;
        private readonly ExcluirCulturaValidator _excluirCulturaValidator;

        public CulturaService(FarmPlannerContext context, CulturaValidator adicionarCulturaValidator, ExcluirCulturaValidator excluirCulturaValidator = null)
        {
            _context = context;
            _adicionarCulturaValidator = adicionarCulturaValidator;
            _excluirCulturaValidator = excluirCulturaValidator;
        }

        public async Task<CulturaViewModel> AdicionarCultura(CulturaViewModel dados)
        {
            _adicionarCulturaValidator.ValidateAndThrow(dados);
            var Cultura = new Cultura();
            Cultura.Descricao = dados.Descricao;
            Cultura.UnidadeProdutiva = dados.UnidadeProdutiva;
            Cultura.NomeProduto = dados.NomeProduto;
            Cultura.CodigoExterno = dados.CodigoExterno;
            Cultura.DiasEstimadosEmergencia = dados.DiasEstimadosEmergencia;
            await _context.AddAsync(Cultura);
            await _context.SaveChangesAsync();
            return new CulturaViewModel
            {
                Descricao = Cultura.Descricao,
                NomeProduto = Cultura.NomeProduto,
                UnidadeProdutiva = Cultura.UnidadeProdutiva,
                CodigoExterno = Cultura.CodigoExterno,
                DiasEstimadosEmergencia = Cultura.DiasEstimadosEmergencia,
                Id = Cultura.Id
            };
        }

        public async Task<CulturaViewModel>? SalvarCultura(int id, CulturaViewModel dados)
        {
            _adicionarCulturaValidator.ValidateAndThrow(dados);
            var Cultura = _context.culturas.Find(id);
            if (Cultura != null)
            {
                Cultura.Descricao = dados.Descricao;
                Cultura.UnidadeProdutiva = dados.UnidadeProdutiva;
                Cultura.NomeProduto = dados.NomeProduto;
                Cultura.CodigoExterno = dados.CodigoExterno;
                Cultura.DiasEstimadosEmergencia = dados.DiasEstimadosEmergencia;
                _context.Update(Cultura);
                await _context.SaveChangesAsync();
                return new CulturaViewModel
                {
                    UnidadeProdutiva = Cultura.UnidadeProdutiva,
                    NomeProduto = Cultura.NomeProduto,
                    Descricao = Cultura.Descricao,
                    CodigoExterno = Cultura.CodigoExterno,
                    Id = Cultura.Id,
                    DiasEstimadosEmergencia = Cultura.DiasEstimadosEmergencia
                };
            }
            else return null;
        }

        public async Task<CulturaViewModel>? ExcluirCultura(int id)
        {
            var Cultura = _context.culturas.Find(id);
            CulturaViewModel dados = new CulturaViewModel
            {
                UnidadeProdutiva = Cultura.UnidadeProdutiva,
                NomeProduto = Cultura.NomeProduto,
                Descricao = Cultura.Descricao,
                CodigoExterno = Cultura.CodigoExterno,
                Id = Cultura.Id,
                DiasEstimadosEmergencia = Cultura.DiasEstimadosEmergencia
            };

            _excluirCulturaValidator.ValidateAndThrow(dados);
            if (Cultura != null)
            {
                _context.culturas.Remove(Cultura);
                await _context.SaveChangesAsync();
                return new CulturaViewModel
                {
                    Descricao = dados.Descricao,
                    NomeProduto = dados.NomeProduto,
                    DiasEstimadosEmergencia = dados.DiasEstimadosEmergencia,
                    CodigoExterno = dados.CodigoExterno,
                    Id = dados.Id,
                    UnidadeProdutiva = dados.UnidadeProdutiva
                };
            }
            else return null;
        }

        public async Task<CulturaViewModel>? ListarCulturaById(int id)
        {
            var Cultura = _context.culturas.Include(c => c.Variedades)
                .ThenInclude(v => v.tecnologia).Where(c => c.Id == id).FirstOrDefault();

            if (Cultura != null)
            {
                return new CulturaViewModel
                {
                    Descricao = Cultura.Descricao,
                    UnidadeProdutiva = Cultura.UnidadeProdutiva,
                    CodigoExterno = Cultura.CodigoExterno,
                    Id = Cultura.Id,
                    NomeProduto = Cultura.NomeProduto,
                    DiasEstimadosEmergencia = Cultura.DiasEstimadosEmergencia,
                    listVariedade = Cultura.Variedades.Select(v => new ListVariedadeViewModel
                    {
                        Id = v.Id,
                        Descricao = v.Descricao,
                        desctecnologia = v.tecnologia.Descricao,
                        Ciclo = v.Ciclo,
                        CodigoExterno = v.CodigoExterno,
                        IdCultura = v.IdCultura,
                        IdTecnologia = v.IdTecnologia,
                        desccultura = v.cultura.Descricao
                    }).ToList()
                };
            }
            else return null;
        }

        public async Task<IEnumerable<CulturaViewModel>> ListarCultura(string? filtro)
        {
            var condicao = (Cultura m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.culturas.Include(c => c.Variedades)
                .ThenInclude(v => v.tecnologia);
            var Culturas = query.Where(condicao).ToList();
            var cview = Culturas.Select(c => new CulturaViewModel
            {
                Descricao = c.Descricao,
                UnidadeProdutiva = c.UnidadeProdutiva,
                CodigoExterno = c.CodigoExterno,
                DiasEstimadosEmergencia = c.DiasEstimadosEmergencia,
                NomeProduto = c.NomeProduto,
                Id = c.Id,
                listVariedade = c.Variedades.Select(v => new ListVariedadeViewModel
                {
                    Id = v.Id,
                    Descricao = v.Descricao,
                    desctecnologia = v.tecnologia.Descricao,
                    Ciclo = v.Ciclo,
                    CodigoExterno = v.CodigoExterno,
                    IdCultura = v.IdCultura,
                    IdTecnologia = v.IdTecnologia,
                    desccultura = v.cultura.Descricao
                }).ToList()
            }
                ).ToList();

            return (cview);
        }

        public async Task<IEnumerable<CulturaViewModel>> ListarCulturaVariedade(string? filtro)
        {
            var condicao = (Cultura m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper())
            || String.IsNullOrWhiteSpace(filtro) || m.Variedades.Any(v => v.Descricao.ToUpper().Contains(filtro.ToUpper())));
            var query = _context.culturas.Include(c => c.Variedades)
                .ThenInclude(v => v.tecnologia);
            var Culturas = query.Where(condicao).ToList();
            var cview = Culturas.Select(c => new CulturaViewModel
            {
                Descricao = c.Descricao,
                UnidadeProdutiva = c.UnidadeProdutiva,
                CodigoExterno = c.CodigoExterno,
                DiasEstimadosEmergencia = c.DiasEstimadosEmergencia,
                NomeProduto = c.NomeProduto,
                Id = c.Id,
                listVariedade = c.Variedades.Select(v => new ListVariedadeViewModel
                {
                    Id = v.Id,
                    Descricao = v.Descricao,
                    desctecnologia = v.tecnologia.Descricao,
                    Ciclo = v.Ciclo,
                    CodigoExterno = v.CodigoExterno,
                    IdCultura = v.IdCultura,
                    IdTecnologia = v.IdTecnologia,
                    desccultura = v.cultura.Descricao
                }).ToList()
            }
                ).ToList();

            return (cview);
        }
    }
}