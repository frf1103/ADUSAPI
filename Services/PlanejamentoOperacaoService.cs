using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.PlanejamentoOperacao;
using FarmPlannerAPICore.Models.MaquinaPlanejada;
using FarmPlannerAPICore.Models.PlanejamentoOperacao;
using FarmPlannerAPICore.Models.ProdutoPlanejado;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FarmPlannerAPI.Services
{
    public class PlanejamentoOperacaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly PlanejamentoOperacaoValidator _adicionarPlanejamentoOperacaoValidator;
        private readonly ExcluirPlanejamentoOperacaoValidator _excluirPlanejamentoOperacaoValidator;
        private readonly ProdutoPlanejadoService _produtoplan;
        private readonly MaquinaPlanejadaService _maquinaplan;

        public PlanejamentoOperacaoService(FarmPlannerContext context, PlanejamentoOperacaoValidator adicionarPlanejamentoOperacaoValidator, ExcluirPlanejamentoOperacaoValidator excluirPlanejamentoOperacaoValidator, ProdutoPlanejadoService produtoplan, MaquinaPlanejadaService maquinaplan)
        {
            _context = context;
            _adicionarPlanejamentoOperacaoValidator = adicionarPlanejamentoOperacaoValidator;
            _excluirPlanejamentoOperacaoValidator = excluirPlanejamentoOperacaoValidator;
            _produtoplan = produtoplan;
            _maquinaplan = maquinaplan;
        }

        public async Task<(PlanejamentoOperacaoViewModel, List<string> listerros)> AdicionarPlanejamentoOperacao(PlanejamentoOperacaoViewModel dados)
        {
            var validationErrors = new List<ValidationResult>();
            _adicionarPlanejamentoOperacaoValidator.ValidateAndThrow(dados);
            var errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
            if (errorMessages.Count == 0)
            {
                var PlanejamentoOperacao = new PlanejamentoOperacao();
                PlanejamentoOperacao.DAE = dados.DAE;
                PlanejamentoOperacao.Status = dados.Status;
                PlanejamentoOperacao.Area = dados.Area;
                PlanejamentoOperacao.CustoOperacao = dados.CustoOperacao;
                PlanejamentoOperacao.DataPrevista = dados.DataPrevista;
                PlanejamentoOperacao.IdConfigArea = dados.IdConfigArea;
                PlanejamentoOperacao.IdOperacao = dados.IdOperacao;
                // PlanejamentoOperacao.IdOrcamento = dados.IdOrcamento;
                PlanejamentoOperacao.QCombustivelEstimado = dados.QCombustivelEstimado;
                PlanejamentoOperacao.QHorasEstimadas = dados.QHorasEstimadas;
                PlanejamentoOperacao.IdOperacao = dados.IdOperacao;
                PlanejamentoOperacao.idconta = dados.idconta;
                PlanejamentoOperacao.uid = dados.uid;
                PlanejamentoOperacao.datains = DateTime.Now;
                PlanejamentoOperacao.Plantio = dados.Plantio;

                await _context.AddAsync(PlanejamentoOperacao);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Planejamento de operacoes " + PlanejamentoOperacao.Id.ToString() + "/" + PlanejamentoOperacao.IdOperacao.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return (new PlanejamentoOperacaoViewModel
                {
                    Id = PlanejamentoOperacao.Id,
                    DAE = PlanejamentoOperacao.DAE,
                    Status = PlanejamentoOperacao.Status,
                    Area = PlanejamentoOperacao.Area,
                    CustoOperacao = PlanejamentoOperacao.CustoOperacao,
                    DataPrevista = PlanejamentoOperacao.DataPrevista,
                    IdConfigArea = PlanejamentoOperacao.IdConfigArea,
                    IdOperacao = PlanejamentoOperacao.IdOperacao,
                    //                IdOrcamento=PlanejamentoOperacao.IdOrcamento,
                    Plantio = PlanejamentoOperacao.Plantio,
                    QCombustivelEstimado = PlanejamentoOperacao.QCombustivelEstimado,
                    QHorasEstimadas = PlanejamentoOperacao.QHorasEstimadas
                }, null);
            }
            else
            {
                errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
                return (null, errorMessages);
            }
        }

        public async Task<PlanejamentoOperacaoViewModel>? SalvarPlanejamentoOperacao(int id, string idconta, PlanejamentoOperacaoViewModel dados)
        {
            var PlanejamentoOperacao = _context.planejoperacoes.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (PlanejamentoOperacao != null)
            {
                PlanejamentoOperacao.DAE = dados.DAE;
                PlanejamentoOperacao.Status = dados.Status;
                PlanejamentoOperacao.Area = dados.Area;
                PlanejamentoOperacao.CustoOperacao = dados.CustoOperacao;
                PlanejamentoOperacao.DataPrevista = dados.DataPrevista;
                PlanejamentoOperacao.IdConfigArea = dados.IdConfigArea;
                PlanejamentoOperacao.IdOperacao = dados.IdOperacao;
                //              PlanejamentoOperacao.IdOrcamento = dados.IdOrcamento;
                PlanejamentoOperacao.QCombustivelEstimado = dados.QCombustivelEstimado;
                PlanejamentoOperacao.QHorasEstimadas = dados.QHorasEstimadas;
                PlanejamentoOperacao.IdOperacao = dados.IdOperacao;
                PlanejamentoOperacao.uid = dados.uid;
                PlanejamentoOperacao.dataup = DateTime.Now;

                _context.Update(PlanejamentoOperacao);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Planejamento de operacoes " + PlanejamentoOperacao.Id.ToString() + "/" + PlanejamentoOperacao.IdOperacao.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new PlanejamentoOperacaoViewModel
                {
                    Id = PlanejamentoOperacao.Id,
                    DAE = PlanejamentoOperacao.DAE,
                    Status = PlanejamentoOperacao.Status,
                    Area = PlanejamentoOperacao.Area,
                    CustoOperacao = PlanejamentoOperacao.CustoOperacao,
                    DataPrevista = PlanejamentoOperacao.DataPrevista,
                    IdConfigArea = PlanejamentoOperacao.IdConfigArea,
                    IdOperacao = PlanejamentoOperacao.IdOperacao,
                    //                IdOrcamento = PlanejamentoOperacao.IdOrcamento,
                    Plantio = PlanejamentoOperacao.Plantio,
                    QCombustivelEstimado = PlanejamentoOperacao.QCombustivelEstimado,
                    QHorasEstimadas = PlanejamentoOperacao.QHorasEstimadas
                };
            }
            else return null;
        }

        public async Task<PlanejamentoOperacaoViewModel>? ExcluirPlanejamentoOperacao(int id, string idconta, string uid)
        {
            var PlanejamentoOperacao = _context.planejoperacoes.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (PlanejamentoOperacao != null)
            {
                PlanejamentoOperacaoViewModel dados = new PlanejamentoOperacaoViewModel
                {
                    Id = PlanejamentoOperacao.Id
                };
                _excluirPlanejamentoOperacaoValidator.ValidateAndThrow(dados);
                _context.planejoperacoes.Remove(PlanejamentoOperacao);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Planejamento de operacoes " + PlanejamentoOperacao.Id.ToString() + "/" + PlanejamentoOperacao.IdOperacao.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new PlanejamentoOperacaoViewModel
                {
                    Id = PlanejamentoOperacao.Id,
                    DAE = PlanejamentoOperacao.DAE,
                    Status = PlanejamentoOperacao.Status,
                    Area = PlanejamentoOperacao.Area,
                    CustoOperacao = PlanejamentoOperacao.CustoOperacao,
                    DataPrevista = PlanejamentoOperacao.DataPrevista,
                    IdConfigArea = PlanejamentoOperacao.IdConfigArea,
                    IdOperacao = PlanejamentoOperacao.IdOperacao,
                    //              IdOrcamento = PlanejamentoOperacao.IdOrcamento,
                    Plantio = PlanejamentoOperacao.Plantio,
                    QCombustivelEstimado = PlanejamentoOperacao.QCombustivelEstimado,
                    QHorasEstimadas = PlanejamentoOperacao.QHorasEstimadas
                };
            }
            else return null;
        }

        public async Task<PlanejamentoOperacaoViewModel>? ListarPlanejamentoOperacaoById(int id, string idconta)
        {
            var PlanejamentoOperacao = _context.planejoperacoes.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (PlanejamentoOperacao != null)
            {
                return new PlanejamentoOperacaoViewModel
                {
                    Id = PlanejamentoOperacao.Id,
                    DAE = PlanejamentoOperacao.DAE,
                    Status = PlanejamentoOperacao.Status,
                    Area = PlanejamentoOperacao.Area,
                    CustoOperacao = PlanejamentoOperacao.CustoOperacao,
                    DataPrevista = PlanejamentoOperacao.DataPrevista,
                    IdConfigArea = PlanejamentoOperacao.IdConfigArea,
                    IdOperacao = PlanejamentoOperacao.IdOperacao,
                    //            IdOrcamento = PlanejamentoOperacao.IdOrcamento,
                    Plantio = PlanejamentoOperacao.Plantio,
                    QCombustivelEstimado = PlanejamentoOperacao.QCombustivelEstimado,
                    QHorasEstimadas = PlanejamentoOperacao.QHorasEstimadas
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListPlanejamentoOperacaoViewModel>> ListarPlanejamentoOperacao(int idorganizacao, int idsafra, int idano, int idfazenda, int idoperacao, int idtalhao, string idconta, int idvariedade, DateTime ini, DateTime fim)
        {
            //            var condicao = (PlanejamentoOperacao m) => (idfazenda == 0 || m.IdFazenda == idfazenda) &&
            //           (idsafra == 0 || m.IdSafra == idsafra) &&
            //          (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.planejoperacoes
                .Include(m => m.configArea).Include(m => m.operacao).Include(m => m.configArea.talhao).Include(m => m.configArea.talhao.fazenda)
                .Where(m => ((idtalhao == 0 || m.configArea.IdTalhao == idtalhao) && (m.idconta == idconta) &&
                (idfazenda == 0 || m.configArea.talhao.IdFazenda == idfazenda) &&
                (m.configArea.IdSafra == idsafra || idsafra == 0) && (m.IdOperacao == idoperacao || idoperacao == 0))
                && (idvariedade == 0 || m.configArea.IdVariedade == idvariedade) && m.DataPrevista >= ini && m.DataPrevista <= fim &&
                m.configArea.talhao.IdAnoAgricola == idano && m.configArea.talhao.fazenda.IdOrganizacao == idorganizacao
                );
            var PlanejamentoOperacaos = query
                .Select(c => new ListPlanejamentoOperacaoViewModel
                {
                    Id = c.Id,
                    DAE = c.DAE,
                    Status = c.Status,
                    Area = c.Area,
                    CustoOperacao = c.CustoOperacao,
                    DataPrevista = c.DataPrevista,
                    IdConfigArea = c.IdConfigArea,
                    IdOperacao = c.IdOperacao,
                    //  IdOrcamento = c.IdOrcamento,
                    Plantio = c.Plantio,
                    QCombustivelEstimado = c.QCombustivelEstimado,
                    QHorasEstimadas = c.QHorasEstimadas,
                    Descoperacao = c.operacao.Descricao,
                    Desctalhao = c.configArea.talhao.Descricao,
                    //   DescOrcamento=c.orcamentoProduto.Descricao,
                    DescSafra = c.configArea.safra.Descricao,
                    descconfig = c.configArea.talhao.fazenda.Descricao.Trim() + "/" + c.configArea.talhao.Descricao.Trim() + "/" + c.configArea.variedade.Descricao.Trim()
                }
                ).ToList();
            return (PlanejamentoOperacaos);
        }

        public async Task<(decimal rendimento, decimal consumo)> BuscaParametros(string idconta, int idmodelo, int idmaquina, int idconfigarea, int idcultura, int idoperacao)
        {
            var ca = _context.configareas.Where(x => x.idconta == idconta && x.Id == idconfigarea).FirstOrDefault();
            if (c != null)
            {
                if (idmaquina != 0)
                {
                    var maqp = _context.maquinasparametro.Where(m => m.idconta == idconta && m.IdMaquina == idmaquina && m.IdOperacao == idoperacao && m.IdCultura == idcultura).FirstOrDefault();
                    if (maqp != null)
                    {
                        return (maqp.Rendimento, maqp.Consumo);
                    }
                    else
                    {
                        var modp = _context.modelosparametros.Where(m => m.idconta == idconta && m.IdModeloMaquina == idmodelo && m.IdOperacao == idoperacao && m.IdCultura == idcultura).FirstOrDefault();
                        if (modp != null)
                        {
                            return (modp.Rendimento, modp.Consumo);
                        }
                        else
                        {
                            var op = _context.operacoes.Where(m => m.idconta == idconta && m.IdTipoOperacao == idoperacao).FirstOrDefault();
                            if (modp != null)
                            {
                                return (modp.Rendimento, modp.Consumo);
                            }
                        }
                    }
                }
            }
            return (0, 0);
        }

        public async Task<(bool success, List<string> erros)> AdicionarPlanejOperAreaAssistente(string idconta, string uid, List<AssistentePlanejOperViewModel> dados)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            foreach (var c in dados)
            {
                try
                {
                    decimal tothoras, totcomb;
                    tothoras = 0; totcomb = 0;
                    PlanejamentoOperacaoViewModel pl = new PlanejamentoOperacaoViewModel
                    {
                        Area = c.area,
                        DAE = c.dae,
                        IdConfigArea = c.idconfig,
                        Plantio = c.plantio,
                        IdOperacao = c.operacao,
                        idconta = idconta,
                        QCombustivelEstimado = 0,
                        Status = 0,
                        CustoOperacao = 0,
                        uid = uid,
                        QHorasEstimadas = 0,
                        DataPrevista = c.dataprevista
                    };
                    var (x, success) = await AdicionarPlanejamentoOperacao(pl);
                    if (success != null)
                    {
                        await transaction.RollbackAsync();
                        return (false, success);
                    }
                    foreach (var prod in c.produtos)
                    {
                        ProdutoPlanejadoViewModel pr = new ProdutoPlanejadoViewModel
                        {
                            idconta = idconta,
                            uid = uid,
                            IdPlanejamento = x.Id,
                            IdConfigArea = c.idconfig,
                            AreaPercent = prod.percent,
                            Dosagem = prod.dosagem,
                            IdPrincipioAtivo = (prod.idprincipio == 0) ? null : prod.idprincipio,
                            IdProduto = (prod.idproduto == 0) ? null : prod.idproduto,
                            Tamanho = prod.tamanho,
                            TotalProduto = prod.tamanho * prod.dosagem * prod.percent / 100
                        };
                        var (xp, successp) = await _produtoplan.AdicionarProdutoPlanejado(pr);
                        if (success != null)
                        {
                            await transaction.RollbackAsync();
                            return (false, success);
                        }
                    }

                    foreach (var maq in c.maquinas)
                    {
                        var (rend, cons) = await BuscaParametros(idconta, maq.idmodelo, maq.idmaquina, c.idconfig, 0, c.operacao);
                        decimal horas = (c.perc * c.area / 100) / rend;
                        decimal comb = horas * cons;
                        MaquinaPlanejadaViewModel mq = new MaquinaPlanejadaViewModel
                        {
                            idconta = idconta,
                            uid = uid,
                            IdPlanejamento = x.Id,
                            IdMaquina = maq.idmaquina,
                            IdModeloMaquina = maq.idmodelo,
                            Consumo = cons,
                            Rendimento = rend,
                            QtdHoraEstimada = horas,
                            QtdCombEstimado = comb
                        };
                        var (xm, successm) = await _maquinaplan.AdicionarMaquinaPlanejada(mq);
                        if (successm != null)
                        {
                            await transaction.RollbackAsync();
                            return (false, success);
                        }
                        tothoras = tothoras + horas;
                        totcomb = totcomb + comb;
                    }
                    pl.QCombustivelEstimado = totcomb;
                    pl.QHorasEstimadas = tothoras;
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
    }
}