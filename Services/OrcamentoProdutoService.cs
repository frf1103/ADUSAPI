using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.OrcamentoProduto;
using FarmPlannerAPICore.Models.OrcamentoProduto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class OrcamentoProdutoService
    {
        private readonly FarmPlannerContext _context;
        private readonly OrcamentoProdutoValidator _adicionarOrcamentoProdutoValidator;
        private readonly ExcluirOrcamentoProdutoValidator _excluirOrcamentoProdutoValidator;

        public OrcamentoProdutoService(FarmPlannerContext context, OrcamentoProdutoValidator adicionarOrcamentoProdutoValidator, ExcluirOrcamentoProdutoValidator excluirOrcamentoProdutoValidator)
        {
            _context = context;
            _adicionarOrcamentoProdutoValidator = adicionarOrcamentoProdutoValidator;
            _excluirOrcamentoProdutoValidator = excluirOrcamentoProdutoValidator;
        }

        public async Task<OrcamentoProdutoViewModel> AdicionarOrcamentoProduto(OrcamentoProdutoViewModel dados)
        {
            _adicionarOrcamentoProdutoValidator.ValidateAndThrow(dados);
            var OrcamentoProduto = new OrcamentoProduto();
            OrcamentoProduto.Descricao = dados.Descricao;
            OrcamentoProduto.IdSafra = dados.IdSafra;
            OrcamentoProduto.IdFazenda = dados.IdFazenda;
            OrcamentoProduto.idconta = dados.idconta;
            OrcamentoProduto.uid = dados.uid;
            OrcamentoProduto.datains = DateTime.Now;

            await _context.AddAsync(OrcamentoProduto);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Orçamento hectarizado " + OrcamentoProduto.Id.ToString() + "/" + OrcamentoProduto.IdSafra.ToString() + "/" + OrcamentoProduto.IdFazenda.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new OrcamentoProdutoViewModel
            {
                Descricao = OrcamentoProduto.Descricao,

                Id = OrcamentoProduto.Id,
                IdFazenda = OrcamentoProduto.IdFazenda,
                IdSafra = OrcamentoProduto.IdSafra
            };
        }

        public async Task<OrcamentoProdutoViewModel>? SalvarOrcamentoProduto(int id, string idconta, OrcamentoProdutoViewModel dados)
        {
            var OrcamentoProduto = _context.orcamentosproduto.Where(o => o.idconta == idconta && o.Id == id).FirstOrDefault();
            if (OrcamentoProduto != null)
            {
                OrcamentoProduto.Descricao = dados.Descricao;
                OrcamentoProduto.IdSafra = dados.IdSafra;
                OrcamentoProduto.IdFazenda = dados.IdFazenda;
                OrcamentoProduto.uid = dados.uid;
                OrcamentoProduto.dataup = DateTime.Now;

                _context.Update(OrcamentoProduto);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Orçamento hectarizado " + OrcamentoProduto.Id.ToString() + "/" + OrcamentoProduto.IdSafra.ToString() + "/" + OrcamentoProduto.IdFazenda.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new OrcamentoProdutoViewModel
                {
                    Descricao = OrcamentoProduto.Descricao,

                    Id = OrcamentoProduto.Id,
                    IdFazenda = OrcamentoProduto.IdFazenda,
                    IdSafra = OrcamentoProduto.IdSafra
                };
            }
            else return null;
        }

        public async Task<OrcamentoProdutoViewModel>? ExcluirOrcamentoProduto(int id, string idconta, string uid)
        {
            var OrcamentoProduto = _context.orcamentosproduto.Where(o => o.idconta == idconta && o.Id == id).FirstOrDefault();
            if (OrcamentoProduto != null)
            {
                OrcamentoProdutoViewModel dados = new OrcamentoProdutoViewModel
                {
                    Id = OrcamentoProduto.Id
                };
                _excluirOrcamentoProdutoValidator.ValidateAndThrow(dados);
                _context.orcamentosproduto.Remove(OrcamentoProduto);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Orçamento hectarizado " + OrcamentoProduto.Id.ToString() + "/" + OrcamentoProduto.IdSafra.ToString() + "/" + OrcamentoProduto.IdFazenda.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new OrcamentoProdutoViewModel
                {
                    Descricao = OrcamentoProduto.Descricao,

                    Id = OrcamentoProduto.Id,
                    IdFazenda = OrcamentoProduto.IdFazenda,
                    IdSafra = OrcamentoProduto.IdSafra
                };
            }
            else return null;
        }

        public async Task<OrcamentoProdutoViewModel>? ListarOrcamentoProdutoById(int id, string idconta)
        {
            var OrcamentoProduto = _context.orcamentosproduto.Where(o => o.idconta == idconta && o.Id == id).FirstOrDefault();
            if (OrcamentoProduto != null)
            {
                return new OrcamentoProdutoViewModel
                {
                    Descricao = OrcamentoProduto.Descricao,

                    Id = OrcamentoProduto.Id,
                    IdFazenda = OrcamentoProduto.IdFazenda,
                    IdSafra = OrcamentoProduto.IdSafra
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListOrcamentoProdutoViewModel>> ListarOrcamentoProduto(int idfazenda, int idsafra, string idconta, int idprincipio, int idproduto, string? filtro)
        {
            var condicao = (OrcamentoProduto m) => (idfazenda == 0 || m.IdFazenda == idfazenda) &&
            (idsafra == 0 || m.IdSafra == idsafra) && m.idconta == idconta &&
            (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()))
            && (idprincipio == 0 || m.produtoorcamento.Any(p => p.IdPrincipioAtivo == idprincipio)) &&
            (idproduto == 0 || m.produtoorcamento.Any(p => p.IdProduto == idproduto));
            var query = _context.orcamentosproduto
                .Include(m => m.fazenda).Include(m => m.safra);
            var OrcamentoProdutos = query.Where(condicao)
                .Select(c => new ListOrcamentoProdutoViewModel
                {
                    Descricao = c.Descricao,

                    Id = c.Id,
                    IdFazenda = c.IdFazenda,
                    IdSafra = c.IdSafra,
                    DescFazenda = c.fazenda.Descricao,
                    DescSafra = c.safra.Descricao
                }
                ).ToList();
            return (OrcamentoProdutos);
        }
    }
}