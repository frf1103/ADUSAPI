using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.GrupoProduto;
using FarmPlannerAPICore.Models.GrupoProduto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class GrupoProdutoService
    {
        private readonly FarmPlannerContext _context;
        private readonly GrupoProdutoValidator _adicionarGrupoProdutoValidator;
        private readonly ExcluirGrupoProdutoValidator _excluirGrupoProdutoValidator;

        public GrupoProdutoService(FarmPlannerContext context, GrupoProdutoValidator adicionarGrupoProdutoValidator, ExcluirGrupoProdutoValidator excluirGrupoProdutoValidator)
        {
            _context = context;
            _adicionarGrupoProdutoValidator = adicionarGrupoProdutoValidator;
            _excluirGrupoProdutoValidator = excluirGrupoProdutoValidator;
        }

        public async Task<GrupoProdutoViewModel> AdicionarGrupoProduto(GrupoProdutoViewModel dados)
        {
            _adicionarGrupoProdutoValidator.ValidateAndThrow(dados);
            var GrupoProduto = new GrupoProduto();
            GrupoProduto.Descricao = dados.Descricao;
            GrupoProduto.Tipo = dados.Tipo;

            await _context.AddAsync(GrupoProduto);
            await _context.SaveChangesAsync();
            return new GrupoProdutoViewModel
            {
                Descricao = GrupoProduto.Descricao,
                Tipo = dados.Tipo,
                Id = GrupoProduto.Id,
            };
        }

        public async Task<GrupoProdutoViewModel>? SalvarGrupoProduto(int id, GrupoProdutoViewModel dados)
        {
            var GrupoProduto = _context.gruposprodutos.Find(id);
            if (GrupoProduto != null)
            {
                GrupoProduto.Descricao = dados.Descricao;
                GrupoProduto.Tipo = dados.Tipo;

                _context.Update(GrupoProduto);
                await _context.SaveChangesAsync();
                return new GrupoProdutoViewModel
                {
                    Descricao = GrupoProduto.Descricao,
                    Id = GrupoProduto.Id,
                    Tipo = GrupoProduto.Tipo
                };
            }
            else return null;
        }

        public async Task<GrupoProdutoViewModel>? ExcluirGrupoProduto(int id)
        {
            var GrupoProduto = _context.gruposprodutos.Find(id);
            if (GrupoProduto != null)
            {
                GrupoProdutoViewModel dados = new GrupoProdutoViewModel
                {
                    Id = GrupoProduto.Id,
                    Descricao = GrupoProduto.Descricao,
                    Tipo = GrupoProduto.Tipo
                };
                _excluirGrupoProdutoValidator.ValidateAndThrow(dados);
                _context.gruposprodutos.Remove(GrupoProduto);
                await _context.SaveChangesAsync();
                return new GrupoProdutoViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<GrupoProdutoViewModel>? ListarGrupoProdutoById(int id)
        {
            var GrupoProduto = _context.gruposprodutos.Find(id);
            if (GrupoProduto != null)
            {
                return new GrupoProdutoViewModel
                {
                    Descricao = GrupoProduto.Descricao,
                    Id = GrupoProduto.Id,
                    Tipo = GrupoProduto.Tipo
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListGrupoProdutoViewModel>> ListarGrupoProduto(string? filtro)
        {
            var GrupoProdutos = _context.gruposprodutos.
                Where(m => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Select(c => new ListGrupoProdutoViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Tipo = c.Tipo,
                    desctipo = c.Tipo.ToString()
                }
                ).ToList();
            return (GrupoProdutos);
        }
    }
}