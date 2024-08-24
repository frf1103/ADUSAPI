using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.PlanejamentoOperacao;
using FarmPlannerAPICore.Models.PlanejamentoOperacao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class PlanejamentoOperacaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly PlanejamentoOperacaoValidator _adicionarPlanejamentoOperacaoValidator;
        private readonly ExcluirPlanejamentoOperacaoValidator _excluirPlanejamentoOperacaoValidator;

        public PlanejamentoOperacaoService(FarmPlannerContext context, PlanejamentoOperacaoValidator adicionarPlanejamentoOperacaoValidator, ExcluirPlanejamentoOperacaoValidator excluirPlanejamentoOperacaoValidator)
        {
            _context = context;
            _adicionarPlanejamentoOperacaoValidator = adicionarPlanejamentoOperacaoValidator;
            _excluirPlanejamentoOperacaoValidator = excluirPlanejamentoOperacaoValidator;
        }

        public async Task<PlanejamentoOperacaoViewModel> AdicionarPlanejamentoOperacao(PlanejamentoOperacaoViewModel dados)
        {
            _adicionarPlanejamentoOperacaoValidator.ValidateAndThrow(dados);
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

            await _context.AddAsync(PlanejamentoOperacao);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Planejamento de operacoes " + PlanejamentoOperacao.Id.ToString() + "/" + PlanejamentoOperacao.IdOperacao.ToString(), datalog = DateTime.Now });
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
                //                IdOrcamento=PlanejamentoOperacao.IdOrcamento,
                Plantio = PlanejamentoOperacao.Plantio,
                QCombustivelEstimado = PlanejamentoOperacao.QCombustivelEstimado,
                QHorasEstimadas = PlanejamentoOperacao.QHorasEstimadas
            };
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
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Planejamento de operacoes " + PlanejamentoOperacao.Id.ToString() + "/" + PlanejamentoOperacao.IdOperacao.ToString(), datalog = DateTime.Now });
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
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Planejamento de operacoes " + PlanejamentoOperacao.Id.ToString() + "/" + PlanejamentoOperacao.IdOperacao.ToString(), datalog = DateTime.Now });
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

        public async Task<IEnumerable<ListPlanejamentoOperacaoViewModel>> ListarPlanejamentoOperacao(int idorganizacao,int idsafra, int idano, int idfazenda, int idoperacao, int idtalhao, string idconta, int idvariedade, DateTime ini, DateTime fim)
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
                m.configArea.talhao.IdAnoAgricola == idano && m.configArea.talhao.fazenda.IdOrganizacao==idorganizacao
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
    }
}