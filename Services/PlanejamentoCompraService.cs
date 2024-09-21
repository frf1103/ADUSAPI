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

        public async Task<PlanejamentoCompraViewModel> AdicionarPlanejamentoCompra(PlanejamentoCompraViewModel dados, bool commit = true)
        {
            _adicionarPlanejamentoCompraValidator.ValidateAndThrow(dados);
            var PlanejamentoCompra = new PlanejamentoCompra();

            PlanejamentoCompra.IdPrincipio = dados.IdPrincipio;
            PlanejamentoCompra.IdFazenda = dados.IdFazenda;
            PlanejamentoCompra.IdSafra = dados.IdSafra;
            PlanejamentoCompra.QtdComprada = dados.QtdComprada;
            PlanejamentoCompra.QtdComprar = dados.QtdNecessaria - dados.QtdEstoque;
            PlanejamentoCompra.QtdEstoque = dados.QtdEstoque;
            PlanejamentoCompra.QtdNecessaria = dados.QtdNecessaria;
            PlanejamentoCompra.idconta = dados.idconta;
            PlanejamentoCompra.Saldo = PlanejamentoCompra.QtdComprar - PlanejamentoCompra.QtdComprada;
            PlanejamentoCompra.uid = dados.uid;
            PlanejamentoCompra.datains = DateTime.Now;

            await _context.AddAsync(PlanejamentoCompra);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Planejamento de compras " + PlanejamentoCompra.Id.ToString() + "/" + PlanejamentoCompra.IdFazenda.ToString() + "/" + PlanejamentoCompra.IdSafra.ToString() + "/" + PlanejamentoCompra.IdPrincipio.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
            if (commit)
            {
                await _context.SaveChangesAsync();
            }
            return new PlanejamentoCompraViewModel
            {
                Id = PlanejamentoCompra.Id,
                IdPrincipio = PlanejamentoCompra.IdPrincipio,
                IdSafra = PlanejamentoCompra.IdSafra,
                QtdComprada = PlanejamentoCompra.QtdComprada,
                QtdComprar = PlanejamentoCompra.QtdComprar,
                QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                QtdEstoque = PlanejamentoCompra.QtdEstoque,
                Saldo = PlanejamentoCompra.Saldo,
                IdFazenda = PlanejamentoCompra.IdFazenda
            };
        }

        public async Task<PlanejamentoCompraViewModel>? SalvarPlanejamentoCompra(int id, string idconta, PlanejamentoCompraViewModel dados, bool commit = true)
        {
            var PlanejamentoCompra = _context.planejamentoCompras.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (PlanejamentoCompra != null)
            {
                PlanejamentoCompra.IdFazenda = dados.IdFazenda;
                PlanejamentoCompra.IdPrincipio = dados.IdPrincipio;
                PlanejamentoCompra.IdSafra = dados.IdSafra;

                PlanejamentoCompra.QtdComprada = dados.QtdComprada;
                PlanejamentoCompra.QtdComprar = dados.QtdNecessaria - dados.QtdEstoque;
                PlanejamentoCompra.QtdEstoque = dados.QtdEstoque;
                PlanejamentoCompra.QtdNecessaria = dados.QtdNecessaria;
                PlanejamentoCompra.Saldo = PlanejamentoCompra.QtdComprar - PlanejamentoCompra.QtdComprada;
                PlanejamentoCompra.uid = dados.uid;
                PlanejamentoCompra.dataup = DateTime.Now;

                _context.Update(PlanejamentoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Planejamento de compras " + PlanejamentoCompra.Id.ToString() + "/" + PlanejamentoCompra.IdFazenda.ToString() + "/" + PlanejamentoCompra.IdSafra.ToString() + "/" + PlanejamentoCompra.IdPrincipio.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                if (commit)
                {
                    await _context.SaveChangesAsync();
                }
                return new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id,
                    IdPrincipio = PlanejamentoCompra.IdPrincipio,
                    IdSafra = PlanejamentoCompra.IdSafra,
                    QtdComprada = PlanejamentoCompra.QtdComprada,
                    QtdComprar = PlanejamentoCompra.QtdComprar,
                    QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                    QtdEstoque = PlanejamentoCompra.QtdEstoque,
                    Saldo = PlanejamentoCompra.Saldo,
                    IdFazenda = PlanejamentoCompra.IdFazenda
                };
            }
            else return null;
        }

        public async Task<PlanejamentoCompraViewModel>? ExcluirPlanejamentoCompra(int id, string idconta, string uid, bool commit = true)
        {
            var PlanejamentoCompra = _context.planejamentoCompras.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (PlanejamentoCompra != null)
            {
                PlanejamentoCompraViewModel dados = new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id
                };
                _excluirPlanejamentoCompraValidator.ValidateAndThrow(dados);
                _context.planejamentoCompras.Remove(PlanejamentoCompra);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Planejamento de compras " + PlanejamentoCompra.Id.ToString() + "/" + PlanejamentoCompra.IdFazenda.ToString() + "/" + PlanejamentoCompra.IdSafra.ToString() + "/" + PlanejamentoCompra.IdPrincipio.ToString(), datalog = DateTime.Now, idconta = idconta });
                if (commit)
                {
                    await _context.SaveChangesAsync();
                }
                return new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id,
                    IdPrincipio = PlanejamentoCompra.IdPrincipio,
                    IdSafra = PlanejamentoCompra.IdSafra,
                    QtdComprada = PlanejamentoCompra.QtdComprada,
                    QtdComprar = PlanejamentoCompra.QtdComprar,
                    QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                    QtdEstoque = PlanejamentoCompra.QtdEstoque,
                    Saldo = PlanejamentoCompra.Saldo,
                    IdFazenda = PlanejamentoCompra.IdFazenda
                };
            }
            else return null;
        }

        public async Task<PlanejamentoCompraViewModel>? ListarPlanejamentoCompraById(int id, string idconta)
        {
            var PlanejamentoCompra = _context.planejamentoCompras.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (PlanejamentoCompra != null)
            {
                return new PlanejamentoCompraViewModel
                {
                    Id = PlanejamentoCompra.Id,
                    IdPrincipio = PlanejamentoCompra.IdPrincipio,
                    IdSafra = PlanejamentoCompra.IdSafra,
                    QtdComprada = PlanejamentoCompra.QtdComprada,
                    QtdComprar = PlanejamentoCompra.QtdComprar,
                    QtdNecessaria = PlanejamentoCompra.QtdNecessaria,
                    QtdEstoque = PlanejamentoCompra.QtdEstoque,
                    Saldo = PlanejamentoCompra.Saldo,
                    IdFazenda = PlanejamentoCompra.IdFazenda
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListPlanejamentoCompraViewModel>> ListarPlanejamento(string idconta, int idano, int idorganizacao, int idprincipio, int idsafra, int idfazenda)
        {
            var query = _context.planejamentoCompras
                .Include(m => m.principio).Include(m => m.safra).Include(m => m.safra.anoAgricola)
                .Include(m => m.safra.anoAgricola.organizacao)
                .Include(m => m.safra.anoAgricola.organizacao.conta)
                .Where((m => (m.safra.anoAgricola.organizacao.idconta == idconta) &&
                (m.safra.IdAnoAgricola == idano) &&
                (idprincipio == 0 || m.IdPrincipio == idprincipio) &&
                (idsafra == 0 || m.IdSafra == idsafra) &&
                (m.safra.anoAgricola.IdOrganizacao == idorganizacao) &&
                (idfazenda == 0 || m.IdFazenda == idfazenda)));

            var PlanejamentoCompras = query
                .Select(c => new ListPlanejamentoCompraViewModel
                {
                    Id = c.Id,
                    IdPrincipio = c.IdPrincipio,
                    IdSafra = c.IdSafra,
                    QtdComprada = c.QtdComprada,
                    QtdComprar = c.QtdComprar,
                    QtdNecessaria = c.QtdNecessaria,
                    QtdEstoque = c.QtdEstoque,
                    Saldo = c.Saldo,
                    DescPrincipio = c.principio.Descricao,
                    DescSafra = c.safra.Descricao,
                    DescFazenda = c.fazenda.Descricao,
                    IdFazenda = c.IdFazenda
                }
                ).ToList();
            return (PlanejamentoCompras);
        }
    }
}