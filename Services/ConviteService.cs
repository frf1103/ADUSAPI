using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Migrations;
using ADUSAPICore.Models.Convite;
using ADUSClient.Convite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace ADUSAPI.Services
{
    public class ConviteService
    {
        private readonly ADUSContext _context;

        public ConviteService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<List<ADUSAPICore.Models.Convite.ConviteViewModel>> ListarConvitesAsync(string idcoprodutor, string idafiliado, int? status, int? expirados, string? titular)
        {
            Expression<Func<Convite, bool>> condicao = x =>
                (idcoprodutor == null || x.afiliado.idcoprodutor == idcoprodutor || idcoprodutor == "0") &&
                (idafiliado == "null" || x.IdAfiliado == idafiliado) &&
                (status == null || x.Status == status || status == 3) &&
                (titular == "null" || x.Titular.Contains(titular)) &&
                (
                    (expirados == 1 && x.DataExpiracao <= DateTime.Now) ||
                    (expirados == 0 && x.DataExpiracao > DateTime.Now) ||
                    (expirados == 2)
                );

            return await _context.convites
                .Include(x => x.afiliado)
                .Where(condicao)
                .Select(c => new ADUSAPICore.Models.Convite.ConviteViewModel
                {
                    IdConvite = c.IdConvite,
                    Fone = c.Fone,
                    Email = c.Email,
                    DataCriacao = c.DataCriacao,
                    DataExpiracao = c.DataExpiracao,
                    IdAfiliado = c.IdAfiliado,
                    IdPlataforma = c.IdPlataforma,
                    Status = c.Status,
                    idassinatura = c.idassinatura,
                    idformapgto = c.idformapgto,
                    Titular = c.Titular
                })
                .ToListAsync();
        }

        public async Task<ADUSAPICore.Models.Convite.ConviteViewModel?> ObterPorId(string id)
        {
            var c = await _context.convites.FindAsync(id);
            if (c == null) return null;

            return new ADUSAPICore.Models.Convite.ConviteViewModel
            {
                IdConvite = c.IdConvite,
                Fone = c.Fone,
                Email = c.Email,
                DataCriacao = c.DataCriacao,
                DataExpiracao = c.DataExpiracao,
                IdAfiliado = c.IdAfiliado,
                IdPlataforma = c.IdPlataforma,
                Status = c.Status,
                idassinatura = c.idassinatura,
                idformapgto = c.idformapgto,
                Titular = c.Titular
            };
        }

        public async Task Adicionar(ADUSAPICore.Models.Convite.ConviteViewModel vm)
        {
            var convite = new Convite
            {
                IdConvite = vm.IdConvite,
                Fone = vm.Fone,
                Email = vm.Email,
                DataCriacao = vm.DataCriacao,
                DataExpiracao = vm.DataExpiracao,
                IdAfiliado = vm.IdAfiliado,
                IdPlataforma = vm.IdPlataforma,
                Status = vm.Status,
                idassinatura = vm.idassinatura,
                idformapgto = vm.idformapgto,
                Titular = vm.Titular
            };

            _context.convites.Add(convite);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(ADUSAPICore.Models.Convite.ConviteViewModel vm)
        {
            var convite = await _context.convites.FindAsync(vm.IdConvite);
            if (convite == null) return;

            convite.Fone = vm.Fone;
            convite.Email = vm.Email;
            convite.DataCriacao = vm.DataCriacao;
            convite.DataExpiracao = vm.DataExpiracao;
            convite.IdAfiliado = vm.IdAfiliado;
            convite.IdPlataforma = vm.IdPlataforma;
            convite.Status = vm.Status;
            convite.idassinatura = vm.idassinatura;
            convite.idformapgto = vm.idformapgto;
            convite.Titular = vm.Titular;

            _context.convites.Update(convite);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(string id)
        {
            var convite = await _context.convites.FindAsync(id);
            if (convite != null)
            {
                _context.convites.Remove(convite);
                await _context.SaveChangesAsync();
            }
        }
    }
}