using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.OrcamentoProduto;
using FarmPlannerAPICore.Models.OrcamentoProduto;

using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ProdutoOrcamentoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoOrcamentoValidator _adicionarProdutoOrcamentoValidator;
        private readonly ExcluirProdutoOrcamentoValidator _excluirProdutoOrcamentoValidator;

        public ProdutoOrcamentoService(FarmPlannerContext context, ProdutoOrcamentoValidator adicionarProdutoOrcamentoValidator, ExcluirProdutoOrcamentoValidator excluirProdutoOrcamentoValidator)
        {
            _context = context;
            _adicionarProdutoOrcamentoValidator = adicionarProdutoOrcamentoValidator;
            _excluirProdutoOrcamentoValidator = excluirProdutoOrcamentoValidator;
        }

        public async Task<ProdutoOrcamentoViewModel> AdicionarProdutoOrcamento(ProdutoOrcamentoViewModel dados)
        {
            _adicionarProdutoOrcamentoValidator.ValidateAndThrow(dados);
            var ProdutoOrcamento = new ProdutoOrcamento();
            ProdutoOrcamento.TipoProdutoOrc = dados.TipoProdutoOrc;
            ProdutoOrcamento.IdProduto = dados.IdProduto;
            ProdutoOrcamento.IdOrcamento = dados.IdOrcamento;
            ProdutoOrcamento.IdPrincipioAtivo = dados.IdPrincipioAtivo;
            ProdutoOrcamento.PrecoUnitario = dados.PrecoUnitario;
            ProdutoOrcamento.DataPreco = dados.DataPreco;
            ProdutoOrcamento.idconta = dados.idconta;

            await _context.AddAsync(ProdutoOrcamento);
            await _context.SaveChangesAsync();
            return new ProdutoOrcamentoViewModel
            {
                Id = ProdutoOrcamento.Id,
                IdOrcamento = ProdutoOrcamento.IdOrcamento,
                IdProduto = ProdutoOrcamento.IdProduto,
                TipoProdutoOrc = ProdutoOrcamento.TipoProdutoOrc,
                PrecoUnitario = ProdutoOrcamento.PrecoUnitario,
                IdPrincipioAtivo = ProdutoOrcamento.IdPrincipioAtivo,
                DataPreco = ProdutoOrcamento.DataPreco
            };
        }

        public async Task<ProdutoOrcamentoViewModel>? SalvarProdutoOrcamento(int id, ProdutoOrcamentoViewModel dados)
        {
            var ProdutoOrcamento = _context.produtosorcamento.Find(id);
            if (ProdutoOrcamento != null)
            {
                ProdutoOrcamento.TipoProdutoOrc = dados.TipoProdutoOrc;
                ProdutoOrcamento.IdProduto = dados.IdProduto;
                ProdutoOrcamento.IdOrcamento = dados.IdOrcamento;
                ProdutoOrcamento.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                ProdutoOrcamento.PrecoUnitario = dados.PrecoUnitario;
                ProdutoOrcamento.DataPreco = dados.DataPreco;

                _context.Update(ProdutoOrcamento);
                await _context.SaveChangesAsync();
                return new ProdutoOrcamentoViewModel
                {
                    Id = ProdutoOrcamento.Id,
                    IdOrcamento = ProdutoOrcamento.IdOrcamento,
                    IdProduto = ProdutoOrcamento.IdProduto,
                    TipoProdutoOrc = ProdutoOrcamento.TipoProdutoOrc,
                    PrecoUnitario = ProdutoOrcamento.PrecoUnitario,
                    IdPrincipioAtivo = ProdutoOrcamento.IdPrincipioAtivo,
                    DataPreco = ProdutoOrcamento.DataPreco
                };
            }
            else return null;
        }

        public async Task<ProdutoOrcamentoViewModel>? ExcluirProdutoOrcamento(int id, ProdutoOrcamentoViewModel dados)
        {
            _excluirProdutoOrcamentoValidator.ValidateAndThrow(dados);
            var ProdutoOrcamento = _context.produtosorcamento.Find(id);
            if (ProdutoOrcamento != null)
            {
                _context.produtosorcamento.Remove(ProdutoOrcamento);
                await _context.SaveChangesAsync();
                return new ProdutoOrcamentoViewModel
                {
                    Id = ProdutoOrcamento.Id,
                    IdOrcamento = ProdutoOrcamento.IdOrcamento,
                    IdProduto = ProdutoOrcamento.IdProduto,
                    TipoProdutoOrc = ProdutoOrcamento.TipoProdutoOrc,
                    PrecoUnitario = ProdutoOrcamento.PrecoUnitario,
                    IdPrincipioAtivo = ProdutoOrcamento.IdPrincipioAtivo,
                    DataPreco = ProdutoOrcamento.DataPreco
                };
            }
            else return null;
        }

        public async Task<ProdutoOrcamentoViewModel>? ListarProdutoOrcamentoById(int id)
        {
            var ProdutoOrcamento = _context.produtosorcamento.Find(id);
            if (ProdutoOrcamento != null)
            {
                return new ProdutoOrcamentoViewModel
                {
                    Id = ProdutoOrcamento.Id,
                    IdOrcamento = ProdutoOrcamento.IdOrcamento,
                    IdProduto = ProdutoOrcamento.IdProduto,
                    TipoProdutoOrc = ProdutoOrcamento.TipoProdutoOrc,
                    PrecoUnitario = ProdutoOrcamento.PrecoUnitario,
                    IdPrincipioAtivo = ProdutoOrcamento.IdPrincipioAtivo,
                    DataPreco = ProdutoOrcamento.DataPreco
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListProdutoOrcamentoViewModel>> ListarProdutoOrcamentoByOrcamento(int idorcamento, int idprincativo, int idproduto)
        {
            var condicao = (ProdutoOrcamento m) => (m.IdOrcamento == idorcamento) && (idproduto == 0 || m.IdProduto == idproduto) &&
            (idprincativo == 0 || m.IdPrincipioAtivo == idprincativo);

            var query = _context.produtosorcamento
                .Include(x => x.princativo).Include(x => x.produto);
            var ProdutoOrcamentos = query.Where(condicao)
                .Select(c => new ListProdutoOrcamentoViewModel
                {
                    Id = c.Id,
                    IdOrcamento = c.IdOrcamento,
                    IdProduto = c.IdProduto,
                    TipoProdutoOrc = c.TipoProdutoOrc,
                    PrecoUnitario = c.PrecoUnitario,
                    IdPrincipioAtivo = c.IdPrincipioAtivo,
                    descpricativo = c.princativo.Descricao,
                    descproduto = c.produto.Descricao,
                    desctipoproduto = c.TipoProdutoOrc.ToString(),
                    DataPreco = c.DataPreco
                }
                ).ToList();
            return (ProdutoOrcamentos);
        }
    }
}