using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPICore.Models.PedidoCompra;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static FarmPlannerAPI.Validators.EntregaCompra.EntregaCompraValidator;

namespace FarmPlannerAPI.Services
{
    public class EntregaCompraService
    {
        private readonly FarmPlannerContext _context;
        private readonly AddEntregaCompraValidator _adicionarEntregaCompraValidator;
        private readonly ExcluirEntregaCompraValidator _excluirEntregaCompraValidator;

        public EntregaCompraService(FarmPlannerContext context, AddEntregaCompraValidator adicionarEntregaCompraValidator, ExcluirEntregaCompraValidator excluirEntregaCompraValidator)
        {
            _context = context;
            _adicionarEntregaCompraValidator = adicionarEntregaCompraValidator;
            _excluirEntregaCompraValidator = excluirEntregaCompraValidator;
        }

        public async Task<EntregaCompraViewModel> AdicionarEntregaCompra(EntregaCompraViewModel dados)
        {
            _adicionarEntregaCompraValidator.ValidateAndThrow(dados);
            var EntregaCompra = new EntregaCompra();

            EntregaCompra.idpedido = dados.idpedido;
            EntregaCompra.idproduto = dados.idproduto;

            EntregaCompra.id = dados.id;
            EntregaCompra.idconta = dados.idconta;
            EntregaCompra.uid = dados.uid;
            EntregaCompra.datains = DateTime.Now;
            EntregaCompra.idprodutopedido = dados.idprodutopedido;
            EntregaCompra.documento = dados.documento;
            EntregaCompra.conversor = dados.conversor;
            EntregaCompra.dataentrega = dados.dataentrega;
            EntregaCompra.idunidentrega = dados.idunidentrega;
            EntregaCompra.qtd = dados.qtd;

            await _context.AddAsync(EntregaCompra);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Entrega de compra " + EntregaCompra.id.ToString() + "/" + EntregaCompra.idpedido.ToString() + "/" + EntregaCompra.idproduto.ToString() + "/" + EntregaCompra.documento, datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new EntregaCompraViewModel
            {
                id = EntregaCompra.id,
                idconta = EntregaCompra.idconta,
                idproduto = EntregaCompra.idproduto,
                idpedido = EntregaCompra.idpedido,
                idunidentrega = EntregaCompra.idunidentrega,
                dataentrega = EntregaCompra.dataentrega,
                conversor = EntregaCompra.conversor,
                documento = EntregaCompra.documento,
                idprodutopedido = EntregaCompra.idprodutopedido,
                qtd = EntregaCompra.qtd,
                uid = EntregaCompra.uid,
                datains = EntregaCompra.datains,
                dataup = EntregaCompra.dataup
            };
        }

        public async Task<EntregaCompraViewModel>? SalvarEntregaCompra(int id, string idconta, EntregaCompraViewModel dados)
        {
            var EntregaCompra = _context.entregascompras.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (EntregaCompra != null)
            {
                EntregaCompra.idpedido = dados.idpedido;
                EntregaCompra.idproduto = dados.idproduto;

                EntregaCompra.id = dados.id;
                EntregaCompra.idconta = dados.idconta;
                EntregaCompra.uid = dados.uid;
                EntregaCompra.dataup = DateTime.Now;
                EntregaCompra.idprodutopedido = dados.idprodutopedido;
                EntregaCompra.documento = dados.documento;
                EntregaCompra.conversor = dados.conversor;
                EntregaCompra.dataentrega = dados.dataentrega;
                EntregaCompra.idunidentrega = dados.idunidentrega;
                EntregaCompra.qtd = dados.qtd;

                _context.Update(EntregaCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteraçao  Entrega  compra " + EntregaCompra.id.ToString() + "/" + EntregaCompra.idpedido.ToString() + "/" + EntregaCompra.idproduto.ToString() + "/" + EntregaCompra.documento, datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new EntregaCompraViewModel
                {
                    id = EntregaCompra.id,
                    idconta = EntregaCompra.idconta,
                    idproduto = EntregaCompra.idproduto,
                    idpedido = EntregaCompra.idpedido,
                    idunidentrega = EntregaCompra.idunidentrega,
                    dataentrega = EntregaCompra.dataentrega,
                    conversor = EntregaCompra.conversor,
                    documento = EntregaCompra.documento,
                    idprodutopedido = EntregaCompra.idprodutopedido,
                    qtd = EntregaCompra.qtd,
                    uid = EntregaCompra.uid,
                    datains = EntregaCompra.datains,
                    dataup = EntregaCompra.dataup
                };
            }
            else return null;
        }

        public async Task<EntregaCompraViewModel>? ExcluirEntregaCompra(int id, string idconta, string uid)
        {
            var EntregaCompra = _context.entregascompras.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (EntregaCompra != null)
            {
                EntregaCompraViewModel dados = new EntregaCompraViewModel
                {
                    id = EntregaCompra.id
                };
                _excluirEntregaCompraValidator.ValidateAndThrow(dados);
                _context.entregascompras.Remove(EntregaCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusao  Entrega de compra " + EntregaCompra.id.ToString() + "/" + EntregaCompra.idpedido.ToString() + "/" + EntregaCompra.idproduto.ToString() + "/" + EntregaCompra.documento, datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new EntregaCompraViewModel
                {
                    id = EntregaCompra.id,
                    idconta = EntregaCompra.idconta,
                    idproduto = EntregaCompra.idproduto,
                    idpedido = EntregaCompra.idpedido,
                    idunidentrega = EntregaCompra.idunidentrega,
                    dataentrega = EntregaCompra.dataentrega,
                    conversor = EntregaCompra.conversor,
                    documento = EntregaCompra.documento,
                    idprodutopedido = EntregaCompra.idprodutopedido,
                    qtd = EntregaCompra.qtd,
                    uid = EntregaCompra.uid,
                    datains = EntregaCompra.datains,
                    dataup = EntregaCompra.dataup
                };
            }
            else return null;
        }

        public async Task<EntregaCompraViewModel>? ListarEntregaCompraById(int id, string idconta)
        {
            var EntregaCompra = _context.entregascompras.Where(o => o.idconta == idconta && o.id == id).FirstOrDefault();
            if (EntregaCompra != null)
            {
                return new EntregaCompraViewModel
                {
                    id = EntregaCompra.id,
                    idconta = EntregaCompra.idconta,
                    idproduto = EntregaCompra.idproduto,
                    idpedido = EntregaCompra.idpedido,
                    idunidentrega = EntregaCompra.idunidentrega,
                    dataentrega = EntregaCompra.dataentrega,
                    conversor = EntregaCompra.conversor,
                    documento = EntregaCompra.documento,
                    idprodutopedido = EntregaCompra.idprodutopedido,
                    qtd = EntregaCompra.qtd,
                    uid = EntregaCompra.uid,
                    datains = EntregaCompra.datains,
                    dataup = EntregaCompra.dataup
                };
            }
            else return null;
        }

        public async Task<IEnumerable<EntregaCompraViewModel>> ListarEntregaCompraByPedido(int idpedido, string idconta)
        {
            var condicao = (EntregaCompra m) => (m.idpedido == idpedido && m.idconta == idconta);
            var query = _context.entregascompras.Include(m => m.produtoCompra).Include(m => m.produto);

            var EntregaCompras = query.Where(condicao)
                .Select(c => new EntregaCompraViewModel
                {
                    id = c.id,
                    idconta = c.idconta,
                    idproduto = c.idproduto,
                    idpedido = c.idpedido,
                    idunidentrega = c.idunidentrega,
                    dataentrega = c.dataentrega,
                    conversor = c.conversor,
                    documento = c.documento,
                    idprodutopedido = c.idprodutopedido,
                    qtd = c.qtd,
                    uid = c.uid,
                    datains = c.datains,
                    dataup = c.dataup
                }
                ).ToList();
            return (EntregaCompras);
        }
    }
}