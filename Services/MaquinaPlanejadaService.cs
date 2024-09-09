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

        public async Task<(bool sucess, MaquinaPlanejadaViewModel maq)> AdicionarMaquinaPlanejada(MaquinaPlanejadaViewModel dados)
        {
            _adicionarMaquinaPlanejadaValidator.ValidateAndThrow(dados);
            var MaquinaPlanejada = new MaquinaPlanejada();
            MaquinaPlanejada.IdMaquina = dados.IdMaquina;
            MaquinaPlanejada.IdModeloMaquina = dados.IdModeloMaquina;
            MaquinaPlanejada.Consumo = dados.Consumo;
            MaquinaPlanejada.Rendimento = dados.Rendimento;
            MaquinaPlanejada.IdPlanejamento = dados.IdPlanejamento;
            MaquinaPlanejada.QtdCombEstimado = dados.QtdCombEstimado;
            MaquinaPlanejada.QtdHoraEstimada = dados.QtdHoraEstimada;
            MaquinaPlanejada.idconta = dados.idconta;

            await _context.AddAsync(MaquinaPlanejada);
            await _context.SaveChangesAsync();
            return (true, new MaquinaPlanejadaViewModel
            {
                Id = MaquinaPlanejada.Id,
                IdMaquina = MaquinaPlanejada.IdMaquina,
                IdModeloMaquina = MaquinaPlanejada.IdModeloMaquina,
                Consumo = MaquinaPlanejada.Consumo,
                Rendimento = MaquinaPlanejada.Rendimento,
                IdPlanejamento = MaquinaPlanejada.IdPlanejamento,
                QtdCombEstimado = MaquinaPlanejada.QtdCombEstimado,
                QtdHoraEstimada = MaquinaPlanejada.QtdHoraEstimada
            });
        }

        public async Task<MaquinaPlanejadaViewModel>? SalvarMaquinaPlanejada(int id, MaquinaPlanejadaViewModel dados)
        {
            var MaquinaPlanejada = _context.maquinasplanejadas.Find(id);
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

        public async Task<MaquinaPlanejadaViewModel>? ExcluirMaquinaPlanejada(int id, MaquinaPlanejadaViewModel dados)
        {
            _excluirMaquinaPlanejadaValidator.ValidateAndThrow(dados);
            var MaquinaPlanejada = _context.maquinasplanejadas.Find(id);
            if (MaquinaPlanejada != null)
            {
                _context.maquinasplanejadas.Remove(MaquinaPlanejada);
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

        public async Task<MaquinaPlanejadaViewModel>? ListarMaquinaPlanejadaById(int id)
        {
            var MaquinaPlanejada = _context.maquinasplanejadas.Find(id);
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

        public async Task<IEnumerable<ListMaquinaPlanejadaViewModel>> ListarMaquinaPlanejadaByPlanejamento(int idplanejamento)
        {
            var query = _context.maquinasplanejadas
                .Include(m => m.maquina).Include(m => m.modelomaquina)
                .Where(m => (m.IdPlanejamento == idplanejamento));
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