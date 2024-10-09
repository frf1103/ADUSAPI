using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.ProdutoPlanejado;
using FarmPlannerAPICore.Models.PlanejamentoCompra;
using FarmPlannerAPICore.Models.ProdutoPlanejado;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FarmPlannerAPI.Services
{
    public class ProdutoPlanejadoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoPlanejadoValidator _adicionarProdutoPlanejadoValidator;
        private readonly ExcluirProdutoPlanejadoValidator _excluirProdutoPlanejadoValidator;
        private readonly PlanejamentoCompraService _planejcompra;

        public ProdutoPlanejadoService(FarmPlannerContext context, ProdutoPlanejadoValidator adicionarProdutoPlanejadoValidator, ExcluirProdutoPlanejadoValidator excluirProdutoPlanejadoValidator, PlanejamentoCompraService planejcompra)
        {
            _context = context;
            _adicionarProdutoPlanejadoValidator = adicionarProdutoPlanejadoValidator;
            _excluirProdutoPlanejadoValidator = excluirProdutoPlanejadoValidator;
            _planejcompra = planejcompra;
        }

        public async Task<(ProdutoPlanejadoViewModel, List<string> listerros)> AdicionarProdutoPlanejado(ProdutoPlanejadoViewModel dados, bool commit = true)
        {
            var validationErrors = new List<ValidationResult>();
            _adicionarProdutoPlanejadoValidator.ValidateAndThrow(dados);

            var errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
            if (errorMessages.Count == 0)
            {
                var ProdutoPlanejado = new ProdutoPlanejado();

                ProdutoPlanejado.IdProduto = (dados.IdProduto == 0) ? null : dados.IdProduto;
                //   ProdutoPlanejado.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                ProdutoPlanejado.Dosagem = dados.Dosagem;

                ProdutoPlanejado.Tamanho = dados.Tamanho;
                ProdutoPlanejado.TotalProduto = dados.TotalProduto;
                ProdutoPlanejado.IdPlanejamento = dados.IdPlanejamento;
                ProdutoPlanejado.idconta = dados.idconta;
                ProdutoPlanejado.uid = dados.uid;
                ProdutoPlanejado.datains = DateTime.Now;

                await _context.AddAsync(ProdutoPlanejado);
                if (commit)
                {
                    var planej = _context.planejoperacoes.Where(p => p.Id == dados.IdPlanejamento).Include(p => p.configArea).Include(p => p.configArea.talhao).FirstOrDefault();
                    var p = _context.produtoplanejados.Where(p => p.IdProduto == dados.IdProduto &&
                                                                p.planejamentoOperacao.configArea.IdSafra == planej.configArea.IdSafra &&
                                                                p.planejamentoOperacao.configArea.talhao.IdFazenda == planej.configArea.talhao.IdFazenda).ToList();
                    decimal qtdplanej = p.Sum(p => p.TotalProduto);

                    PlanejamentoCompraViewModel pp = new PlanejamentoCompraViewModel
                    {
                        idconta = dados.idconta,
                        IdFazenda = planej.configArea.talhao.IdFazenda,
                        idproduto = (int)dados.IdProduto,
                        IdSafra = planej.configArea.IdSafra,
                        QtdNecessaria = qtdplanej + dados.TotalProduto,
                        QtdComprada = 0,
                        QtdComprar = 0,
                        QtdEstoque = 0,
                        Saldo = 0,
                        uid = dados.uid
                    };
                    var compra = _context.planejamentoCompras.Where(p => p.idconta == dados.idconta && p.IdFazenda == planej.configArea.talhao.IdFazenda && p.IdSafra == planej.configArea.IdSafra && p.idproduto == dados.IdProduto).FirstOrDefault();
                    if (compra == null)
                    {
                        _planejcompra.AdicionarPlanejamentoCompra(pp, false);
                    }
                    else
                    {
                        pp.QtdComprar = compra.QtdComprar;
                        pp.Saldo = compra.Saldo;
                        pp.QtdEstoque = compra.QtdEstoque;
                        pp.QtdNecessaria = qtdplanej;
                        pp.QtdComprada = compra.QtdComprada;
                        pp.IdFazenda = compra.IdFazenda;
                        pp.IdSafra = compra.IdSafra;
                        pp.idconta = compra.idconta;
                        pp.Id = compra.Id;
                        pp.uid = compra.uid;
                        _planejcompra.SalvarPlanejamentoCompra(compra.Id, dados.idconta, pp, false);
                    };
                }
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  produto do Planejamento de operacoes " + ProdutoPlanejado.IdPlanejamento.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return (new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    //         IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                }, null);
            }
            else
            {
                errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
                return (null, errorMessages);
            }
        }

        public async Task<ProdutoPlanejadoViewModel>? SalvarProdutoPlanejado(int id, string idconta, ProdutoPlanejadoViewModel dados, bool commit = true)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (ProdutoPlanejado != null)
            {
                ProdutoPlanejado.IdProduto = (dados.IdProduto == 0) ? null : dados.IdProduto;
                //      ProdutoPlanejado.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                ProdutoPlanejado.Dosagem = dados.Dosagem;

                ProdutoPlanejado.Tamanho = dados.Tamanho;
                ProdutoPlanejado.TotalProduto = dados.TotalProduto;
                ProdutoPlanejado.IdPlanejamento = dados.IdPlanejamento;
                ProdutoPlanejado.uid = dados.uid;
                ProdutoPlanejado.dataup = DateTime.Now;

                _context.Update(ProdutoPlanejado);
                await _context.SaveChangesAsync();
                var planej = _context.planejoperacoes.Where(p => p.Id == dados.IdPlanejamento).Include(p => p.configArea).Include(p => p.configArea.talhao).FirstOrDefault();
                decimal qtdplanej = _context.produtoplanejados.Where(p => p.IdProduto == dados.IdProduto &&
                                                            p.planejamentoOperacao.configArea.IdSafra == planej.configArea.IdSafra &&
                                                            p.planejamentoOperacao.configArea.talhao.IdFazenda == planej.configArea.talhao.IdFazenda).Sum(p => p.TotalProduto);

                var compra = _context.planejamentoCompras.Where(p => p.idconta == dados.idconta && p.IdFazenda == planej.configArea.talhao.IdFazenda && p.IdSafra == planej.configArea.IdSafra && p.idproduto == dados.IdProduto).FirstOrDefault();

                PlanejamentoCompraViewModel pp = new PlanejamentoCompraViewModel
                {
                    QtdComprar = compra.QtdComprar,
                    Saldo = compra.Saldo,
                    QtdEstoque = compra.QtdEstoque,
                    QtdNecessaria = qtdplanej,
                    QtdComprada = compra.QtdComprada,
                    IdFazenda = compra.IdFazenda,
                    IdSafra = compra.IdSafra,
                    idconta = compra.idconta,
                    Id = compra.Id,
                    uid = compra.uid,
                    idproduto = compra.idproduto ?? 0
                };
                _planejcompra.SalvarPlanejamentoCompra(compra.Id, dados.idconta, pp, false);

                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Altereção do  produto do Planejamento de operacoes " + ProdutoPlanejado.IdPlanejamento.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                if (commit)
                {
                    await _context.SaveChangesAsync();
                }
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    //   IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,

                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<ProdutoPlanejadoViewModel>? ExcluirProdutoPlanejado(int id, string uid, string idconta, bool commit = true)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (ProdutoPlanejado != null)
            {
                ProdutoPlanejadoViewModel dados = new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto=ProdutoPlanejado.IdProduto
                };
                _excluirProdutoPlanejadoValidator.ValidateAndThrow(dados);

                _context.produtoplanejados.Remove(ProdutoPlanejado);
                if (commit)
                {
                    await _context.SaveChangesAsync();
                }
                var planej = _context.planejoperacoes.Where(p => p.Id == dados.IdPlanejamento).Include(p => p.configArea).Include(p => p.configArea.talhao).FirstOrDefault();
                decimal qtdplanej = _context.produtoplanejados.Where(p => p.IdProduto == dados.IdProduto &&
                                                            p.planejamentoOperacao.configArea.IdSafra == planej.configArea.IdSafra &&
                                                            p.planejamentoOperacao.configArea.talhao.IdFazenda == planej.configArea.talhao.IdFazenda).Sum(p => p.TotalProduto);

                var compra = _context.planejamentoCompras.Where(p => p.idconta == dados.idconta && p.IdFazenda == planej.configArea.talhao.IdFazenda && p.IdSafra == planej.configArea.IdSafra && p.idproduto == dados.IdProduto).FirstOrDefault();

                PlanejamentoCompraViewModel pp = new PlanejamentoCompraViewModel
                {
                    QtdComprar = compra.QtdComprar,
                    Saldo = compra.Saldo,
                    QtdEstoque = compra.QtdEstoque,
                    QtdNecessaria = qtdplanej,
                    QtdComprada = compra.QtdComprada,
                    IdFazenda = compra.IdFazenda,
                    IdSafra = compra.IdSafra,
                    idconta = compra.idconta,
                    Id = compra.Id,
                    uid = compra.uid,
                    idproduto = compra.idproduto ?? 0
                };

                _planejcompra.SalvarPlanejamentoCompra(compra.Id, dados.idconta, pp, false);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exlusão do  produto do Planejamento de operacoes " + ProdutoPlanejado.IdPlanejamento.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString(), datalog = DateTime.Now, idconta = idconta });

                await _context.SaveChangesAsync();
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    //IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<ProdutoPlanejadoViewModel>? ListarProdutoPlanejadoById(int id, string idconta)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (ProdutoPlanejado != null)
            {
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    //    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListProdutoPlanejadoViewModel>> ListarProdutoPlanejadoByPlanejamento(int idplanejamento, string idconta)
        {
            var query = _context.produtoplanejados
                .Include(m => m.produto)
                .Where(m => (m.IdPlanejamento == idplanejamento && m.idconta == idconta));
            var ProdutoPlanejados = query
                .Select(c => new ListProdutoPlanejadoViewModel
                {
                    Id = c.Id,
                    IdProduto = c.IdProduto,
                    //       IdPrincipioAtivo = c.IdPrincipioAtivo,
                    Dosagem = c.Dosagem,

                    Tamanho = c.Tamanho,
                    TotalProduto = c.TotalProduto,
                    IdPlanejamento = c.IdPlanejamento,
                    //   descprincativo = c.principioativo.Descricao,
                    descproduto = c.produto.Descricao ?? "Sem Produto",
                    descricao = (c.IdProduto == null) ? " " : c.produto.Descricao,
                    idcodigo = (int)((c.IdProduto == null) ? 0 : c.IdProduto),
                    idtipo = (c.IdProduto == null) ? "PA" : "PR"
                }
                ).ToList();
            return (ProdutoPlanejados);
        }
    }
}