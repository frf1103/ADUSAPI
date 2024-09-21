using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Produto;
using FarmPlannerAPICore.Models.Produto;
using FarmPlannerAPICore.Models.ProdutoPrincipio;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ProdutoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoValidator _adicionarProdutoValidator;
        private readonly ExcluirProdutoValidator _excluirProdutoValidator;
        private readonly ProdutoPrincipioValidator _adicionarProdutoPrincipioValidator;

        public ProdutoService(FarmPlannerContext context, ProdutoValidator adicionarProdutoValidator, ExcluirProdutoValidator excluirProdutoValidator, ProdutoPrincipioValidator adicionarProdutoPrincipioValidator)
        {
            _context = context;
            _adicionarProdutoValidator = adicionarProdutoValidator;
            _excluirProdutoValidator = excluirProdutoValidator;
            _adicionarProdutoPrincipioValidator = adicionarProdutoPrincipioValidator;
        }

        public async Task<ProdutoViewModel> AdicionarProduto(ProdutoViewModel dados)
        {
            _adicionarProdutoValidator.ValidateAndThrow(dados);
            var Produto = new Produto();
            Produto.Descricao = dados.Descricao;
            Produto.IdGrupoProduto = dados.IdGrupoProduto;
            //Produto.IdPrincipioAtivo = dados.IdPrincipioAtivo;
            Produto.idconta = dados.idconta;
            Produto.idunidade = dados.idunidade;
            Produto.IdFabricante = dados.IdFabricante;
            Produto.uid = dados.uid;
            Produto.datains = DateTime.Now;

            await _context.AddAsync(Produto);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão do Produto " + dados.Descricao.Trim(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new ProdutoViewModel
            {
                Descricao = Produto.Descricao,
                Id = Produto.Id,
                IdFabricante = Produto.IdFabricante,
                // IdPrincipioAtivo = Produto.IdPrincipioAtivo,
                idconta = Produto.idconta,
                idunidade = Produto.idunidade,
                IdGrupoProduto = Produto.IdGrupoProduto
            };
        }

        public async Task<ProdutoViewModel>? SalvarProduto(int id, string idconta, string uid, ProdutoViewModel dados)
        {
            var Produto = _context.produtos.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (Produto != null)
            {
                Produto.Descricao = dados.Descricao;
                Produto.IdGrupoProduto = dados.IdGrupoProduto;
                //  Produto.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                Produto.idconta = dados.idconta;
                Produto.idunidade = dados.idunidade;
                Produto.IdFabricante = dados.IdFabricante;
                Produto.dataup = DateTime.Now;

                _context.Update(Produto);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Alteração do Produto " + dados.Descricao.Trim(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new ProdutoViewModel
                {
                    Descricao = Produto.Descricao,
                    Id = Produto.Id,
                    IdFabricante = Produto.IdFabricante,
                    //    IdPrincipioAtivo = Produto.IdPrincipioAtivo,
                    idconta = Produto.idconta,
                    idunidade = Produto.idunidade,
                    IdGrupoProduto = Produto.IdGrupoProduto
                };
            }
            else return null;
        }

        public async Task<ProdutoViewModel>? ExcluirProduto(int id, string idconta, string uid)
        {
            var Produto = _context.produtos.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (Produto != null)
            {
                ProdutoViewModel dados = new ProdutoViewModel
                {
                    Id = Produto.Id
                };
                _excluirProdutoValidator.ValidateAndThrow(dados);
                _context.produtos.Remove(Produto);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão do Produto " + dados.Descricao.Trim(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new ProdutoViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id,
                    IdFabricante = dados.IdFabricante,
                    //   IdPrincipioAtivo = dados.IdPrincipioAtivo,
                    idconta = dados.idconta,
                    idunidade = dados.idunidade,
                    IdGrupoProduto = dados.IdGrupoProduto
                };
            }
            else return null;
        }

        public async Task<ProdutoViewModel>? ListarProdutoById(int id, string idconta)
        {
            var Produto = _context.produtos.Include(p => p.grupoProduto).Where(m => (m.idconta == idconta && m.Id == id)).FirstOrDefault();
            if (Produto != null)
            {
                return new ProdutoViewModel
                {
                    Descricao = Produto.Descricao,
                    Id = Produto.Id,
                    IdFabricante = Produto.IdFabricante,
                    //  IdPrincipioAtivo = Produto.IdPrincipioAtivo,
                    idconta = Produto.idconta,
                    idunidade = Produto.idunidade,
                    IdGrupoProduto = Produto.IdGrupoProduto,
                    tipo = (int)Produto.grupoProduto.Tipo
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListProdutoViewModel>> ListarProduto(string? filtro, string idconta, int idgrupo, int idfab, int idprincipio, int tipo)
        {
            var Produtos = _context.produtos.Where(m => (m.idconta == idconta)
                && (idgrupo == 0 || m.IdGrupoProduto == idgrupo) && (idfab == 0 || m.IdFabricante == idfab)
                && (idprincipio == 0 || m.produtosprincipio.Any(p => p.idproduto == m.Id && p.idprincipio == idprincipio))
                //&& (idprincipio == 0 || m.IdPrincipioAtivo == idprincipio)
                && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Include(c => c.grupoProduto)
                .Include(c => c.parceiro)
                .Include(c => c.unidade)
                .Where(c => (tipo == -1 && c.grupoProduto.Tipo != 5) || c.grupoProduto.Tipo == tipo)
                //.Include(c => c.principioAtivo)
                .Select(c => new ListProdutoViewModel
                {
                    Descricao = c.Descricao,
                    Id = c.Id,
                    IdFabricante = c.IdFabricante,
                    //    IdPrincipioAtivo = c.IdPrincipioAtivo,
                    idconta = c.idconta,
                    unidadeBasica = c.idunidade,
                    IdGrupoProduto = c.IdGrupoProduto,
                    descfab = c.parceiro.Fantasia,
                    //   descprincipio = c.principioAtivo.Descricao,
                    descgrupo = c.grupoProduto.Descricao,
                    descunidade = c.unidade.descricao,
                    tipo = c.grupoProduto.Tipo.ToString()
                }
                ).ToList();
            return (Produtos);
        }

        /* principio dos produtos */

        public async Task<ProdutoPrincipioViewModel> AdicionarProdutoPrincipio(ProdutoPrincipioViewModel dados)
        {
            _adicionarProdutoPrincipioValidator.ValidateAndThrow(dados);
            var Produto = new ProdutoPrincipioAtivo();
            Produto.idproduto = dados.idproduto;
            Produto.idprincipio = dados.idprincipio;
            Produto.idconta = dados.idconta;
            Produto.quantidade = dados.quantidade;
            await _context.AddAsync(Produto);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão do Produto/Principio " + dados.idproduto.ToString().Trim() + "/" + dados.idprincipio.ToString().Trim(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new ProdutoPrincipioViewModel
            {
                idconta = Produto.idconta,
                idprincipio = Produto.idprincipio,
                idproduto = Produto.idproduto,
                quantidade = Produto.quantidade
            };
        }

        public async Task<ProdutoPrincipioViewModel>? SalvarProdutoPrincipio(int idproduto, int idprincipio, string idconta, ProdutoPrincipioViewModel dados)
        {
            var Produto = _context.produtosprincipio.Where(x => x.idconta == idconta && x.idproduto == idproduto && x.idprincipio == idprincipio).FirstOrDefault();

            if (Produto != null)
            {
                Produto.idproduto = dados.idproduto;
                Produto.idprincipio = dados.idprincipio;
                Produto.idconta = dados.idconta;
                Produto.quantidade = dados.quantidade;

                _context.Update(Produto);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração do Produto/Principio " + dados.idproduto.ToString().Trim() + "/" + dados.idprincipio.ToString().Trim(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new ProdutoPrincipioViewModel
                {
                    idconta = Produto.idconta,
                    idprincipio = Produto.idprincipio,
                    idproduto = Produto.idproduto,
                    quantidade = Produto.quantidade
                };
            }
            else return null;
        }

        public async Task<ProdutoPrincipioViewModel>? ExcluirProdutoPrincipio(int idproduto, int idprincipio, string idconta, string uid)
        {
            var Produto = _context.produtosprincipio.Where(x => x.idconta == idconta && x.idproduto == idproduto && x.idprincipio == idprincipio).FirstOrDefault();
            if (Produto != null)
            {
                //                ProdutoPrincipioViewModel dados = new ProdutoPrincipioViewModel
                //                {
                //                    id = Produto.id
                //                };
                //                _excluirProdutoValidator.ValidateAndThrow(dados);
                _context.produtosprincipio.Remove(Produto);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão do Produto/Principio " + idproduto.ToString().Trim() + "/" + idprincipio.ToString().Trim(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new ProdutoPrincipioViewModel
                {
                    idconta = Produto.idconta,
                    idprincipio = Produto.idprincipio,
                    idproduto = Produto.idproduto,
                    quantidade = Produto.quantidade
                };
            }
            else return null;
        }

        public async Task<ProdutoPrincipioViewModel>? ListarProdutoPrincipioById(int idprincipio, int idproduto, string idconta)
        {
            var Produto = _context.produtosprincipio.Where(x => x.idconta == idconta && x.idproduto == idproduto && x.idprincipio == idprincipio).FirstOrDefault();
            if (Produto != null)
            {
                return new ProdutoPrincipioViewModel
                {
                    idconta = Produto.idconta,
                    idprincipio = Produto.idprincipio,
                    idproduto = Produto.idproduto,
                    quantidade = Produto.quantidade
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ProdutoPrincipioViewModel>> ListarProdutoPrincipio(string idconta, int idprincipio, int idproduto)
        {
            var Produtos = _context.produtosprincipio.Where(m => (m.idconta == idconta)
                && (idprincipio == 0 || m.idprincipio == idprincipio) && (idproduto == 0 || m.idproduto == idproduto))
                .Include(p => p.principioAtivo)
                .Include(p => p.produtoproduto)
                //&& (idprincipio == 0 || m.IdPrincipioAtivo == idprincipio)

                //.Include(c => c.principioAtivo)
                .Select(c => new ProdutoPrincipioViewModel
                {
                    idconta = c.idconta,
                    idprincipio = c.idprincipio,
                    idproduto = c.idproduto,
                    quantidade = c.quantidade,
                    descprincipio = c.principioAtivo.Descricao,
                    descproduto = c.produtoproduto.Descricao
                }
                ).ToList();
            return (Produtos);
        }
    }
}