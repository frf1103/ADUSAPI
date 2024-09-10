using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.MaquinaPlanejada;
using FarmPlannerAPICore.Models.MaquinaPlanejada;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class MaquinaPlanejadaService
    {
        private readonly FarmPlannerContext _context;
        private readonly MaquinaPlanejadaValidator _adicionarMaquinaPlanejadaValidator;
        private readonly ExcluirMaquinaPlanejadaValidator _excluirMaquinaPlanejadaValidator;

        public MaquinaPlanejadaService(FarmPlannerContext context, MaquinaPlanejadaValidator adicionarMaquinaPlanejadaValidator, ExcluirMaquinaPlanejadaValidator excluirMaquinaPlanejadaValidator)
        {
            _context = context;
            _adicionarMaquinaPlanejadaValidator = adicionarMaquinaPlanejadaValidator;
            _excluirMaquinaPlanejadaValidator = excluirMaquinaPlanejadaValidator;
        }

        public async Task<(MaquinaPlanejadaViewModel maq, List<string> listerros)> AdicionarMaquinaPlanejada(MaquinaPlanejadaViewModel dados)
        {
            _adicionarMaquinaPlanejadaValidator.ValidateAndThrow(dados);
            var MaquinaPlanejada = new MaquinaPlanejada();
            MaquinaPlanejada.IdMaquina = (dados.IdMaquina == 0) ? null : dados.IdMaquina;
            MaquinaPlanejada.IdModeloMaquina = dados.IdModeloMaquina;
            MaquinaPlanejada.Consumo = dados.Consumo;
            MaquinaPlanejada.Rendimento = dados.Rendimento;
            MaquinaPlanejada.IdPlanejamento = dados.IdPlanejamento;
            MaquinaPlanejada.QtdCombEstimado = dados.QtdCombEstimado;
            MaquinaPlanejada.QtdHoraEstimada = dados.QtdHoraEstimada;
            MaquinaPlanejada.idconta = dados.idconta;
            MaquinaPlanejada.uid = dados.uid;
            MaquinaPlanejada.datains = DateTime.Now;

            await _context.AddAsync(MaquinaPlanejada);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  Maquina Planejada " + MaquinaPlanejada.Id.ToString() + "/" + MaquinaPlanejada.IdPlanejamento.ToString() + "/" + MaquinaPlanejada.IdMaquina.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return (new MaquinaPlanejadaViewModel
            {
                Id = MaquinaPlanejada.Id,
                IdMaquina = MaquinaPlanejada.IdMaquina,
                IdModeloMaquina = MaquinaPlanejada.IdModeloMaquina,
                Consumo = MaquinaPlanejada.Consumo,
                Rendimento = MaquinaPlanejada.Rendimento,
                IdPlanejamento = MaquinaPlanejada.IdPlanejamento,
                QtdCombEstimado = MaquinaPlanejada.QtdCombEstimado,
                QtdHoraEstimada = MaquinaPlanejada.QtdHoraEstimada
            }, null);
        }

        public async Task<MaquinaPlanejadaViewModel>? SalvarMaquinaPlanejada(int id, string idconta, MaquinaPlanejadaViewModel dados)
        {
            var MaquinaPlanejada = _context.maquinasplanejadas.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (MaquinaPlanejada != null)
            {
                MaquinaPlanejada.IdMaquina = dados.IdMaquina;
                MaquinaPlanejada.IdModeloMaquina = dados.IdModeloMaquina;
                MaquinaPlanejada.Consumo = dados.Consumo;
                MaquinaPlanejada.Rendimento = dados.Rendimento;
                MaquinaPlanejada.IdPlanejamento = dados.IdPlanejamento;
                MaquinaPlanejada.QtdCombEstimado = dados.QtdCombEstimado;
                MaquinaPlanejada.QtdHoraEstimada = dados.QtdHoraEstimada;

                _context.Update(MaquinaPlanejada);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração  Maquina Planejada " + MaquinaPlanejada.Id.ToString() + "/" + MaquinaPlanejada.IdPlanejamento.ToString() + "/" + MaquinaPlanejada.IdMaquina.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new MaquinaPlanejadaViewModel
                {
                    Id = MaquinaPlanejada.Id,
                    IdMaquina = (int)MaquinaPlanejada.IdMaquina,
                    IdModeloMaquina = MaquinaPlanejada.IdModeloMaquina,
                    Consumo = MaquinaPlanejada.Consumo,
                    Rendimento = MaquinaPlanejada.Rendimento,
                    IdPlanejamento = MaquinaPlanejada.IdPlanejamento,
                    QtdCombEstimado = MaquinaPlanejada.QtdCombEstimado,
                    QtdHoraEstimada = MaquinaPlanejada.QtdHoraEstimada
                };
            }
            else return null;
        }

        public async Task<MaquinaPlanejadaViewModel>? ExcluirMaquinaPlanejada(int id, string idconta, string uid)
        {
            var MaquinaPlanejada = _context.maquinasplanejadas.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (MaquinaPlanejada != null)
            {
                MaquinaPlanejadaViewModel dados = new MaquinaPlanejadaViewModel
                {
                    Id = id
                };
                _excluirMaquinaPlanejadaValidator.ValidateAndThrow(dados);
                _context.maquinasplanejadas.Remove(MaquinaPlanejada);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  Maquina Planejada " + MaquinaPlanejada.Id.ToString() + "/" + MaquinaPlanejada.IdPlanejamento.ToString() + "/" + MaquinaPlanejada.IdMaquina.ToString(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new MaquinaPlanejadaViewModel
                {
                    Id = MaquinaPlanejada.Id,
                    IdMaquina = MaquinaPlanejada.IdMaquina,
                    IdModeloMaquina = MaquinaPlanejada.IdModeloMaquina,
                    Consumo = MaquinaPlanejada.Consumo,
                    Rendimento = MaquinaPlanejada.Rendimento,
                    IdPlanejamento = MaquinaPlanejada.IdPlanejamento,
                    QtdCombEstimado = MaquinaPlanejada.QtdCombEstimado,
                    QtdHoraEstimada = MaquinaPlanejada.QtdHoraEstimada
                };
            }
            else return null;
        }

        public async Task<MaquinaPlanejadaViewModel>? ListarMaquinaPlanejadaById(int id, string idconta)
        {
            var MaquinaPlanejada = _context.maquinasplanejadas.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (MaquinaPlanejada != null)
            {
                return new MaquinaPlanejadaViewModel
                {
                    Id = MaquinaPlanejada.Id,
                    IdMaquina = MaquinaPlanejada.IdMaquina,
                    IdModeloMaquina = MaquinaPlanejada.IdModeloMaquina,
                    Consumo = MaquinaPlanejada.Consumo,
                    Rendimento = MaquinaPlanejada.Rendimento,
                    IdPlanejamento = MaquinaPlanejada.IdPlanejamento,
                    QtdCombEstimado = MaquinaPlanejada.QtdCombEstimado,
                    QtdHoraEstimada = MaquinaPlanejada.QtdHoraEstimada
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListMaquinaPlanejadaViewModel>> ListarMaquinaPlanejadaByPlanejamento(int idplanejamento, string idconta)
        {
            var query = _context.maquinasplanejadas
                .Include(m => m.maquina).Include(m => m.modelomaquina)
                .Where(m => (m.IdPlanejamento == idplanejamento && m.idconta == idconta));
            var MaquinaPlanejadas = query
                .Select(c => new ListMaquinaPlanejadaViewModel
                {
                    Id = c.Id,
                    IdMaquina = c.IdMaquina,
                    IdModeloMaquina = c.IdModeloMaquina,
                    Consumo = c.Consumo,
                    Rendimento = c.Rendimento,
                    IdPlanejamento = c.IdPlanejamento,
                    QtdCombEstimado = c.QtdCombEstimado,
                    QtdHoraEstimada = c.QtdHoraEstimada,
                    DescMaquina = c.maquina.Descricao,
                    DescModelo = c.modelomaquina.Descricao
                }
                ).ToList();
            return (MaquinaPlanejadas);
        }
    }
}