using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Validators.Banco;
using ADUSAPICore.Models.Banco;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Services
{
    public class BancoService
    {
        private readonly ADUSContext _context;
        private readonly BancoValidator _bancoValidator;
        private readonly ExcluirBancoValidator _excluirBancoValidator;

        public BancoService(
            ADUSContext context,
            BancoValidator bancoValidator,
            ExcluirBancoValidator excluirBancoValidator)
        {
            _context = context;
            _bancoValidator = bancoValidator;
            _excluirBancoValidator = excluirBancoValidator;
        }

        public async Task<BancoViewModel> AdicionarBanco(BancoViewModel dados)
        {
            _bancoValidator.ValidateAndThrow(dados);

            var banco = new Banco
            {
                Descricao = dados.Descricao,
                Codigo = dados.Codigo
            };

            await _context.bancos.AddAsync(banco);
            await _context.SaveChangesAsync();

            return new BancoViewModel
            {
                Id = banco.Id,
                Descricao = banco.Descricao,
                Codigo = banco.Codigo
            };
        }

        public async Task<BancoViewModel?> SalvarBanco(int id, BancoViewModel dados)
        {
            var banco = await _context.bancos.FindAsync(id);
            if (banco == null) return null;

            banco.Descricao = dados.Descricao;
            banco.Codigo = dados.Codigo;

            _context.Update(banco);
            await _context.SaveChangesAsync();

            return new BancoViewModel
            {
                Id = banco.Id,
                Descricao = banco.Descricao,
                Codigo = banco.Codigo
            };
        }

        public async Task<BancoViewModel?> ExcluirBanco(int id)
        {
            var banco = await _context.bancos.FindAsync(id);
            if (banco == null) return null;

            var dados = new BancoViewModel
            {
                Id = banco.Id,
                Descricao = banco.Descricao,
                Codigo = banco.Codigo
            };

            _excluirBancoValidator.ValidateAndThrow(dados);

            _context.bancos.Remove(banco);
            await _context.SaveChangesAsync();

            return dados;
        }

        public async Task<BancoViewModel?> ListarBancoById(int id)
        {
            var banco = await _context.bancos.FindAsync(id);
            if (banco == null) return null;

            return new BancoViewModel
            {
                Id = banco.Id,
                Descricao = banco.Descricao,
                Codigo = banco.Codigo
            };
        }

        public async Task<IEnumerable<BancoViewModel>> ListarBanco(string? filtro)
        {
            return await _context.bancos
                .Where(b => string.IsNullOrWhiteSpace(filtro) || b.Descricao.ToUpper().Contains(filtro.ToUpper()))
                .Select(b => new BancoViewModel
                {
                    Id = b.Id,
                    Descricao = b.Descricao,
                    Codigo = b.Codigo
                })
                .ToListAsync();
        }
    }
}