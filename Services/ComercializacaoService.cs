using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Comercializacao;
using FarmPlannerAPICore.Models.Comercializacao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ComercializacaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ComercializacaoValidator _adicionarComercializacaoValidator;
        private readonly ExcluirComercializacaoValidator _excluirComercializacaoValidator;

        public ComercializacaoService(FarmPlannerContext context, ComercializacaoValidator adicionarComercializacaoValidator, ExcluirComercializacaoValidator excluirComercializacaoValidator)
        {
            _context = context;
            _adicionarComercializacaoValidator = adicionarComercializacaoValidator;
            _excluirComercializacaoValidator = excluirComercializacaoValidator;
        }

        public async Task<ComercializacaoViewModel> AdicionarComercializacao(ComercializacaoViewModel dados)
        {
            _adicionarComercializacaoValidator.ValidateAndThrow(dados);
            var Comercializacao = new Comercializacao();
            Comercializacao.IdSafra = dados.IdSafra;
            Comercializacao.IdMoeda = dados.IdMoeda;
            Comercializacao.IdParceiro = dados.IdParceiro;
            Comercializacao.CBOT = dados.CBOT;
            Comercializacao.Quantidade = dados.Quantidade;
            Comercializacao.Cambio = dados.Cambio;
            Comercializacao.DataEntrega = dados.DataEntrega;
            Comercializacao.DataPagamento = dados.DataPagamento;
            Comercializacao.Premio = dados.Premio;
            Comercializacao.Descontos = dados.Descontos;
            Comercializacao.Trava = dados.Trava;
            Comercializacao.ValorLiquido = dados.ValorLiquido;
            Comercializacao.ValorUnitario = dados.ValorUnitario;
            Comercializacao.ValorTotal = dados.ValorTotal;
            Comercializacao.idconta = dados.idconta;

            await _context.AddAsync(Comercializacao);
            await _context.SaveChangesAsync();
            return new ComercializacaoViewModel
            {
                IdSafra = Comercializacao.IdSafra,
                IdMoeda = Comercializacao.IdMoeda,
                IdParceiro = Comercializacao.IdParceiro,
                CBOT = Comercializacao.CBOT,
                Quantidade = Comercializacao.Quantidade,
                Cambio = Comercializacao.Cambio,
                DataEntrega = Comercializacao.DataEntrega,
                DataPagamento = Comercializacao.DataPagamento,
                Premio = Comercializacao.Premio,
                Descontos = Comercializacao.Descontos,
                Trava = Comercializacao.Trava,
                ValorLiquido = Comercializacao.ValorLiquido,
                ValorUnitario = Comercializacao.ValorUnitario,
                ValorTotal = Comercializacao.ValorTotal,
                Id = Comercializacao.Id
            };
        }

        public async Task<ComercializacaoViewModel>? SalvarComercializacao(int id, ComercializacaoViewModel dados)
        {
            var Comercializacao = _context.comercializacoes.Find(id);
            if (Comercializacao != null)
            {
                Comercializacao.IdSafra = dados.IdSafra;
                Comercializacao.IdMoeda = dados.IdMoeda;
                Comercializacao.IdParceiro = dados.IdParceiro;
                Comercializacao.CBOT = dados.CBOT;
                Comercializacao.Quantidade = dados.Quantidade;
                Comercializacao.Cambio = dados.Cambio;
                Comercializacao.DataEntrega = dados.DataEntrega;
                Comercializacao.DataPagamento = dados.DataPagamento;
                Comercializacao.Premio = dados.Premio;
                Comercializacao.Descontos = dados.Descontos;
                Comercializacao.Trava = dados.Trava;
                Comercializacao.ValorLiquido = dados.ValorLiquido;
                Comercializacao.ValorUnitario = dados.ValorUnitario;
                Comercializacao.ValorTotal = dados.ValorTotal;

                _context.Update(Comercializacao);
                await _context.SaveChangesAsync();
                return new ComercializacaoViewModel
                {
                    IdSafra = Comercializacao.IdSafra,
                    IdMoeda = Comercializacao.IdMoeda,
                    IdParceiro = Comercializacao.IdParceiro,
                    CBOT = Comercializacao.CBOT,
                    Quantidade = Comercializacao.Quantidade,
                    Cambio = Comercializacao.Cambio,
                    DataEntrega = Comercializacao.DataEntrega,
                    DataPagamento = Comercializacao.DataPagamento,
                    Premio = Comercializacao.Premio,
                    Descontos = Comercializacao.Descontos,
                    Trava = Comercializacao.Trava,
                    ValorLiquido = Comercializacao.ValorLiquido,
                    ValorUnitario = Comercializacao.ValorUnitario,
                    ValorTotal = Comercializacao.ValorTotal,
                    Id = Comercializacao.Id
                };
            }
            else return null;
        }

        public async Task<ComercializacaoViewModel>? ExcluirComercializacao(int id, ComercializacaoViewModel dados)
        {
            _excluirComercializacaoValidator.ValidateAndThrow(dados);
            var Comercializacao = _context.comercializacoes.Find(id);
            if (Comercializacao != null)
            {
                _context.comercializacoes.Remove(Comercializacao);
                await _context.SaveChangesAsync();
                return new ComercializacaoViewModel
                {
                    IdSafra = dados.IdSafra,
                    IdMoeda = dados.IdMoeda,
                    IdParceiro = dados.IdParceiro,
                    CBOT = dados.CBOT,
                    Quantidade = dados.Quantidade,
                    Cambio = dados.Cambio,
                    DataEntrega = dados.DataEntrega,
                    DataPagamento = dados.DataPagamento,
                    Premio = dados.Premio,
                    Descontos = dados.Descontos,
                    Trava = dados.Trava,
                    ValorLiquido = dados.ValorLiquido,
                    ValorUnitario = dados.ValorUnitario,
                    ValorTotal = dados.ValorTotal,
                    Id = Comercializacao.Id
                };
            }
            else return null;
        }

        public async Task<ComercializacaoViewModel>? ListarComercializacaoById(int id)
        {
            var Comercializacao = _context.comercializacoes.Find(id);
            if (Comercializacao != null)
            {
                return new ComercializacaoViewModel
                {
                    IdSafra = Comercializacao.IdSafra,
                    IdMoeda = Comercializacao.IdMoeda,
                    IdParceiro = Comercializacao.IdParceiro,
                    CBOT = Comercializacao.CBOT,
                    Quantidade = Comercializacao.Quantidade,
                    Cambio = Comercializacao.Cambio,
                    DataEntrega = Comercializacao.DataEntrega,
                    DataPagamento = Comercializacao.DataPagamento,
                    Premio = Comercializacao.Premio,
                    Descontos = Comercializacao.Descontos,
                    Trava = Comercializacao.Trava,
                    ValorLiquido = Comercializacao.ValorLiquido,
                    ValorUnitario = Comercializacao.ValorUnitario,
                    ValorTotal = Comercializacao.ValorTotal,
                    Id = Comercializacao.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListComercializacaoViewModel>> ListarComercializacao(string idconta, int idano, int idsafra, int idparceiro, int idmoeda, DateTime? dini = null, DateTime? dfim = null)
        {
            //var condicao = (Comercializacao m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));

            var Comercializacoes = _context.comercializacoes.Include(m => m.safra).Include(m => m.moeda).Include(m => m.parceiro)
                .Include(m => m.safra.anoAgricola.organizacao)
                .Where(m => (m.safra.anoAgricola.organizacao.idconta == idconta) &&
                (m.safra.anoAgricola.Id == idano) &&
                (idsafra == 0 || m.IdSafra == idsafra) &&
                (idparceiro == 0 || m.IdParceiro == idparceiro) &&
                (idmoeda == 0 || m.IdMoeda == idmoeda))
                .Select(c => new ListComercializacaoViewModel
                {
                    IdSafra = c.IdSafra,
                    IdMoeda = c.IdMoeda,
                    IdParceiro = c.IdParceiro,
                    CBOT = c.CBOT,
                    Quantidade = c.Quantidade,
                    Cambio = c.Cambio,
                    DataEntrega = c.DataEntrega,
                    DataPagamento = c.DataPagamento,
                    Premio = c.Premio,
                    Descontos = c.Descontos,
                    Trava = c.Trava,
                    ValorLiquido = c.ValorLiquido,
                    ValorUnitario = c.ValorUnitario,
                    ValorTotal = c.ValorTotal,
                    Id = c.Id,
                    descmoeda = c.moeda.Descricao,
                    descsafra = c.safra.Descricao,
                    nomeparceiro = c.parceiro.Fantasia
                }
                ).ToList();
            return (Comercializacoes);
        }
    }
}