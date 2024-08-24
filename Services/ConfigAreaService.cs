using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.ConfigArea;
using FarmPlannerAPICore.Models.ConfigArea;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json;

namespace FarmPlannerAPI.Services
{
    public class ConfigAreaService
    {
        private readonly FarmPlannerContext _context;
        private readonly AdicionarConfigAreaValidator _adicionarConfigAreaValidator;
        private readonly ExcluirConfigAreaValidator _excluirConfigAreaValidator;
        private readonly EditarConfigAreaValidator _editarConfigAreaValidator;

        public ConfigAreaService(FarmPlannerContext context, AdicionarConfigAreaValidator adicionarConfigAreaValidator, ExcluirConfigAreaValidator excluirConfigAreaValidator, EditarConfigAreaValidator editarConfigAreaValidator)
        {
            _context = context;
            _adicionarConfigAreaValidator = adicionarConfigAreaValidator;
            _excluirConfigAreaValidator = excluirConfigAreaValidator;
            _editarConfigAreaValidator = editarConfigAreaValidator;
        }

        public async Task<(ConfigAreaViewModel, List<string> listerros)> AdicionarConfigArea(ConfigAreaViewModel dados)
        {
            var validationErrors = new List<ValidationResult>();

            _adicionarConfigAreaValidator.ValidateAndThrow(dados);
            var errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
            if (errorMessages.Count == 0)
            {
                var ConfigArea = new ConfigArea();
                ConfigArea.IdSafra = dados.IdSafra;
                ConfigArea.Area = dados.Area;
                ConfigArea.IdVariedade = dados.IdVariedade;
                ConfigArea.IdTalhao = dados.IdTalhao;
                ConfigArea.PopulacaoRecomendada = dados.PopulacaoRecomendada;
                ConfigArea.ProdEstimada = dados.ProdEstimada;
                ConfigArea.Espacamento = dados.Espacamento;
                ConfigArea.Germinacao = dados.Germinacao;
                ConfigArea.MargemSeguranca = dados.MargemSeguranca;
                ConfigArea.PMS = dados.PMS;
                ConfigArea.QtdSementePrevista = dados.QtdSementePrevista;
                ConfigArea.UnidadeSementePrevista = dados.UnidadeSementePrevista;
                ConfigArea.idconta = dados.idconta;
                ConfigArea.Stand = dados.Stand;
                ConfigArea.uid = dados.uid;
                ConfigArea.datains = DateTime.Now;

                await _context.AddAsync(ConfigArea);
                await _context.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Talhao/Variedade/area " + ConfigArea.IdTalhao.ToString() + "/" + ConfigArea.IdVariedade.ToString() + "/" + ConfigArea.Area.ToString(), datalog = DateTime.Now });
                await _context.SaveChangesAsync();
                return (new ConfigAreaViewModel
                {
                    Id = ConfigArea.Id,
                    IdSafra = ConfigArea.IdSafra,
                    IdTalhao = ConfigArea.IdTalhao,
                    IdVariedade = ConfigArea.IdVariedade,
                    Area = dados.Area,
                    Espacamento = ConfigArea.Espacamento,
                    Germinacao = ConfigArea.Germinacao,
                    MargemSeguranca = ConfigArea.MargemSeguranca,
                    PMS = ConfigArea.PMS,
                    ProdEstimada = ConfigArea.ProdEstimada,
                    QtdSementePrevista = ConfigArea.QtdSementePrevista,
                    Stand = ConfigArea.Stand,
                    UnidadeSementePrevista = ConfigArea.UnidadeSementePrevista,
                    PopulacaoRecomendada = ConfigArea.PopulacaoRecomendada
                }, null);
            }
            else
            {
                errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
                return (null, errorMessages);
            }
        }

        public async Task<(bool success, List<string> erros)> AdicionarConfigAreaAssistente(List<ConfigAreaViewModel> dados)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var validationErrors = new List<ValidationResult>();
            foreach (var c in dados)
            {
                try
                {
                    var (x, success) = await AdicionarConfigArea(c);
                    if (success != null)
                    {
                        await transaction.RollbackAsync();
                        return (false, success);
                    }
                }
                catch (Exception ex)
                {
                    // Revertendo a transação em caso de erro
                    await transaction.RollbackAsync();
                    List<string> errorMessages = new List<string> { ex.Message.ToString() };
                    return (false, errorMessages);
                }
            }
            await transaction.CommitAsync();
            return (true, null);
        }

        public async Task<ConfigAreaViewModel>? SalvarConfigArea(int id, string idconta, ConfigAreaViewModel dados)
        {
            _editarConfigAreaValidator.ValidateAndThrow(dados);
            var ConfigArea = _context.configareas.Where(c => c.Id == id && c.idconta == idconta).FirstOrDefault();
            if (ConfigArea != null)
            {
                ConfigArea.IdSafra = dados.IdSafra;
                ConfigArea.Area = dados.Area;
                ConfigArea.IdVariedade = dados.IdVariedade;
                ConfigArea.IdTalhao = dados.IdTalhao;
                ConfigArea.PopulacaoRecomendada = dados.PopulacaoRecomendada;
                ConfigArea.ProdEstimada = dados.ProdEstimada;
                ConfigArea.Espacamento = dados.Espacamento;
                ConfigArea.Germinacao = dados.Germinacao;
                ConfigArea.MargemSeguranca = dados.MargemSeguranca;
                ConfigArea.PMS = dados.PMS;
                ConfigArea.QtdSementePrevista = dados.QtdSementePrevista;
                ConfigArea.UnidadeSementePrevista = dados.UnidadeSementePrevista;
                ConfigArea.Stand = dados.Stand;
                ConfigArea.dataup = DateTime.Now;
                ConfigArea.uid = dados.uid;

                _context.Update(ConfigArea);
                await _context.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Talhao/Variedade/area " + ConfigArea.IdTalhao.ToString() + "/" + ConfigArea.IdVariedade.ToString() + "/" + ConfigArea.Area.ToString(), datalog = DateTime.Now });
                await _context.SaveChangesAsync();
                return new ConfigAreaViewModel
                {
                    Id = ConfigArea.Id,
                    IdSafra = ConfigArea.IdSafra,
                    IdTalhao = ConfigArea.IdTalhao,
                    IdVariedade = ConfigArea.IdVariedade,
                    Area = ConfigArea.Area,
                    Espacamento = ConfigArea.Espacamento,
                    Germinacao = ConfigArea.Germinacao,
                    MargemSeguranca = ConfigArea.MargemSeguranca,
                    PMS = ConfigArea.PMS,
                    ProdEstimada = ConfigArea.ProdEstimada,
                    QtdSementePrevista = ConfigArea.QtdSementePrevista,
                    Stand = ConfigArea.Stand,
                    UnidadeSementePrevista = ConfigArea.UnidadeSementePrevista,
                    PopulacaoRecomendada = ConfigArea.PopulacaoRecomendada
                };
            }
            else return null;
        }

        public async Task<ConfigAreaViewModel>? ExcluirConfigArea(int id, string idconta, string uid)
        {
            var ConfigArea = _context.configareas.Where(c => c.Id == id && c.idconta == idconta).FirstOrDefault();
            if (ConfigArea != null)
            {
                ConfigAreaViewModel dados = new ConfigAreaViewModel
                {
                    Id = ConfigArea.Id
                };
                _excluirConfigAreaValidator.ValidateAndThrow(dados);
                _context.configareas.Remove(ConfigArea);
                await _context.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Talhao/Variedade/area " + ConfigArea.IdTalhao.ToString() + "/" + ConfigArea.IdVariedade.ToString() + "/" + ConfigArea.Area.ToString(), datalog = DateTime.Now });
                await _context.SaveChangesAsync();
                return new ConfigAreaViewModel
                {
                    Id = ConfigArea.Id,
                    IdSafra = ConfigArea.IdSafra,
                    IdTalhao = ConfigArea.IdTalhao,
                    IdVariedade = ConfigArea.IdVariedade,
                    Area = ConfigArea.Area,
                    Espacamento = ConfigArea.Espacamento,
                    Germinacao = ConfigArea.Germinacao,
                    MargemSeguranca = ConfigArea.MargemSeguranca,
                    PMS = ConfigArea.PMS,
                    ProdEstimada = ConfigArea.ProdEstimada,
                    QtdSementePrevista = ConfigArea.QtdSementePrevista,
                    Stand = ConfigArea.Stand,
                    UnidadeSementePrevista = ConfigArea.UnidadeSementePrevista,
                    PopulacaoRecomendada = ConfigArea.PopulacaoRecomendada
                };
            }
            else return null;
        }

        public async Task<ConfigAreaViewModel>? ListarConfigAreaById(int id, string idconta)
        {
            var ConfigArea = _context.configareas.Include(c => c.talhao)
                .Where(c => c.Id == id && c.idconta == idconta).FirstOrDefault();
            if (ConfigArea != null)
            {
                return new ConfigAreaViewModel
                {
                    Id = ConfigArea.Id,
                    IdSafra = ConfigArea.IdSafra,
                    IdTalhao = ConfigArea.IdTalhao,
                    IdVariedade = ConfigArea.IdVariedade,
                    Area = ConfigArea.Area,
                    Espacamento = ConfigArea.Espacamento,
                    Germinacao = ConfigArea.Germinacao,
                    MargemSeguranca = ConfigArea.MargemSeguranca,
                    PMS = ConfigArea.PMS,
                    ProdEstimada = ConfigArea.ProdEstimada,
                    QtdSementePrevista = ConfigArea.QtdSementePrevista,
                    Stand = ConfigArea.Stand,
                    UnidadeSementePrevista = ConfigArea.UnidadeSementePrevista,
                    PopulacaoRecomendada = ConfigArea.PopulacaoRecomendada,
                    idfazenda = ConfigArea.talhao.IdFazenda
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListConfigAreaViewModel>> ListarConfigArea(string idconta, int idano, int idfazenda, int idtalhao, int idvariedade, int idsafra, int idorganizacao)
        {
            var ConfigAreas = _context.configareas.Where(m => (idtalhao == 0 || m.IdTalhao == idtalhao) && (idsafra == 0 || m.IdSafra == idsafra)
                                                         && (idvariedade == 0 || m.IdVariedade == idvariedade))
                .Include(c => c.variedade).Include(c => c.safra)
                .Include(c => c.talhao)
                .Include(c => c.talhao.fazenda)
                .Where(c => c.talhao.IdAnoAgricola == idano && (idfazenda == 0 || c.talhao.IdFazenda == idfazenda) && c.talhao.fazenda.IdOrganizacao == idorganizacao)
                .Select(c => new ListConfigAreaViewModel
                {
                    Area = c.Area,
                    descsafra = c.safra.Descricao,
                    desctalhao = c.talhao.Descricao,
                    descvariedade = c.variedade.Descricao,
                    Id = c.Id,
                    IdSafra = c.IdSafra,
                    IdTalhao = c.IdTalhao,
                    IdVariedade = c.IdVariedade,
                    descfazenda = c.talhao.fazenda.Descricao,
                    descconfig = c.safra.Descricao.Trim() + "/" + c.talhao.fazenda.Descricao.Trim() + "/" + c.talhao.Descricao.Trim() + "/" + c.variedade.Descricao.Trim(),
                    PopulacaoRecomendada = c.PopulacaoRecomendada??0,
                    Espacamento = c.Espacamento??0,
                    PMS = c.PMS??0,
                    MargemSeguranca = c.MargemSeguranca??0,
                    Germinacao = c.Germinacao??0
                }
                ).ToList();
            return (ConfigAreas);
        }
    }
}