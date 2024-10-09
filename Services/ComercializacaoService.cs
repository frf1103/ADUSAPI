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
            Comercializacao.DataPedido = dados.DataPedido;
            Comercializacao.Frete = dados.Frete;
            Comercializacao.idconta = dados.idconta;
            Comercializacao.idconta = dados.idconta;
            Comercializacao.IdFazenda = dados.IdFazenda;
            Comercializacao.uid = dados.uid;
            Comercializacao.datains = DateTime.Now;

            await _context.AddAsync(Comercializacao);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Pedido de compra " + Comercializacao.Id.ToString() + "/" + Comercializacao.IdSafra.ToString() + "/" + Comercializacao.IdFazenda.ToString() + "/" + Comercializacao.IdParceiro.ToString(), datalog = DateTime.Now, idconta = dados.idconta });

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
                Id = Comercializacao.Id,
                IdFazenda = Comercializacao.IdFazenda,
                DataPedido = Comercializacao.DataPedido,
                Frete = Comercializacao.Frete
            };
        }

        public async Task<ComercializacaoViewModel>? SalvarComercializacao(int id, string idconta, ComercializacaoViewModel dados)
        {
            var Comercializacao = _context.comercializacoes.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
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
                Comercializacao.DataPedido = dados.DataPedido;
                Comercializacao.Frete = dados.Frete;

                Comercializacao.idconta = dados.idconta;
                Comercializacao.uid = dados.uid;
                Comercializacao.dataup = DateTime.Now;
                Comercializacao.IdFazenda = dados.IdFazenda;
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Pedido de compra " + Comercializacao.Id.ToString() + "/" + Comercializacao.IdSafra.ToString() + "/" + Comercializacao.IdFazenda.ToString() + "/" + Comercializacao.IdParceiro.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
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
                    Id = Comercializacao.Id,
                    IdFazenda = Comercializacao.IdFazenda,
                    DataPedido = Comercializacao.DataPedido,
                    Frete = Comercializacao.Frete
                };
            }
            else return null;
        }

        public async Task<ComercializacaoViewModel>? ExcluirComercializacao(int id, string idconta, string uid)
        {
            var Comercializacao = _context.comercializacoes.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (Comercializacao != null)
            {
                ComercializacaoViewModel dados = new ComercializacaoViewModel
                {
                    Id = Comercializacao.Id
                };
                _excluirComercializacaoValidator.ValidateAndThrow(dados);
                _context.comercializacoes.Remove(Comercializacao);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusao  Pedido de compra " + Comercializacao.Id.ToString() + "/" + Comercializacao.IdSafra.ToString() + "/" + Comercializacao.IdFazenda.ToString() + "/" + Comercializacao.IdParceiro.ToString() + "/" + Comercializacao.NumeroContrato.ToString().Trim(), datalog = DateTime.Now, idconta = idconta });
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
                    Id = Comercializacao.Id,
                    IdFazenda = Comercializacao.IdFazenda,
                    DataPedido = Comercializacao.DataPedido,
                    Frete = Comercializacao.Frete
                };
            }
            else return null;
        }

        public async Task<ComercializacaoViewModel>? ListarComercializacaoById(int id, string idconta)
        {
            var Comercializacao = _context.comercializacoes.Where(m => m.Id == id && m.idconta == idconta).FirstOrDefault();
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
                    Id = Comercializacao.Id,
                    IdFazenda = Comercializacao.IdFazenda,
                    DataPedido = Comercializacao.DataPedido,
                    Frete = Comercializacao.Frete
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListComercializacaoViewModel>> ListarComercializacao(string idconta, int idano, int idorganizacao, int idfazenda, int idsafra, int idparceiro, int idmoeda, DateTime? dini, DateTime? dfim, string? filtro)
        {
            //var condicao = (Comercializacao m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));

            var Comercializacoes = _context.comercializacoes.Include(m => m.safra).Include(m => m.moeda).Include(m => m.parceiro)
                .Include(m => m.safra.anoAgricola.organizacao).Include(m => m.fazenda)
                .Where(m => (m.safra.anoAgricola.organizacao.idconta == idconta) &&
                (m.safra.anoAgricola.Id == idano) &&
                (idsafra == 0 || m.IdSafra == idsafra) &&
                (idparceiro == 0 || m.IdParceiro == idparceiro) &&
                (idmoeda == 0 || m.IdMoeda == idmoeda) && (m.fazenda.IdOrganizacao == idorganizacao) &&
                (idfazenda == 0 || m.IdFazenda == idfazenda) &&
                (m.DataPedido >= dini && m.DataPedido <= dfim) &&
                (String.IsNullOrWhiteSpace(filtro) || m.NumeroContrato.ToUpper().Contains(filtro.ToUpper()))
                )

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
                    nomeparceiro = c.parceiro.Fantasia,
                    IdFazenda = c.IdFazenda,
                    descfazenda = c.fazenda.Descricao,
                    DataPedido = c.DataPedido,
                    Frete = c.Frete
                }
                ).ToList();
            return (Comercializacoes);
        }

        public async Task<IEnumerable<ItemEntregaContratoViewModel>> ListarItensEntrega(int idcomercialiazao, string idconta, int cond = 1)
        {
            var result = _context.comercializacoes.Where((Comercializacao m) => (m.Id == idcomercialiazao && m.idconta == idconta))

                .GroupBy(p => new { p.Id, p.Quantidade })
                .Select(group => new
                {
                    group.Key.Id,
                    group.Key.Quantidade,
                    Entregas = _context.entregaContratos
                        .Where(e => e.IdComercializacao == group.Key.Id)
                        .Sum(e => (decimal?)e.Quantidade) ?? 0 // Usar int? para evitar exceções de nulos
                })
                .Select(r => new ItemEntregaContratoViewModel
                {
                    id = r.Id,
                    datanf = DateTime.Now.Date,
                    idcomercializacao = r.Id,
                    nf = " ",
                    preco = 0,
                    qtd = r.Quantidade - r.Entregas,
                    qtdcompra = r.Quantidade,
                    recebido = r.Entregas,
                    saldo = r.Quantidade - r.Entregas,
                    total = 0
                })
                .Where(r => r.qtd > 0 || cond == 0)
                .ToList();
            return (result);
        }

        public async Task<IEnumerable<EntregaContratoViewModel>> ListarEntregasByProduto(int id, string idconta)
        {
            var condicao = (EntregaContrato m) => (m.Id == id && m.idconta == idconta);

            var result = _context.entregaContratos.Where(condicao)
                .Select(r => new EntregaContratoViewModel
                {
                    Id = r.Id,
                    DataEntrega = r.DataEntrega,
                    Documento = r.Documento,
                    Quantidade = r.Quantidade,
                    IdComercializacao = r.IdComercializacao
                })
                .OrderBy(x => x.DataEntrega).ToList();
            return (result);
        }
    }
}