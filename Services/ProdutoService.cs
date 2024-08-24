using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Produto;
using FarmPlannerAPICore.Models.Produto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ProdutoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoValidator _adicionarProdutoValidator;
        private readonly ExcluirProdutoValidator _excluirProdutoValidator;

        public ProdutoService(FarmPlannerContext context, ProdutoValidator adicionarProdutoValidator, ExcluirProdutoValidator excluirProdutoValidator)
        {
            _context = context;
            _adicionarProdutoValidator = adicionarProdutoValidator;
            _excluirProdutoValidator = excluirProdutoValidator;
        }

        public async Task<ProdutoViewModel> AdicionarProduto(ProdutoViewModel dados)
        {
            _adicionarProdutoValidator.ValidateAndThrow(dados);
            var Produto = new Produto();
            Produto.Descricao = dados.Descricao;
            Produto.IdGrupoProduto = dados.IdGrupoProduto;
            Produto.IdPrincipioAtivo = dados.IdPrincipioAtivo;
            Produto.idconta = dados.idconta;
            Produto.unidadeBasica = dados.unidadeBasica;
            Produto.IdFabricante = dados.IdFabricante;

            await _context.AddAsync(Produto);
            await _context.SaveChangesAsync();
            return new ProdutoViewModel
            {
                Descricao = Produto.Descricao,
                Id = Produto.Id,
                IdFabricante = Produto.IdFabricante,
                IdPrincipioAtivo = Produto.IdPrincipioAtivo,
                idconta = Produto.idconta,
                unidadeBasica = Produto.unidadeBasica,
                IdGrupoProduto = Produto.IdGrupoProduto
            };
        }

        public async Task<ProdutoViewModel>? SalvarProduto(int id, string idconta, ProdutoViewModel dados)
        {
            var Produto = _context.produtos.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (Produto != null)
            {
                Produto.Descricao = dados.Descricao;
                Produto.IdGrupoProduto = dados.IdGrupoProduto;
                Produto.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                Produto.idconta = dados.idconta;
                Produto.unidadeBasica = dados.unidadeBasica;
                Produto.IdFabricante = dados.IdFabricante;

                _context.Update(Produto);
                await _context.SaveChangesAsync();
                return new ProdutoViewModel
                {
                    Descricao = Produto.Descricao,
                    Id = Produto.Id,
                    IdFabricante = Produto.IdFabricante,
                    IdPrincipioAtivo = Produto.IdPrincipioAtivo,
                    idconta = Produto.idconta,
                    unidadeBasica = Produto.unidadeBasica,
                    IdGrupoProduto = Produto.IdGrupoProduto
                };
            }
            else return null;
        }

        public async Task<ProdutoViewModel>? ExcluirProduto(int id, string idconta)
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
                await _context.SaveChangesAsync();
                return new ProdutoViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id,
                    IdFabricante = dados.IdFabricante,
                    IdPrincipioAtivo = dados.IdPrincipioAtivo,
                    idconta = dados.idconta,
                    unidadeBasica = dados.unidadeBasica,
                    IdGrupoProduto = dados.IdGrupoProduto
                };
            }
            else return null;
        }

        public async Task<ProdutoViewModel>? ListarProdutoById(int id, string idconta)
        {
            var Produto = _context.produtos.Where(m => (m.idconta == idconta && m.Id == id)).FirstOrDefault();
            if (Produto != null)
            {
                return new ProdutoViewModel
                {
                    Descricao = Produto.Descricao,
                    Id = Produto.Id,
                    IdFabricante = Produto.IdFabricante,
                    IdPrincipioAtivo = Produto.IdPrincipioAtivo,
                    idconta = Produto.idconta,
                    unidadeBasica = Produto.unidadeBasica,
                    IdGrupoProduto = Produto.IdGrupoProduto
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListProdutoViewModel>> ListarProduto(string? filtro, string idconta, int idgrupo, int idfab, int idprincipio)
        {
            var Produtos = _context.produtos.Where(m => (m.idconta == idconta)
                && (idgrupo == 0 || m.IdGrupoProduto == idgrupo) && (idfab == 0 || m.IdFabricante == idfab)
                && (idprincipio == 0 || m.IdPrincipioAtivo == idprincipio)
                && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Include(c => c.grupoProduto)
                .Include(c => c.parceiro)
                .Include(c => c.principioAtivo)
                .Select(c => new ListProdutoViewModel
                {
                    Descricao = c.Descricao,
                    Id = c.Id,
                    IdFabricante = c.IdFabricante,
                    IdPrincipioAtivo = c.IdPrincipioAtivo,
                    idconta = c.idconta,
                    unidadeBasica = c.unidadeBasica,
                    IdGrupoProduto = c.IdGrupoProduto,
                    descfab = c.parceiro.Fantasia,
                    descprincipio = c.principioAtivo.Descricao,
                    descgrupo = c.grupoProduto.Descricao,
                    descunidade = c.unidadeBasica.ToString()
                }
                ).ToList();
            return (Produtos);
        }
    }
}