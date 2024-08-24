using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Maquinas;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class MaquinaParametroService
    {
        private readonly FarmPlannerContext _context;
        private readonly MaquinaParametroValidator _adicionarMarcaValidator;
        //private readonly ExcluirMaquinaParametroValidator _excluirMarcaValidator;

        public MaquinaParametroService(FarmPlannerContext context, MaquinaParametroValidator adicionarMarcaValidator)
        {
            _context = context;
            _adicionarMarcaValidator = adicionarMarcaValidator;
            //  _excluirMarcaValidator = excluirMarcaValidator;
        }

        public async Task<MaquinaParametroViewModel> AdicionarMaquinaParametro(MaquinaParametroViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);
            var reg = new MaquinaParametro();
            reg.IdCultura = dados.IdCultura;
            reg.IdMaquina = dados.IdMaquina;
            reg.IdOperacao = dados.IdOperacao;
            reg.Consumo = dados.Consumo;
            reg.Rendimento = dados.Rendimento;
            reg.idconta = dados.idconta;

            await _context.AddAsync(reg);
            await _context.SaveChangesAsync();
            return new MaquinaParametroViewModel
            {
                Id = reg.Id,
                IdOperacao = reg.IdOperacao,
                IdCultura = reg.IdCultura,
                IdMaquina = reg.IdMaquina,
                Consumo = reg.Consumo,
                Rendimento = reg.Rendimento
            };
        }

        public async Task<MaquinaParametroViewModel>? SalvarMaquinaParametro(int id, string idconta, MaquinaParametroViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);
            var reg = _context.maquinasparametro.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (reg != null)
            {
                reg.IdCultura = dados.IdCultura;
                reg.IdMaquina = dados.IdMaquina;
                reg.IdOperacao = dados.IdOperacao;
                reg.Consumo = dados.Consumo;
                reg.Rendimento = dados.Rendimento;

                _context.Update(reg);
                await _context.SaveChangesAsync();
                return new MaquinaParametroViewModel
                {
                    Id = reg.Id,
                    IdOperacao = reg.IdOperacao,
                    IdCultura = reg.IdCultura,
                    IdMaquina = reg.IdMaquina,
                    Consumo = reg.Consumo,
                    Rendimento = reg.Rendimento
                };
            }
            else return null;
        }

        public async Task<MaquinaParametroViewModel>? ExcluirMaquinaParametro(int id, string idconta)
        {
            var reg = _context.maquinasparametro.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (reg != null)
            {
                _context.maquinasparametro.Remove(reg);
                await _context.SaveChangesAsync();
                return new MaquinaParametroViewModel
                {
                    Id = reg.Id,
                    IdOperacao = reg.IdOperacao,
                    IdCultura = reg.IdCultura,
                    IdMaquina = reg.IdMaquina,
                    Consumo = reg.Consumo,
                    Rendimento = reg.Rendimento
                };
            }
            else return null;
        }

        public async Task<MaquinaParametroViewModel>? ListarMaquinaParametroById(int id, string idconta)
        {
            var reg = _context.maquinasparametro.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (reg != null)
            {
                return new MaquinaParametroViewModel
                {
                    Id = reg.Id,
                    IdOperacao = reg.IdOperacao,
                    IdCultura = reg.IdCultura,
                    IdMaquina = reg.IdMaquina,
                    Consumo = reg.Consumo,
                    Rendimento = reg.Rendimento
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListMaquinaParametroViewModel>> ListarMaquinaParametroByMaquina(int idmaquina, int idcultura, int idoperacao, string idconta)
        {
            var reg = _context.maquinasparametro.Where(c => (idmaquina == 0 || c.IdMaquina == idmaquina) && (c.idconta == idconta) &&
                     (idoperacao == 0 || c.IdOperacao == idoperacao) && (idcultura == 0 || c.IdCultura == idcultura))
                .Include(c => c.maquina)

                .Include(c => c.cultura)

                .Include(c => c.operacao)
                .Select(c => new ListMaquinaParametroViewModel
                {
                    Consumo = c.Consumo,
                    Rendimento = c.Rendimento,
                    Id = c.Id,
                    IdMaquina = c.IdMaquina,
                    IdCultura = c.IdCultura,
                    IdOperacao = c.IdOperacao,
                    DescCultura = c.cultura.Descricao,
                    DescMaquina = c.maquina.Descricao,
                    DescOperacao = c.operacao.Descricao
                }
                ).ToList();
            return (reg);
        }
    }
}