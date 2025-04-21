using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPICore.Models.Enum;
using ADUSAPICore.Models.Parcela;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ADUSAPI.Services
{
    public class ParcelaService
    {
        private readonly ADUSContext _context;
        //   private readonly ParcelaValidator _adicionarParcelaValidator;
        // private readonly ExcluirParcelaValidator _excluirParcelaValidator;

        public ParcelaService(ADUSContext context)
        //, ParcelaValidator adicionarParcelaValidator)
        //, ExcluirParcelaValidator excluirParcelaValidator)
        {
            _context = context;
            //_adicionarParcelaValidator = adicionarParcelaValidator;
            //_excluirParcelaValidator = excluirParcelaValidator;
        }

        public async Task<ParcelaViewModel> AdicionarParcela(ParcelaViewModel dados)
        {
            //_adicionarParcelaValidator.ValidateAndThrow(dados);
            var conta = new Parcela();
            conta.id = dados.id;
            conta.idcheckout = dados.idcheckout;
            conta.idcaixa = dados.idcaixa;
            conta.idformapagto = dados.idformapagto;
            conta.comissao = dados.comissao;
            conta.databaixa = dados.databaixa;
            conta.datavencimento = dados.datavencimento;
            conta.descontoantecipacao = dados.descontoantecipacao;
            conta.descontoplataforma = dados.descontoplataforma;
            conta.idassinatura = dados.idassinatura;
            conta.nossonumero = dados.nossonumero;
            conta.numparcela = dados.numparcela;
            conta.plataforma = dados.plataforma;
            conta.valorliquido = dados.valorliquido;
            conta.observacao = dados.observacao;
            conta.acrescimos = dados.acrescimos;
            conta.descontos = dados.descontos;
            conta.valor = dados.valor;

            conta.datains = DateTime.Now;
            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new ParcelaViewModel
            {
                id = conta.id,
                idassinatura = conta.idassinatura,
                idcheckout = conta.idcheckout,
                idcaixa = conta.idcaixa,
                idformapagto = conta.idformapagto,
                acrescimos = conta.acrescimos,
                descontos = conta.descontos,
                descontoantecipacao = conta.descontoantecipacao,
                descontoplataforma = conta.descontoplataforma,
                comissao = (double)conta.comissao,
                numparcela = conta.numparcela,
                nossonumero = conta.nossonumero,
                databaixa = conta.databaixa,
                datavencimento = conta.datavencimento,
                observacao = conta.observacao,
                plataforma = conta.plataforma,
                valorliquido = conta.valorliquido
            };
        }

        public async Task<ParcelaViewModel>? SalvarParcela(string id, ParcelaViewModel dados)
        {
            //_adicionarParcelaValidator.ValidateAndThrow(dados);
            var conta = _context.parcelas.Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                conta.id = dados.id;
                conta.idcheckout = dados.idcheckout;
                conta.idcaixa = dados.idcaixa;
                conta.idformapagto = dados.idformapagto;
                conta.comissao = dados.comissao;
                conta.databaixa = dados.databaixa;
                conta.datavencimento = dados.datavencimento;
                conta.descontoantecipacao = dados.descontoantecipacao;
                conta.descontoplataforma = dados.descontoplataforma;
                conta.idassinatura = dados.idassinatura;
                conta.nossonumero = dados.nossonumero;
                conta.numparcela = dados.numparcela;
                conta.plataforma = dados.plataforma;
                conta.valorliquido = dados.valorliquido;
                conta.observacao = dados.observacao;
                conta.acrescimos = dados.acrescimos;
                conta.descontos = dados.descontos;
                conta.valor = dados.valor;
                conta.dataup = DateTime.Now;

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new ParcelaViewModel
                {
                    id = conta.id,
                    idassinatura = conta.idassinatura,
                    idcheckout = conta.idcheckout,
                    idcaixa = conta.idcaixa,
                    idformapagto = conta.idformapagto,
                    acrescimos = conta.acrescimos,
                    descontos = conta.descontos,
                    descontoantecipacao = conta.descontoantecipacao,
                    descontoplataforma = conta.descontoplataforma,
                    comissao = (double)conta.comissao,
                    numparcela = conta.numparcela,
                    nossonumero = conta.nossonumero,
                    databaixa = conta.databaixa,
                    datavencimento = conta.datavencimento,
                    observacao = conta.observacao,
                    plataforma = conta.plataforma,
                    valorliquido = conta.valorliquido,
                    valor = conta.valor
                };
            }
            else return null;
        }

        public async Task<ParcelaViewModel>? ExcluirParcela(string id)
        {
            var conta = _context.parcelas.Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                ParcelaViewModel dados = new ParcelaViewModel
                {
                    id = conta.id,
                };
                //  _excluirParcelaValidator.ValidateAndThrow(dados);
                _context.parcelas.Remove(conta);
                await _context.SaveChangesAsync();
                return new ParcelaViewModel
                {
                    id = conta.id,
                    idassinatura = conta.idassinatura,
                    idcheckout = conta.idcheckout,
                    idcaixa = conta.idcaixa,
                    idformapagto = conta.idformapagto,
                    acrescimos = conta.acrescimos,
                    descontos = conta.descontos,
                    descontoantecipacao = conta.descontoantecipacao,
                    descontoplataforma = conta.descontoplataforma,
                    comissao = (double)conta.comissao,
                    numparcela = conta.numparcela,
                    nossonumero = conta.nossonumero,
                    databaixa = conta.databaixa,
                    datavencimento = conta.datavencimento,
                    observacao = conta.observacao,
                    plataforma = conta.plataforma,
                    valorliquido = conta.valorliquido,
                    valor = conta.valor
                };
            }
            else return null;
        }

        public async Task<ParcelaViewModel>? ListarParcelaById(string id)
        {
            var conta = _context.parcelas
            .Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                return new ParcelaViewModel
                {
                    id = conta.id,
                    idassinatura = conta.idassinatura,
                    idcheckout = conta.idcheckout,
                    idcaixa = conta.idcaixa,
                    idformapagto = conta.idformapagto,
                    acrescimos = conta.acrescimos,
                    descontos = conta.descontos,
                    descontoantecipacao = conta.descontoantecipacao,
                    descontoplataforma = conta.descontoplataforma,
                    comissao = (double)conta.comissao,
                    numparcela = conta.numparcela,
                    nossonumero = conta.nossonumero,
                    databaixa = conta.databaixa,
                    datavencimento = conta.datavencimento,
                    observacao = conta.observacao,
                    plataforma = conta.plataforma,
                    valorliquido = conta.valorliquido,
                    valor = conta.valor
                };
            }
            else return null;
        }

        public async Task<ParcelaViewModel>? ListarParcelaByIdCheckout(string id)
        {
            var conta = _context.parcelas
            .Where(p => p.idcheckout == id).FirstOrDefault();
            if (conta != null)
            {
                return new ParcelaViewModel
                {
                    id = conta.id,
                    idassinatura = conta.idassinatura,
                    idcheckout = conta.idcheckout,
                    idcaixa = conta.idcaixa,
                    idformapagto = conta.idformapagto,
                    acrescimos = conta.acrescimos,
                    descontos = conta.descontos,
                    descontoantecipacao = conta.descontoantecipacao,
                    descontoplataforma = conta.descontoplataforma,
                    comissao = (double)conta.comissao,
                    numparcela = conta.numparcela,
                    nossonumero = conta.nossonumero,
                    databaixa = conta.databaixa,
                    datavencimento = conta.datavencimento,
                    observacao = conta.observacao,
                    plataforma = conta.plataforma,
                    valorliquido = conta.valorliquido,
                    valor = conta.valor
                };
            }
            else return null;
        }

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
                    comissao = (double)c.comissao,
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
                    status = (c.databaixa == null) ? "Pendente" : (c.idcaixa == 0 || c.idcaixa == null) ? "Baixado" : "Caixa"
                }
                ).ToList();
            return (contas);
        }
    }
}