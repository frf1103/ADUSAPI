using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Safra;
using FarmPlannerAPICore.Models.Localidades;
using FarmPlannerAPICore.Models.Safra;
using FluentValidation;
using System.Reflection;

namespace FarmPlannerAPI.Services
{
    public class SafraService
    {
        private readonly FarmPlannerContext _context;
        private readonly SafraValidator _adicionarSafraValidator;
        private readonly ExcluirSafraValidator _excluirSafraValidator;

        public SafraService(FarmPlannerContext context, SafraValidator adicionarSafraValidator, ExcluirSafraValidator excluirSafraValidator)
        {
            _context = context;
            _adicionarSafraValidator = adicionarSafraValidator;
            _excluirSafraValidator = excluirSafraValidator;
        }

        public async Task<SafraViewModel> AdicionarSafra(SafraViewModel dados)
        {
            _adicionarSafraValidator.ValidateAndThrow(dados);
            var Safra = new Safra();
            Safra.Descricao = dados.Descricao;
            Safra.DataInicio = dados.DataInicio;
            Safra.DataFim = dados.DataFim;
            Safra.CodigoExterno = dados.CodigoExterno;
            Safra.IdAnoAgricola = dados.IdAnoAgricola;
            Safra.Abertura = dados.Abertura;
            Safra.Reforma = dados.Reforma;
            Safra.IdCultura = dados.IdCultura;
            Safra.idconta = dados.idconta;
            await _context.AddAsync(Safra);
            await _context.SaveChangesAsync();
            return new SafraViewModel
            {
                Descricao = Safra.Descricao,
                DataInicio = Safra.DataInicio,
                DataFim = Safra.DataFim,
                CodigoExterno = Safra.CodigoExterno,
                Id = Safra.Id,
                IdAnoAgricola = Safra.IdAnoAgricola,
                IdCultura = Safra.IdCultura,
                Reforma = Safra.Reforma,
                Abertura = Safra.Abertura
            };
        }

        public async Task<SafraViewModel>? SalvarSafra(int id, string idconta, SafraViewModel dados)
        {
            var Safra = _context.safras.Find(id);
            if (Safra != null)
            {
                Safra.Descricao = dados.Descricao;
                Safra.DataInicio = dados.DataInicio;
                Safra.DataFim = dados.DataFim;
                Safra.CodigoExterno = dados.CodigoExterno;
                Safra.IdAnoAgricola = dados.IdAnoAgricola;
                Safra.Abertura = dados.Abertura;
                Safra.Reforma = dados.Reforma;
                Safra.IdCultura = dados.IdCultura;
                _context.Update(Safra);
                await _context.SaveChangesAsync();
                return new SafraViewModel
                {
                    Descricao = Safra.Descricao,
                    DataInicio = Safra.DataInicio,
                    DataFim = Safra.DataFim,
                    CodigoExterno = Safra.CodigoExterno,
                    Id = Safra.Id,
                    IdAnoAgricola = Safra.IdAnoAgricola,
                    IdCultura = Safra.IdCultura,
                    Reforma = Safra.Reforma,
                    Abertura = Safra.Abertura
                };
            }
            else return null;
        }

        public async Task<SafraViewModel>? ExcluirSafra(int id, string idconta)
        {
            var Safra = _context.safras.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (Safra != null)
            {
                SafraViewModel dados = new SafraViewModel
                {
                    Id = Safra.Id
                };

                _excluirSafraValidator.ValidateAndThrow(dados);

                _context.safras.Remove(Safra);
                await _context.SaveChangesAsync();
                return new SafraViewModel
                {
                    Descricao = Safra.Descricao,
                    DataInicio = Safra.DataInicio,
                    DataFim = Safra.DataFim,
                    CodigoExterno = Safra.CodigoExterno,
                    Id = Safra.Id,
                    IdAnoAgricola = Safra.IdAnoAgricola,
                    IdCultura = Safra.IdCultura,
                    Reforma = Safra.Reforma,
                    Abertura = Safra.Abertura
                };
            }
            else return null;
        }

        public async Task<SafraViewModel>? ListarSafraById(int id, string idconta)
        {
            var Safra = _context.safras.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (Safra != null)
            {
                return new SafraViewModel
                {
                    Descricao = Safra.Descricao,
                    DataInicio = Safra.DataInicio,
                    DataFim = Safra.DataFim,
                    CodigoExterno = Safra.CodigoExterno,
                    Id = Safra.Id,
                    IdAnoAgricola = Safra.IdAnoAgricola,
                    IdCultura = Safra.IdCultura,
                    Reforma = Safra.Reforma,
                    Abertura = Safra.Abertura
                };
            }
            else return null;
        }

        public async Task<IEnumerable<SafraViewModel>> ListarSafra(int idanoagricola, string idconta, int idcultura, string? filtro)
        {
            var condicao = (Safra m) => (m.idconta == idconta) && (idcultura == 0 || m.IdCultura == idcultura) && (m.IdAnoAgricola == idanoagricola) && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.safras.AsQueryable();
            var Safras = query.Where(condicao)
                .Select(c => new SafraViewModel
                {
                    Descricao = c.Descricao,
                    DataInicio = c.DataInicio,
                    DataFim = c.DataFim,
                    CodigoExterno = c.CodigoExterno,
                    Id = c.Id,
                    IdAnoAgricola = c.IdAnoAgricola,
                    IdCultura = c.IdCultura,
                    Reforma = c.Reforma,
                    Abertura = c.Abertura
                }
                ).ToList();
            return (Safras);
        }
    }
}