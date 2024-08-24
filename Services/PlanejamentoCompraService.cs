using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.PlanejamentoCompra;
using FarmPlannerAPICore.Models.PlanejamentoCompra;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class PlanejamentoCompraService
    {
        private readonly FarmPlannerContext _context;
        private readonly PlanejamentoCompraValidator _adicionarPlanejamentoCompraValidator;
        private readonly ExcluirPlanejamentoCompraValidator _excluirPlanejamentoCompraValidator;

        public PlanejamentoCompraService(FarmPlannerContext context, PlanejamentoCompraValidator adicionarPlanejamentoCompraValidator, ExcluirPlanejamentoCompraValidator excluirPlanejamentoCompraValidator)
        {
            _context = context;
            _adicionarPlanejamentoCompraValidator = adicionarPlanejamentoCompraValidator;
            _excluirPlanejamentoCompraValidator = excluirPlanejamentoCompraValidator;
        }

        public async Task<PlanejamentoCompraViewModel> AdicionarPlanejamentoCompra(PlanejamentoCompraViewModel dados)
        {
            _adicionarPlanejamentoCompraValidator.ValidateAndThrow(dados);
            var PlanejamentoCompra = new PlanejamentoCompra();

            PlanejamentoCompra.IdProduto = dados.IdProduto;
            PlanejamentoCompra.IdSafra = dados.IdSafra;
            PlanejamentoCompra.Saldo = dados.Saldo;
            PlanejamentoCompra.QtdComprada = dados.QtdComprada;
            PlanejamentoCompra.QtdComprar = dados.QtdComprar;
            PlanejamentoCompra.QtdEstoque = dados.QtdEstoque;
            PlanejamentoCompra.QtdNecessaria = dados.QtdNecessaria;
            PlanejamentoCompra.idconta = dados.idconta;

            await _context.AddAsync(PlanejamentoCompra);
            await _context.SaveChangesAsync();
            return new PlanejamentoCompraViewModel
            {
                Id = PlanejamentoCompra.Id,
                IdProduto = PlanejamentoCompra.IdProduto,
                IdSafra = PlanejamentoCompra.IdSafra,
                QtdComprada = PlanejamentoCompra.QtdComprada,
                QtdComprar = PlanejamentoCompra.QtdComprar,
                QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                QtdEstoque = PlanejamentoCompra.QtdEstoque,
                Saldo = PlanejamentoCompra.Saldo
            };
        }

        public async Task<PlanejamentoCompraViewModel>? SalvarPlanejamentoCompra(int id, PlanejamentoCompraViewModel dados)
        {
            var PlanejamentoCompra = _context.planejamentoCompras.Find(id);
            if (PlanejamentoCompra != null)
            {
                PlanejamentoCompra.IdProduto = dados.IdProduto;
                PlanejamentoCompra.IdSafra = dados.IdSafra;
                PlanejamentoCompra.Saldo = dados.Saldo;
                PlanejamentoCompra.QtdComprada = dados.QtdComprada;
                PlanejamentoCompra.QtdComprar = dados.QtdComprar;
                PlanejamentoCompra.QtdEstoque = dados.QtdEstoque;
                PlanejamentoCompra.QtdNecessaria = dados.QtdNecessaria;

                _context.Update(PlanejamentoCompra);
                await _context.SaveChangesAsync();
                return new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id,
                    IdProduto = PlanejamentoCompra.IdProduto,
                    IdSafra = PlanejamentoCompra.IdSafra,
                    QtdComprada = PlanejamentoCompra.QtdComprada,
                    QtdComprar = PlanejamentoCompra.QtdComprar,
                    QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                    QtdEstoque = PlanejamentoCompra.QtdEstoque,
                    Saldo = PlanejamentoCompra.Saldo
                };
            }
            else return null;
        }

        public async Task<PlanejamentoCompraViewModel>? ExcluirPlanejamentoCompra(int id, PlanejamentoCompraViewModel dados)
        {
            _excluirPlanejamentoCompraValidator.ValidateAndThrow(dados);
            var PlanejamentoCompra = _context.planejamentoCompras.Find(id);
            if (PlanejamentoCompra != null)
            {
                _context.planejamentoCompras.Remove(PlanejamentoCompra);
                await _context.SaveChangesAsync();
                return new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id,
                    IdProduto = PlanejamentoCompra.IdProduto,
                    IdSafra = PlanejamentoCompra.IdSafra,
                    QtdComprada = PlanejamentoCompra.QtdComprada,
                    QtdComprar = PlanejamentoCompra.QtdComprar,
                    QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                    QtdEstoque = PlanejamentoCompra.QtdEstoque,
                    Saldo = PlanejamentoCompra.Saldo
                };
            }
            else return null;
        }

        public async Task<PlanejamentoCompraViewModel>? ListarPlanejamentoCompraById(int id)
        {
            var PlanejamentoCompra = _context.planejamentoCompras.Find(id);
            if (PlanejamentoCompra != null)
            {
                return new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id,
                    IdProduto = PlanejamentoCompra.IdProduto,
                    IdSafra = PlanejamentoCompra.IdSafra,
                    QtdComprada = PlanejamentoCompra.QtdComprada,
                    QtdComprar = PlanejamentoCompra.QtdComprar,
                    QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                    QtdEstoque = PlanejamentoCompra.QtdEstoque,
                    Saldo = PlanejamentoCompra.Saldo
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListPlanejamentoCompraViewModel>> ListarPlanejamento(string idconta, int idano, int idorganizacao, int idproduto, int idsafra)
        {
            var query = _context.planejamentoCompras
                .Include(m => m.produto).Include(m => m.safra).Include(m => m.safra.anoAgricola)
                .Include(m => m.safra.anoAgricola.organizacao)
                .Include(m => m.safra.anoAgricola.organizacao.conta)
                .Where((m => (m.safra.anoAgricola.organizacao.idconta == idconta) &&
                (m.safra.IdAnoAgricola == idano) &&
                (idproduto == 0 || m.IdProduto == idproduto) &&
                (idsafra == 0 || m.IdSafra == idsafra) &&
                (m.safra.anoAgricola.IdOrganizacao == idorganizacao)));

            var PlanejamentoCompras = query
                .Select(c => new ListPlanejamentoCompraViewModel
                {
                    Id = c.Id,
                    IdProduto = c.IdProduto,
                    IdSafra = c.IdSafra,
                    QtdComprada = c.QtdComprada,
                    QtdComprar = c.QtdComprar,
                    QtdNecessaria = c.QtdNecessaria,
                    QtdEstoque = c.QtdEstoque,
                    Saldo = c.Saldo,
                    DescProduto = c.produto.Descricao,
                    DescSafra = c.safra.Descricao
                }
                ).ToList();
            return (PlanejamentoCompras);
        }
    }
}