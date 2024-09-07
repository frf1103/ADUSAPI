using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.AnoAgricola;
using FarmPlannerAPICore.Models.AnoAgricola;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class AnoAgricolaService
    {
        private readonly FarmPlannerContext _context;
        private readonly AdicionarAnoAgricolaValidator _adicionarAnoAgricolaValidator;
        private readonly ExcluirAnoAgricolaValidator _excluirAnoAgricolaValidator;

        public AnoAgricolaService(FarmPlannerContext context, AdicionarAnoAgricolaValidator adicionarAnoAgricolaValidator, ExcluirAnoAgricolaValidator excluirAnoAgricolaValidator)
        {
            _context = context;
            _adicionarAnoAgricolaValidator = adicionarAnoAgricolaValidator;
            _excluirAnoAgricolaValidator = excluirAnoAgricolaValidator;
        }

        public async Task<AdicionarAnoAgricolaViewModel> AdicionarAnoAgricola(AdicionarAnoAgricolaViewModel dados)
        {
            _adicionarAnoAgricolaValidator.ValidateAndThrow(dados);
            var AnoAgricola = new AnoAgricola();
            AnoAgricola.Descricao = dados.Descricao;
            AnoAgricola.DataInicio = (DateTime)dados.DataInicio;
            AnoAgricola.DataFim = (DateTime)dados.DataFim;
            AnoAgricola.CodigoExterno = dados.CodigoExterno;
            AnoAgricola.IdOrganizacao = (int)dados.IdOrganizacao;
            AnoAgricola.idconta = dados.idconta;
            await _context.AddAsync(AnoAgricola);
            await _context.SaveChangesAsync();
            return new AdicionarAnoAgricolaViewModel
            {
                Descricao = AnoAgricola.Descricao,
                DataInicio = AnoAgricola.DataInicio,
                DataFim = AnoAgricola.DataFim,
                CodigoExterno = AnoAgricola.CodigoExterno,
                Id = AnoAgricola.Id,
                IdOrganizacao = AnoAgricola.IdOrganizacao,
                idconta = AnoAgricola.idconta
            };
        }

        public async Task<AdicionarAnoAgricolaViewModel>? SalvarAnoAgricola(int id, string idconta, AdicionarAnoAgricolaViewModel dados)
        {
            var AnoAgricola = _context.anosagricolas.Where(a => a.Id == id && a.idconta == idconta).FirstOrDefault();
            if (AnoAgricola != null)
            {
                _adicionarAnoAgricolaValidator.ValidateAndThrow(dados);
                AnoAgricola.Descricao = dados.Descricao;
                AnoAgricola.DataInicio = (DateTime)dados.DataInicio;
                AnoAgricola.DataFim = (DateTime)dados.DataFim;
                AnoAgricola.CodigoExterno = dados.CodigoExterno;
                AnoAgricola.IdOrganizacao = (int)dados.IdOrganizacao;

                _context.Update(AnoAgricola);
                await _context.SaveChangesAsync();
                return new AdicionarAnoAgricolaViewModel
                {
                    Descricao = AnoAgricola.Descricao,
                    DataInicio = AnoAgricola.DataInicio,
                    DataFim = AnoAgricola.DataFim,
                    CodigoExterno = AnoAgricola.CodigoExterno,
                    Id = AnoAgricola.Id,
                    IdOrganizacao = AnoAgricola.IdOrganizacao,
                    idconta = AnoAgricola.idconta
                };
            }
            else return null;
        }

        public async Task<AdicionarAnoAgricolaViewModel>? ExcluirAnoAgricola(int id, string idconta)
        {
            var AnoAgricola = _context.anosagricolas.Where(a => a.Id == id && a.idconta == idconta).FirstOrDefault();
            if (AnoAgricola != null)
            {
                AdicionarAnoAgricolaViewModel dados = new AdicionarAnoAgricolaViewModel
                {
                    Id = AnoAgricola.Id
                };
                _excluirAnoAgricolaValidator.ValidateAndThrow(dados);
                _context.anosagricolas.Remove(AnoAgricola);
                await _context.SaveChangesAsync();
                return new AdicionarAnoAgricolaViewModel
                {
                    Descricao = dados.Descricao,
                    DataInicio = dados.DataInicio,
                    DataFim = dados.DataFim,
                    CodigoExterno = dados.CodigoExterno,
                    Id = dados.Id,
                    IdOrganizacao = dados.IdOrganizacao,
                    idconta = dados.idconta
                };
            }
            else return null;
        }

        public async Task<AdicionarAnoAgricolaViewModel>? ListarAnoAgricolaById(string idconta, int id)
        {
            var AnoAgricola = _context.anosagricolas.Where(a => a.Id == id && a.idconta == idconta).FirstOrDefault();
            if (AnoAgricola != null)
            {
                return new AdicionarAnoAgricolaViewModel
                {
                    Descricao = AnoAgricola.Descricao,
                    DataInicio = AnoAgricola.DataInicio,
                    DataFim = AnoAgricola.DataFim,
                    CodigoExterno = AnoAgricola.CodigoExterno,
                    Id = AnoAgricola.Id,
                    IdOrganizacao = AnoAgricola.IdOrganizacao
                };
            }
            else return null;
        }

        public async Task<IEnumerable<AdicionarAnoAgricolaViewModel>> ListarAnoAgricola(int idorganizacao, string idconta, string? filtro)
        {
            var condicao = (AnoAgricola m) => (m.IdOrganizacao == idorganizacao || idorganizacao == 0) && m.idconta == idconta && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.anosagricolas.Include(f => f.organizacao).AsQueryable();
            var AnoAgricolas = query.Where(condicao)
                .Select(c => new AdicionarAnoAgricolaViewModel
                {
                    Descricao = c.Descricao,
                    DataInicio = c.DataInicio,
                    DataFim = c.DataFim,
                    CodigoExterno = c.CodigoExterno,
                    Id = c.Id,
                    IdOrganizacao = c.IdOrganizacao,
                    descorganizacao = c.organizacao.Nome
                }
                ).ToList();
            return (AnoAgricolas);
        }
    }
}