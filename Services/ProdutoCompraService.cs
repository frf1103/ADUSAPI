using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.ProdutoCompra;
using FarmPlannerAPICore.Models.PedidoCompra;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ProdutoCompraService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoCompraValidator _adicionarProdutoCompraValidator;
        private readonly ExcluirProdutoCompraValidator _excluirProdutoCompraValidator;

        public ProdutoCompraService(FarmPlannerContext context, ProdutoCompraValidator adicionarProdutoCompraValidator, ExcluirProdutoCompraValidator excluirProdutoCompraValidator)
        {
            _context = context;
            _adicionarProdutoCompraValidator = adicionarProdutoCompraValidator;
            _excluirProdutoCompraValidator = excluirProdutoCompraValidator;
        }

        public async Task<ProdutoCompraViewModel> AdicionarProdutoCompra(ProdutoCompraViewModel dados)
        {
            _adicionarProdutoCompraValidator.ValidateAndThrow(dados);
            var ProdutoCompra = new ProdutoCompra();

            ProdutoCompra.idpedido = dados.idpedido;
            ProdutoCompra.idproduto = dados.idproduto;
            ProdutoCompra.qtdcompra = dados.qtdcompra;
            ProdutoCompra.preco = dados.preco;
            ProdutoCompra.id = dados.id;
            ProdutoCompra.total = dados.total;
            ProdutoCompra.recebido = dados.recebido;
            ProdutoCompra.id = dados.id;
            ProdutoCompra.idconta = dados.idconta;
            ProdutoCompra.uid = dados.uid;
            ProdutoCompra.datains = DateTime.Now;

            await _context.AddAsync(ProdutoCompra);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Produto de compra " + ProdutoCompra.id.ToString() + "/" + ProdutoCompra.idproduto.ToString() + "/" + ProdutoCompra.idproduto.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new ProdutoCompraViewModel
            {
                id = ProdutoCompra.id,
                idconta = ProdutoCompra.idconta,
                idproduto = ProdutoCompra.idproduto,
                idpedido = ProdutoCompra.idpedido,
                qtdcompra = ProdutoCompra.qtdcompra,
                recebido = ProdutoCompra.recebido,
                total = ProdutoCompra.total,
                preco = ProdutoCompra.preco,
                uid = ProdutoCompra.uid,
                datains = ProdutoCompra.datains,
                dataup = ProdutoCompra.dataup
            };
        }

        public async Task<ProdutoCompraViewModel>? SalvarProdutoCompra(int id, string idconta, ProdutoCompraViewModel dados)
        {
            var ProdutoCompra = _context.produtoscompra.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (ProdutoCompra != null)
            {
                ProdutoCompra.idpedido = dados.idpedido;
                ProdutoCompra.idproduto = dados.idproduto;
                ProdutoCompra.qtdcompra = dados.qtdcompra;
                ProdutoCompra.preco = dados.preco;
                ProdutoCompra.id = dados.id;
                ProdutoCompra.total = dados.total;
                ProdutoCompra.recebido = dados.recebido;
                ProdutoCompra.id = dados.id;
                ProdutoCompra.idconta = dados.idconta;
                ProdutoCompra.uid = dados.uid;
                ProdutoCompra.dataup = DateTime.Now;

                _context.Update(ProdutoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteraçao  Produto de compra " + ProdutoCompra.id.ToString() + "/" + ProdutoCompra.idpedido.ToString() + "/" + ProdutoCompra.idproduto.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new ProdutoCompraViewModel
                {
                    id = ProdutoCompra.id,
                    idconta = ProdutoCompra.idconta,
                    idproduto = ProdutoCompra.idproduto,
                    idpedido = ProdutoCompra.idpedido,
                    qtdcompra = ProdutoCompra.qtdcompra,
                    recebido = ProdutoCompra.recebido,
                    total = ProdutoCompra.total,
                    preco = ProdutoCompra.preco,
                    uid = ProdutoCompra.uid,
                    datains = ProdutoCompra.datains,
                    dataup = ProdutoCompra.dataup
                };
            }
            else return null;
        }

        public async Task<ProdutoCompraViewModel>? ExcluirProdutoCompra(int id, string idconta, string uid)
        {
            var ProdutoCompra = _context.produtoscompra.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (ProdutoCompra != null)
            {
                ProdutoCompraViewModel dados = new ProdutoCompraViewModel
                {
                    id = ProdutoCompra.id
                };
                _excluirProdutoCompraValidator.ValidateAndThrow(dados);
                _context.produtoscompra.Remove(ProdutoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusao  Produto de compra " + ProdutoCompra.id.ToString() + "/" + ProdutoCompra.idpedido.ToString() + "/" + ProdutoCompra.idproduto.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new ProdutoCompraViewModel
                {
                    id = ProdutoCompra.id,
                    idconta = ProdutoCompra.idconta,
                    idproduto = ProdutoCompra.idproduto,
                    idpedido = ProdutoCompra.idpedido,
                    qtdcompra = ProdutoCompra.qtdcompra,
                    recebido = ProdutoCompra.recebido,
                    total = ProdutoCompra.total,
                    preco = ProdutoCompra.preco,
                    uid = ProdutoCompra.uid,
                    datains = ProdutoCompra.datains,
                    dataup = ProdutoCompra.dataup
                };
            }
            else return null;
        }

        public async Task<ProdutoCompraViewModel>? ListarProdutoCompraById(int id, string idconta)
        {
            var ProdutoCompra = _context.produtoscompra.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            decimal soma = _context.entregascompras.Where(x => x.idprodutopedido == ProdutoCompra.id).Sum(x => x.qtd);
            if (ProdutoCompra != null)
            {
                return new ProdutoCompraViewModel
                {
                    id = ProdutoCompra.id,
                    idconta = ProdutoCompra.idconta,
                    idproduto = ProdutoCompra.idproduto,
                    idpedido = ProdutoCompra.idpedido,
                    qtdcompra = ProdutoCompra.qtdcompra,
                    recebido = (soma == null) ? 0 : soma,
                    //recebido = ProdutoCompra.recebido,
                    total = ProdutoCompra.total,
                    preco = ProdutoCompra.preco,
                    uid = ProdutoCompra.uid,
                    datains = ProdutoCompra.datains,
                    dataup = ProdutoCompra.dataup
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ProdutoCompraViewModel>> ListarProdutoCompraByPedido(int idpedido, int idproduto, string idconta)
        {
            var condicao = (ProdutoCompra m) => (m.idpedido == idpedido && m.idconta == idconta && (idproduto == 0 || m.idproduto == idproduto));
            var query = _context.produtoscompra.Include(m => m.produto);

            var ProdutoCompras = query.Where(condicao)
                .Select(c => new ProdutoCompraViewModel
                {
                    id = c.id,
                    idconta = c.idconta,
                    idproduto = c.idproduto,
                    idpedido = c.idpedido,
                    qtdcompra = c.qtdcompra,
                    recebido = c.recebido,
                    total = c.total,
                    preco = c.preco,
                    uid = c.uid,
                    datains = c.datains,
                    dataup = c.dataup,
                    descproduto = c.produto.Descricao
                }
                ).ToList();
            return (ProdutoCompras);
        }

        public async Task<IEnumerable<ItemEntregaViewModel>> ListarItensEntrega(int idpedido, string idconta, int idproduto = 0, int cond = 1)
        {
            var condicao = (ProdutoCompra m) => (m.idpedido == idpedido && m.idconta == idconta && (idproduto == 0 || m.idproduto == idproduto));
            var query = _context.produtoscompra.Include(m => m.produto).Include(m => m.entregas);

            var result = _context.produtoscompra.Where((ProdutoCompra m) => (m.idpedido == idpedido && m.idconta == idconta && (idproduto == 0 || m.idproduto == idproduto)))
                .Include(m => m.produto).Include(m => m.produto.unidade)
                .GroupBy(p => new { p.id, p.idpedido, p.idproduto, p.qtdcompra, p.produto.Descricao, p.produto.unidade.descricao, p.produto.idunidade, p.preco, p.total })
                .Select(group => new
                {
                    group.Key.id,
                    group.Key.idpedido,
                    group.Key.idproduto,
                    group.Key.qtdcompra,
                    group.Key.Descricao,
                    group.Key.descricao,
                    group.Key.idunidade,
                    group.Key.preco,
                    group.Key.total,
                    Entregas = _context.entregascompras
                        .Where(e => e.idpedido == group.Key.idpedido && e.idproduto == group.Key.idproduto)
                        .Sum(e => (decimal?)e.qtd) ?? 0 // Usar int? para evitar exceções de nulos
                })
                .Select(r => new ItemEntregaViewModel
                {
                    id = r.id,
                    idpedido = r.idpedido,
                    idproduto = r.idproduto,
                    idpedidoproduto = r.id,
                    idunidade = r.idunidade,
                    qtdcompra = r.qtdcompra,
                    qtd = r.qtdcompra - r.Entregas,
                    conversor = 1,
                    qtdconv = r.qtdcompra - r.Entregas,
                    datanf = DateTime.Now.Date,
                    nf = " ",
                    descproduto = r.Descricao,
                    saldo = r.qtdcompra - r.Entregas,
                    preco = r.preco,
                    total = r.total,
                    recebido = r.Entregas
                })
                .Where(r => r.qtd > 0 || cond == 0)
                .ToList();
            return (result);
        }

        public async Task<IEnumerable<EntregaCompra>> ListarEntregasByProduto(int id, string idconta)
        {
            var condicao = (EntregaCompra m) => (m.idprodutopedido == id && m.idconta == idconta);

            var result = _context.entregascompras.Where(condicao)
                .Select(r => new EntregaCompra
                {
                    id = r.id,
                    idconta = r.idconta,
                    documento = r.documento,
                    idprodutopedido = r.idprodutopedido,
                    dataentrega = r.dataentrega,
                    idpedido = r.idpedido,
                    qtd = r.qtd,
                    conversor = r.conversor,
                    idproduto = r.idproduto,
                    idunidentrega = r.idunidentrega
                })
                .OrderBy(x => x.dataentrega).ToList();
            return (result);
        }

        public async Task<EntregaCompraViewModel> AdicionarEntrega(EntregaCompraViewModel dados)
        {
            //_adicionarProdutoCompraValidator.ValidateAndThrow(dados);
            var EntregaCompra = new EntregaCompra();
            EntregaCompra.idpedido = dados.idpedido;
            EntregaCompra.idprodutopedido = dados.idprodutopedido;
            EntregaCompra.idproduto = dados.idproduto;
            EntregaCompra.documento = dados.documento;
            EntregaCompra.conversor = dados.conversor;
            EntregaCompra.dataentrega = dados.dataentrega;
            EntregaCompra.qtd = dados.qtd;
            EntregaCompra.uid = dados.uid;
            EntregaCompra.idconta = dados.idconta;
            EntregaCompra.datains = dados.datains;
            EntregaCompra.idunidentrega = dados.idunidentrega;

            await _context.AddAsync(EntregaCompra);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog
            {
                uid = dados.uid,
                transacao = "Entrega Pedido compra " + EntregaCompra.idpedido.ToString() + "/" + EntregaCompra.idproduto.ToString() + "/" + EntregaCompra.dataentrega.ToLongDateString() + "/" + EntregaCompra.qtd + "/"
                + EntregaCompra.documento,
                datalog = DateTime.Now,
                idconta = dados.idconta
            });
            await _context.SaveChangesAsync();
            return new EntregaCompraViewModel
            {
                id = EntregaCompra.id,
                idpedido = EntregaCompra.idpedido,
                idproduto = EntregaCompra.idproduto,
                idprodutopedido = EntregaCompra.idprodutopedido,
                documento = EntregaCompra.documento,
                qtd = EntregaCompra.qtd,
                conversor = EntregaCompra.conversor,
                dataentrega = EntregaCompra.dataentrega,
                idunidentrega = EntregaCompra.idunidentrega
            };
        }

        public async Task<EntregaCompraViewModel>? ExcluirEntregaCompraById(int id, string idconta, string uid)
        {
            var ProdutoCompra = _context.entregascompras.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (ProdutoCompra != null)
            {
                ProdutoCompraViewModel dados = new ProdutoCompraViewModel
                {
                    id = ProdutoCompra.id
                };

                _context.entregascompras.Remove(ProdutoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusao  da Entrega " + ProdutoCompra.idpedido.ToString() + "/" + ProdutoCompra.idproduto.ToString() + "/" + ProdutoCompra.documento.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new EntregaCompraViewModel
                {
                    id = ProdutoCompra.id,
                    idconta = ProdutoCompra.idconta,
                    idproduto = ProdutoCompra.idproduto,
                    idpedido = ProdutoCompra.idpedido,
                    uid = ProdutoCompra.uid,
                    datains = ProdutoCompra.datains,
                    dataup = ProdutoCompra.dataup
                };
            }
            else return null;
        }
    }
}