using FarmPlannerAPICore.Models.FarmLog;
using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.AnoAgricola;
using FarmPlannerAPICore.Models.AnoAgricola;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class FarmPlannerLogService
    {
        private readonly FarmPlannerContext _context;

        public FarmPlannerLogService(FarmPlannerContext context)
        {
            _context = context;
        }

        public async Task<FarmPlannerLogViewModel> AdicionarFarmPLannerLog(FarmPlannerLogViewModel dados)
        {
            var FarmPLannerLog = new FarmPlannerLog();
            FarmPLannerLog.uid = dados.uid;
            FarmPLannerLog.transacao = dados.transacao;
            FarmPLannerLog.datalog = DateTime.Now;
            await _context.AddAsync(FarmPLannerLog);
            await _context.SaveChangesAsync();
            return new FarmPlannerLogViewModel
            {
                uid = FarmPLannerLog.uid,
                transacao = FarmPLannerLog.transacao,
                datalog = FarmPLannerLog.datalog,
                id = FarmPLannerLog.id
            };
        }

        public async Task<FarmPlannerLogViewModel>? ListarFarmPLannerLogById(int id)
        {
            var FarmPLannerLog = _context.farmPlannerLogs.Find(id);
            if (FarmPLannerLog != null)
            {
                return new FarmPlannerLogViewModel
                {
                    id = FarmPLannerLog.id,
                    uid = FarmPLannerLog.uid,
                    transacao = FarmPLannerLog.transacao,
                    datalog = FarmPLannerLog.datalog
                };
            }
            else return null;
        }

        public async Task<IEnumerable<FarmPlannerLogViewModel>> ListarFarmPLannerLog(string? filtro)
        {
            var condicao = (FarmPlannerLog m) => (String.IsNullOrWhiteSpace(filtro) || m.transacao.ToUpper().Contains(filtro.ToUpper()));

            var FarmPLannerLogs = _context.farmPlannerLogs.Where(condicao)
                .Select(c => new FarmPlannerLogViewModel
                {
                    id = c.id,
                    uid = c.uid,
                    transacao = c.transacao,
                    datalog = c.datalog
                }
                ).ToList();
            return (FarmPLannerLogs);
        }
    }
}