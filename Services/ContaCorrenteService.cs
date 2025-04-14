using ADUSAPI.Context;
using ADUSAPI.Entities;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ADUSAPI.Validators.Banco;
using ADUSAPICore.Models.Banco;

namespace ADUSAPI.Services
{
    public class ContaCorrenteService
    {
        private readonly ADUSContext _context;
        private readonly ContaCorrenteValidator _validator;

        public ContaCorrenteService(ADUSContext context, ContaCorrenteValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ContaCorrenteViewModel> Adicionar(ContaCorrenteViewModel dados)
        {
            _validator.ValidateAndThrow(dados);
            var entidade = new ContaCorrente
            {
                Id = dados.Id,
                Descricao = dados.Descricao,
                Agencia = dados.Agencia,
                ContaBanco = dados.ContaBanco,
                Titular = dados.Titular,
                BancoId = dados.BancoId
            };
            _context.contascorrentes.Add(entidade);
            await _context.SaveChangesAsync();
            return dados;
        }

        public async Task<ContaCorrenteViewModel?> Salvar(string id, ContaCorrenteViewModel dados)
        {
            var entidade = await _context.contascorrentes.FindAsync(id);
            if (entidade == null) return null;

            entidade.Descricao = dados.Descricao;
            entidade.Agencia = dados.Agencia;
            entidade.ContaBanco = dados.ContaBanco;
            entidade.Titular = dados.Titular;
            entidade.BancoId = dados.BancoId;
            await _context.SaveChangesAsync();

            return dados;
        }

        public async Task<ContaCorrenteViewModel?> Excluir(string id)
        {
            var entidade = await _context.contascorrentes.FindAsync(id);
            if (entidade == null) return null;

            _context.contascorrentes.Remove(entidade);
            await _context.SaveChangesAsync();

            return new ContaCorrenteViewModel
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao,
                Agencia = entidade.Agencia,
                ContaBanco = entidade.ContaBanco,
                Titular = entidade.Titular,
                BancoId = entidade.BancoId
            };
        }

        public async Task<ContaCorrenteViewModel?> GetById(string id)
        {
            var entidade = await _context.contascorrentes.FindAsync(id);
            if (entidade == null) return null;

            return new ContaCorrenteViewModel
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao,
                Agencia = entidade.Agencia,
                ContaBanco = entidade.ContaBanco,
                Titular = entidade.Titular,
                BancoId = entidade.BancoId
            };
        }

        public async Task<IEnumerable<ContaCorrenteViewModel>> Listar(string? filtro, int? bancoId)
        {
            var query = _context.contascorrentes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
                query = query.Where(c => c.Descricao.Contains(filtro));

            if (bancoId.HasValue)
                query = query.Where(c => c.BancoId == bancoId);

            return await query.Select(c => new ContaCorrenteViewModel
            {
                Id = c.Id,
                Descricao = c.Descricao,
                Agencia = c.Agencia,
                ContaBanco = c.ContaBanco,
                Titular = c.Titular,
                BancoId = c.BancoId
            }).ToListAsync();
        }
    }
}