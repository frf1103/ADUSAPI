using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.PedidoCompra;
using FarmPlannerAPICore.Models.PedidoCompra;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FarmPlannerAPI.Services
{
    public class PedidoCompraService
    {
        private readonly FarmPlannerContext _context;
        private readonly PedidoCompraValidator _adicionarPedidoCompraValidator;
        private readonly ExcluirPedidoCompraValidator _excluirPedidoCompraValidator;

        public PedidoCompraService(FarmPlannerContext context, PedidoCompraValidator adicionarPedidoCompraValidator, ExcluirPedidoCompraValidator excluirPedidoCompraValidator)
        {
            _context = context;
            _adicionarPedidoCompraValidator = adicionarPedidoCompraValidator;
            _excluirPedidoCompraValidator = excluirPedidoCompraValidator;
        }

        public async Task<PedidoCompraViewModel> AdicionarPedidoCompra(PedidoCompraViewModel dados)
        {
            _adicionarPedidoCompraValidator.ValidateAndThrow(dados);
            var PedidoCompra = new PedidoCompra();

            PedidoCompra.idsafra = dados.idsafra;
            PedidoCompra.idfazenda = dados.idfazenda;
            PedidoCompra.id = dados.id;
            PedidoCompra.idconta = dados.idconta;
            PedidoCompra.idmoeda = dados.idmoeda;
            PedidoCompra.datapedido = dados.datapedido;
            PedidoCompra.idfornecedor = dados.idfornecedor;
            PedidoCompra.uid = dados.uid;
            PedidoCompra.total = dados.total;
            PedidoCompra.datains = DateTime.Now;
            PedidoCompra.pedidofornecedor = dados.pedidofonecedor;
            PedidoCompra.observacao = dados.observacao;
            PedidoCompra.vencimento = dados.vencimento;

            await _context.AddAsync(PedidoCompra);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Pedido de compra " + PedidoCompra.id.ToString() + "/" + PedidoCompra.idsafra.ToString() + "/" + PedidoCompra.idfazenda.ToString() + "/" + PedidoCompra.idfornecedor.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new PedidoCompraViewModel
            {
                id = PedidoCompra.id,
                idconta = PedidoCompra.idconta,
                datapedido = PedidoCompra.datapedido,
                idsafra = PedidoCompra.idsafra,
                idfornecedor = PedidoCompra.idfornecedor,
                idmoeda = PedidoCompra.idmoeda,
                idfazenda = PedidoCompra.idfazenda,
                total = PedidoCompra.total,
                observacao = PedidoCompra.observacao,
                pedidofonecedor = PedidoCompra.pedidofornecedor,
                vencimento = PedidoCompra.vencimento,
                datains = PedidoCompra.datains,
                dataup = PedidoCompra.dataup,
                uid = PedidoCompra.uid,
            };
        }

        public async Task<PedidoCompraViewModel>? SalvarPedidoCompra(int id, string idconta, PedidoCompraViewModel dados)
        {
            var PedidoCompra = _context.pedidoscompra.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (PedidoCompra != null)
            {
                PedidoCompra.idsafra = dados.idsafra;
                PedidoCompra.idfazenda = dados.idfazenda;
                PedidoCompra.id = dados.id;
                PedidoCompra.idconta = dados.idconta;
                PedidoCompra.idmoeda = dados.idmoeda;
                PedidoCompra.idfornecedor = dados.idfornecedor;
                PedidoCompra.vencimento = dados.vencimento;
                PedidoCompra.datapedido = dados.datapedido;
                PedidoCompra.uid = dados.uid;
                PedidoCompra.total = dados.total;
                PedidoCompra.dataup = DateTime.Now;
                PedidoCompra.pedidofornecedor = dados.pedidofonecedor;
                PedidoCompra.observacao = dados.observacao;

                _context.Update(PedidoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteraçao  Pedido de compra " + PedidoCompra.id.ToString() + "/" + PedidoCompra.idsafra.ToString() + "/" + PedidoCompra.idfazenda.ToString() + "/" + PedidoCompra.idfornecedor.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new PedidoCompraViewModel
                {
                    id = PedidoCompra.id,
                    idconta = PedidoCompra.idconta,
                    idsafra = PedidoCompra.idsafra,
                    idfornecedor = PedidoCompra.idfornecedor,
                    idmoeda = PedidoCompra.idmoeda,
                    datapedido = PedidoCompra.datapedido,
                    idfazenda = PedidoCompra.idfazenda,
                    total = PedidoCompra.total,
                    observacao = PedidoCompra.observacao,
                    pedidofonecedor = PedidoCompra.pedidofornecedor,
                    vencimento = PedidoCompra.vencimento,
                    datains = PedidoCompra.datains,
                    dataup = PedidoCompra.dataup,
                    uid = PedidoCompra.uid,
                };
            }
            else return null;
        }

        public async Task<PedidoCompraViewModel>? ExcluirPedidoCompra(int id, string idconta, string uid)
        {
            var PedidoCompra = _context.pedidoscompra.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (PedidoCompra != null)
            {
                PedidoCompraViewModel dados = new PedidoCompraViewModel
                {
                    id = PedidoCompra.id
                };
                _excluirPedidoCompraValidator.ValidateAndThrow(dados);
                _context.pedidoscompra.Remove(PedidoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Pedido de compra " + PedidoCompra.id.ToString() + "/" + PedidoCompra.idsafra.ToString() + "/" + PedidoCompra.idfazenda.ToString() + "/" + PedidoCompra.idfornecedor.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new PedidoCompraViewModel
                {
                    id = PedidoCompra.id,
                    idconta = PedidoCompra.idconta,
                    idsafra = PedidoCompra.idsafra,
                    idfornecedor = PedidoCompra.idfornecedor,
                    idmoeda = PedidoCompra.idmoeda,
                    idfazenda = PedidoCompra.idfazenda,
                    total = PedidoCompra.total,
                    datapedido = PedidoCompra.datapedido,
                    observacao = PedidoCompra.observacao,
                    pedidofonecedor = PedidoCompra.pedidofornecedor,
                    vencimento = PedidoCompra.vencimento,
                    datains = PedidoCompra.datains,
                    dataup = PedidoCompra.dataup,
                    uid = PedidoCompra.uid,
                };
            }
            else return null;
        }

        public async Task<PedidoCompraViewModel>? ListarPedidoCompraById(int id, string idconta)
        {
            var PedidoCompra = _context.pedidoscompra.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (PedidoCompra != null)
            {
                return new PedidoCompraViewModel
                {
                    id = PedidoCompra.id,
                    idconta = PedidoCompra.idconta,
                    idsafra = PedidoCompra.idsafra,
                    idfornecedor = PedidoCompra.idfornecedor,
                    idmoeda = PedidoCompra.idmoeda,
                    idfazenda = PedidoCompra.idfazenda,
                    total = PedidoCompra.total,
                    datapedido = PedidoCompra.datapedido,
                    observacao = PedidoCompra.observacao,
                    pedidofonecedor = PedidoCompra.pedidofornecedor,
                    vencimento = PedidoCompra.vencimento,
                    datains = PedidoCompra.datains,
                    dataup = PedidoCompra.dataup,
                    uid = PedidoCompra.uid,
                };
            }
            else return null;
        }

        public async Task<IEnumerable<PedidoCompraViewModel>> ListarPedidoCompra(int idorganizacao, int idano, int idfazenda, int idsafra, string idconta, int idproduto, int idmoeda, int idfornec, DateTime ini, DateTime fim, string? filtro)
        {
            var condicao = (PedidoCompra m) => (idfazenda == 0 || m.idfazenda == idfazenda) &&
            (m.fazenda.IdOrganizacao == idorganizacao) &&
            (m.safra.IdAnoAgricola == idano) &&
            (idsafra == 0 || m.idsafra == idsafra) && m.idconta == idconta &&
            (idfornec == 0 || m.idfornecedor == idfornec) &&
            (String.IsNullOrWhiteSpace(filtro) || m.observacao.ToUpper().Contains(filtro.ToUpper()))
            &&
            (idproduto == 0 || m.produtos.Any(p => p.idproduto == idproduto)) &&
            (idmoeda == 0 || m.idmoeda == idmoeda) &&
            (m.datapedido >= ini && m.datapedido <= fim);
            var query = _context.pedidoscompra
                .Include(m => m.fazenda).Include(m => m.safra).Include(m => m.moeda).Include(m => m.parceiro);
            var PedidoCompras = query.Where(condicao)
                .Select(c => new PedidoCompraViewModel
                {
                    id = c.id,
                    idconta = c.idconta,
                    idsafra = c.idsafra,
                    idfornecedor = c.idfornecedor,
                    idmoeda = c.idmoeda,
                    idfazenda = c.idfazenda,
                    total = c.total,
                    observacao = c.observacao,
                    pedidofonecedor = c.pedidofornecedor,
                    descfazenda = c.fazenda.Descricao,
                    descmoeda = c.moeda.Descricao,
                    descsafra = c.safra.Descricao,
                    datapedido = c.datapedido,
                    descfornec = c.parceiro.Fantasia,
                    vencimento = c.vencimento,
                    datains = c.datains,
                    dataup = c.dataup,
                    uid = c.uid,
                }
                ).ToList();
            return (PedidoCompras);
        }
    }
}