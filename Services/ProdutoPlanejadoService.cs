using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.ProdutoPlanejado;
using FarmPlannerAPICore.Models.ProdutoPlanejado;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ProdutoPlanejadoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoPlanejadoValidator _adicionarProdutoPlanejadoValidator;
        private readonly ExcluirProdutoPlanejadoValidator _excluirProdutoPlanejadoValidator;

        public ProdutoPlanejadoService(FarmPlannerContext context, ProdutoPlanejadoValidator adicionarProdutoPlanejadoValidator, ExcluirProdutoPlanejadoValidator excluirProdutoPlanejadoValidator)
        {
            _context = context;
            _adicionarProdutoPlanejadoValidator = adicionarProdutoPlanejadoValidator;
            _excluirProdutoPlanejadoValidator = excluirProdutoPlanejadoValidator;
        }

        public async Task<ProdutoPlanejadoViewModel> AdicionarProdutoPlanejado(ProdutoPlanejadoViewModel dados)
        {
            _adicionarProdutoPlanejadoValidator.ValidateAndThrow(dados);
            var ProdutoPlanejado = new ProdutoPlanejado();

            ProdutoPlanejado.IdProduto = dados.IdProduto;
            ProdutoPlanejado.IdPrincipioAtivo = dados.IdPrincipioAtivo;
            ProdutoPlanejado.Dosagem = dados.Dosagem;
            ProdutoPlanejado.AreaPercent = dados.AreaPercent;
            ProdutoPlanejado.Tamanho = dados.Tamanho;
            ProdutoPlanejado.TotalProduto = dados.TotalProduto;
            ProdutoPlanejado.IdPlanejamento = dados.IdPlanejamento;
            ProdutoPlanejado.idconta = dados.idconta;

            await _context.AddAsync(ProdutoPlanejado);
            await _context.SaveChangesAsync();
            return new ProdutoPlanejadoViewModel
            {
                Id = ProdutoPlanejado.Id,
                IdProduto = ProdutoPlanejado.IdProduto,
                IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                Dosagem = ProdutoPlanejado.Dosagem,
                AreaPercent = ProdutoPlanejado.AreaPercent,
                Tamanho = ProdutoPlanejado.Tamanho,
                TotalProduto = ProdutoPlanejado.TotalProduto,
                IdPlanejamento = ProdutoPlanejado.IdPlanejamento
            };
        }

        public async Task<ProdutoPlanejadoViewModel>? SalvarProdutoPlanejado(int id, ProdutoPlanejadoViewModel dados)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Find(id);
            if (ProdutoPlanejado != null)
            {
                ProdutoPlanejado.IdProduto = dados.IdProduto;
                ProdutoPlanejado.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                ProdutoPlanejado.Dosagem = dados.Dosagem;
                ProdutoPlanejado.AreaPercent = dados.AreaPercent;
                ProdutoPlanejado.Tamanho = dados.Tamanho;
                ProdutoPlanejado.TotalProduto = dados.TotalProduto;
                ProdutoPlanejado.IdPlanejamento = dados.IdPlanejamento;

                _context.Update(ProdutoPlanejado);
                await _context.SaveChangesAsync();
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<ProdutoPlanejadoViewModel>? ExcluirProdutoPlanejado(int id, ProdutoPlanejadoViewModel dados)
        {
            _excluirProdutoPlanejadoValidator.ValidateAndThrow(dados);
            var ProdutoPlanejado = _context.produtoplanejados.Find(id);
            if (ProdutoPlanejado != null)
            {
                _context.produtoplanejados.Remove(ProdutoPlanejado);
                await _context.SaveChangesAsync();
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<ProdutoPlanejadoViewModel>? ListarProdutoPlanejadoById(int id)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Find(id);
            if (ProdutoPlanejado != null)
            {
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListProdutoPlanejadoViewModel>> ListarProdutoPlanejadoByPlanejamento(int idplanejamento)
        {
            var query = _context.produtoplanejados
                .Include(m => m.produto).Include(m => m.principioativo)
                .Where(m => (m.IdPlanejamento == idplanejamento));
            var ProdutoPlanejados = query
                .Select(c => new ListProdutoPlanejadoViewModel
                {
                    Id = c.Id,
                    IdProduto = c.IdProduto,
                    IdPrincipioAtivo = c.IdPrincipioAtivo,
                    Dosagem = c.Dosagem,
                    AreaPercent = c.AreaPercent,
                    Tamanho = c.Tamanho,
                    TotalProduto = c.TotalProduto,
                    IdPlanejamento = c.IdPlanejamento,
                    descprincativo = c.principioativo.Descricao,
                    descproduto = c.produto.Descricao
                }
                ).ToList();
            return (ProdutoPlanejados);
        }
    }
}