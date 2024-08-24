using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPICore.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class PreferUsuService
    {
        private readonly FarmPlannerContext _context;

        public PreferUsuService(FarmPlannerContext context)
        {
            _context = context;
        }

        public async Task<PreferUsuViewModel> AdicionarPreferUsu(PreferUsuViewModel dados)
        {
            var PreferUsu = new PreferUsu();
            PreferUsu.idanoagricola = dados.idanoagricola;
            PreferUsu.idorganizacao = dados.idorganizacao;
            PreferUsu.uid = dados.uid;
            PreferUsu.idconta = dados.idconta;
            await _context.AddAsync(PreferUsu);
            await _context.SaveChangesAsync();
            return new PreferUsuViewModel
            {
                idanoagricola = PreferUsu.idanoagricola,
                idorganizacao = PreferUsu.idorganizacao,
                uid = PreferUsu.uid
            };
        }

        public async Task<PreferUsuViewModel>? SalvarPreferUsu(string uid, PreferUsuViewModel dados)
        {
            var PreferUsu = _context.preferUsus.Find(uid);
            if (PreferUsu != null)
            {
                PreferUsu.idanoagricola = dados.idanoagricola;
                PreferUsu.idorganizacao = dados.idorganizacao;
                _context.Update(PreferUsu);
                await _context.SaveChangesAsync();
                return new PreferUsuViewModel
                {
                    idanoagricola = PreferUsu.idanoagricola,
                    idorganizacao = PreferUsu.idorganizacao,
                    uid = PreferUsu.uid
                };
            }
            else return null;
        }

        public async Task<PreferUsuViewModel> ListarPreferUsu(string uid)
        {
            var condicao = ((PreferUsu m) => m.uid == uid);
            var query = _context.preferUsus
                .Include(p => p.anoAgricola)
                .Include(p => p.organizacao)
                .AsQueryable();
            var PreferUsus = query.Where(condicao)
                .Select(c => new PreferUsuViewModel
                {
                    idanoagricola = c.idanoagricola,
                    idorganizacao = c.idorganizacao,
                    uid = c.uid,
                    descano = c.anoAgricola.Descricao,
                    descorg = c.organizacao.Nome
                }
                ).FirstOrDefault();
            return (PreferUsus);
        }
    }
}