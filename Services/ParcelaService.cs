using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPICore.Models.Enum;
using ADUSAPICore.Models.Parcela;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ADUSAPI.Services
{
    public class ParcelaService
    {
        private readonly ADUSContext _context;
        private readonly ILogger<ParcelaService> _logger;

        public ParcelaService(ADUSContext context, ILogger<ParcelaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ParcelaViewModel> AdicionarParcela(ParcelaViewModel dados)
        {
            var parcela = new Parcela
            {
                id = dados.id,
                idcheckout = dados.idcheckout,
                idcaixa = dados.idcaixa,
                idformapagto = dados.idformapagto,
                comissao = dados.comissao,
                databaixa = dados.databaixa,
                datavencimento = dados.datavencimento,
                descontoantecipacao = dados.descontoantecipacao,
                descontoplataforma = dados.descontoplataforma,
                idassinatura = dados.idassinatura,
                nossonumero = dados.nossonumero,
                numparcela = dados.numparcela,
                plataforma = dados.plataforma,
                valorliquido = dados.valorliquido,
                observacao = dados.observacao,
                acrescimos = dados.acrescimos,
                descontos = dados.descontos,
                valor = dados.valor,
                datains = DateTime.Now
            };

            try
            {
                await _context.AddAsync(parcela);
                await _context.SaveChangesAsync();
                return MapToViewModel(parcela);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro ao adicionar parcela. Verifique as FKs: idassinatura={idassinatura}, idcaixa={idcaixa}",
                    parcela.idassinatura, parcela.idcaixa);

                throw new Exception("Erro ao salvar parcela. Verifique as dependências relacionadas (FKs).");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao adicionar parcela.");
                throw;
            }
        }

        public async Task<ParcelaViewModel?> SalvarParcela(string id, ParcelaViewModel dados)
        {
            var parcela = await _context.parcelas.FirstOrDefaultAsync(p => p.id == id);
            if (parcela == null)
            {
                _logger.LogWarning("Parcela com ID {id} não encontrada para edição.", id);
                return null;
            }

            try
            {
                parcela.idcheckout = dados.idcheckout;
                parcela.idcaixa = dados.idcaixa;
                parcela.idformapagto = dados.idformapagto;
                parcela.comissao = dados.comissao;
                parcela.databaixa = dados.databaixa;
                parcela.datavencimento = dados.datavencimento;
                parcela.descontoantecipacao = dados.descontoantecipacao;
                parcela.descontoplataforma = dados.descontoplataforma;
                parcela.idassinatura = dados.idassinatura;
                parcela.nossonumero = dados.nossonumero;
                parcela.numparcela = dados.numparcela;
                parcela.plataforma = dados.plataforma;
                parcela.valorliquido = dados.valorliquido;
                parcela.observacao = dados.observacao;
                parcela.acrescimos = dados.acrescimos;
                parcela.descontos = dados.descontos;
                parcela.valor = dados.valor;
                parcela.dataup = DateTime.Now;
                parcela.dataestimadapagto = dados.dataestimadapagto;

                _context.Update(parcela);
                await _context.SaveChangesAsync();
                return MapToViewModel(parcela);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro ao editar parcela com ID {id}.", id);
                throw;
            }
        }

        public async Task<ParcelaViewModel?> ExcluirParcela(string id)
        {
            var parcela = await _context.parcelas.FirstOrDefaultAsync(p => p.id == id);
            if (parcela == null)
            {
                _logger.LogWarning("Tentativa de exclusão de parcela não encontrada. ID: {id}", id);
                return null;
            }

            try
            {
                _context.parcelas.Remove(parcela);
                await _context.SaveChangesAsync();
                return MapToViewModel(parcela);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir parcela ID {id}.", id);
                throw;
            }
        }

        public async Task<ParcelaViewModel?> ListarParcelaById(string id)
        {
            var parcela = await _context.parcelas.Include(p => p.assinatura).FirstOrDefaultAsync(p => p.id == id);
            return parcela != null ? MapToViewModel(parcela) : null;
        }

        public async Task<ParcelaViewModel?> ListarParcelaByAssinatura(string idassinatura, int? numeroparcela = 0)
        {
            var parcela = await _context.parcelas.FirstOrDefaultAsync(p => p.idassinatura == idassinatura && (numeroparcela == 0 || p.numparcela == numeroparcela));
            return parcela != null ? MapToViewModel(parcela) : null;
        }

        public async Task<ParcelaViewModel?> ListarParcelaByIdCheckout(string id)
        {
            var parcela = await _context.parcelas.Include(p => p.assinatura).FirstOrDefaultAsync(p => p.nossonumero == id);
            return parcela != null ? MapToViewModel(parcela) : null;
        }

        private ParcelaViewModel MapToViewModel(Parcela parcela) => new()
        {
            id = parcela.id,
            idassinatura = parcela.idassinatura,
            idcheckout = parcela.idcheckout,
            idcaixa = parcela.idcaixa,
            idformapagto = parcela.idformapagto,
            acrescimos = parcela.acrescimos,
            descontos = parcela.descontos,
            descontoantecipacao = parcela.descontoantecipacao,
            descontoplataforma = parcela.descontoplataforma,
            comissao = parcela.comissao,
            numparcela = parcela.numparcela,
            nossonumero = parcela.nossonumero,
            databaixa = parcela.databaixa,
            datavencimento = parcela.datavencimento,
            observacao = parcela.observacao,
            plataforma = parcela.plataforma,
            valorliquido = parcela.valorliquido,
            valor = parcela.valor,
            idparceiro = (parcela.assinatura != null) ? parcela.assinatura.idparceiro : null,
            dataestimadapagto = parcela.dataestimadapagto
        };

        public async Task<IEnumerable<ListParcelaViewModel>> ListarParcela(DateTime ini, DateTime fim, int tipodata, string idparceiro, int forma, string? filtro, int status, string idassinatura)
        {
            var contas = _context.parcelas.Include(m => m.assinatura).Include(m => m.assinatura.parceiro).
                Where((Parcela m) => ((String.IsNullOrWhiteSpace(filtro) || m.observacao.ToUpper().Contains(filtro.ToUpper())) || (String.IsNullOrWhiteSpace(filtro) || (m.idassinatura ?? " ").ToUpper().Contains(filtro.ToUpper())) ||
            (String.IsNullOrWhiteSpace(filtro) || (m.idcheckout ?? " ").ToUpper().Contains(filtro.ToUpper())) ||
            (String.IsNullOrWhiteSpace(filtro) || (m.nossonumero ?? " ").ToUpper().Contains(filtro.ToUpper())))
            && (idassinatura == "0" || m.idassinatura == idassinatura)
             && ((tipodata == 0 && m.datavencimento >= ini && m.datavencimento <= fim)
             || (tipodata == 1 && m.databaixa >= ini && m.databaixa <= fim))
                     && (status == 3 || (status == 0 && m.databaixa == null) || (status == 1 && m.databaixa != null) ||
                     (status == 2 && m.idcaixa != null))
                     && (idparceiro == "0" || m.assinatura.idparceiro == idparceiro)
                        && (forma == 3 || (int)m.idformapagto == forma)

            )
                .Select(c => new ListParcelaViewModel
                {
                    id = c.id,
                    idassinatura = c.idassinatura,
                    idcheckout = c.idcheckout,
                    idcaixa = c.idcaixa,
                    idformapagto = c.idformapagto,
                    acrescimos = c.acrescimos,
                    descontos = c.descontos,
                    descontoantecipacao = c.descontoantecipacao,
                    descontoplataforma = c.descontoplataforma,
                    comissao = c.comissao,
                    numparcela = c.numparcela,
                    nossonumero = c.nossonumero,
                    databaixa = c.databaixa,
                    datavencimento = c.datavencimento,
                    observacao = c.observacao,
                    plataforma = c.plataforma,
                    valorliquido = c.valorliquido,
                    nomeparceiro = c.assinatura.parceiro.RazaoSocial,
                    descforma = c.idformapagto.ToString(),
                    valor = c.valor,
                    status = (c.databaixa == null) ? "Pendente" : (c.idcaixa == 0 || c.idcaixa == null) ? "Baixado" : "Caixa",
                    dataestimadapagto = (c.dataestimadapagto == null) ? DateTime.Parse("31/12/2500") : c.dataestimadapagto
                }
                ).ToList();
            return (contas);
        }

        public async Task<IEnumerable<visaogeralviewmodel>> visaogeralcarteira(DateTime ini, DateTime fim, string idparceiro)
        {
            var hoje = DateTime.Today;

            var query = _context.parcelas
                .Include(p => p.assinatura)
                    .ThenInclude(a => a.parceiro)
                .Where(p =>
                    p.datavencimento >= ini &&
                    p.datavencimento <= fim &&
                    (idparceiro == "0" || p.assinatura.idparceiro.ToString() == idparceiro)
                )
                .GroupBy(p => new
                {
                    p.assinatura.parceiro.uid,
                    p.assinatura.parceiro.RazaoSocial,
                    p.idformapagto,  // sem .ToString()
                    p.plataforma     // sem .ToString()
                })
                .Select(g => new visaogeralviewmodel
                {
                    idParceiro = g.Key.uid,
                    nomeParceiro = g.Key.RazaoSocial,
                    formaPagamento = g.Key.idformapagto.ToString(),
                    plataForma = g.Key.plataforma,
                    valorLiquido = g.Sum(p => p.valorliquido),
                    valorPago = g.Where(p => p.databaixa != null).Sum(p => p.valor),
                    valorVencidas = g.Where(p => p.databaixa == null && p.datavencimento < hoje).Sum(p => p.valor),
                    valorAVencer = g.Where(p => p.databaixa == null && p.datavencimento >= hoje).Sum(p => p.valor),
                    comissaoPaga = g.Where(p => p.idcaixa != null).Sum(p => p.comissao),
                    taxaAntecipacao = g.Sum(p => p.descontoantecipacao),
                    taxaPlataforma = g.Sum(p => p.descontoplataforma),
                    valorRecebido = g.Where(p => p.idcaixa != null).Sum(p => p.valorliquido),
                    valortotal = g.Sum(p => p.valor),
                    arvores = (int)g.Sum(p => p.valor) / 47 / 84,
                    acompensar = g.Where(p => p.idcaixa == null && p.databaixa != null).Sum(p => p.valor)
                })
                .ToList();

            return (query);
        }
    }
}