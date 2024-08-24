using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Maquinas;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class ModeloParametroService
    {
        private readonly FarmPlannerContext _context;
        private readonly ModeloParametroValidator _adicionarMarcaValidator;
        //private readonly ExcluirModeloParametroValidator _excluirMarcaValidator;

        public ModeloParametroService(FarmPlannerContext context, ModeloParametroValidator adicionarMarcaValidator)
        {
            _context = context;
            _adicionarMarcaValidator = adicionarMarcaValidator;
            //  _excluirMarcaValidator = excluirMarcaValidator;
        }

        public async Task<ModeloParametroViewModel> AdicionarModeloParametro(ModeloParametroViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);
            var reg = new ModeloParametro();
            reg.IdCultura = dados.IdCultura;
            reg.IdModeloMaquina = dados.IdModeloMaquina;
            reg.IdOperacao = dados.IdOperacao;
            reg.Consumo = dados.Consumo;
            reg.Rendimento = dados.Rendimento;
            reg.idconta = dados.idconta;

            await _context.AddAsync(reg);
            await _context.SaveChangesAsync();
            return new ModeloParametroViewModel
            {
                Id = reg.Id,
                IdOperacao = reg.IdOperacao,
                IdCultura = reg.IdCultura,
                IdModeloMaquina = reg.IdModeloMaquina,
                Consumo = reg.Consumo,
                Rendimento = reg.Rendimento
            };
        }

        public async Task<ModeloParametroViewModel>? SalvarModeloParametro(int id, string idconta, ModeloParametroViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);
            var reg = _context.modelosparametros.Find(id);
            if (reg != null)
            {
                reg.IdCultura = dados.IdCultura;
                reg.IdModeloMaquina = dados.IdModeloMaquina;
                reg.IdOperacao = dados.IdOperacao;
                reg.Consumo = dados.Consumo;
                reg.Rendimento = dados.Rendimento;

                _context.Update(reg);
                await _context.SaveChangesAsync();
                return new ModeloParametroViewModel
                {
                    Id = reg.Id,
                    IdOperacao = reg.IdOperacao,
                    IdCultura = reg.IdCultura,
                    IdModeloMaquina = reg.IdModeloMaquina,
                    Consumo = reg.Consumo,
                    Rendimento = reg.Rendimento
                };
            }
            else return null;
        }

        public async Task<ModeloParametroViewModel>? ExcluirModeloParametro(int id, string idconta)
        {
            var reg = _context.modelosparametros.Where(m => m.Id == id && m.idconta == idconta).FirstOrDefault();
            if (reg != null)
            {
                _context.modelosparametros.Remove(reg);
                await _context.SaveChangesAsync();
                return new ModeloParametroViewModel
                {
                    Id = reg.Id,
                    IdOperacao = reg.IdOperacao,
                    IdCultura = reg.IdCultura,
                    IdModeloMaquina = reg.IdModeloMaquina,
                    Consumo = reg.Consumo,
                    Rendimento = reg.Rendimento
                };
            }
            else return null;
        }

        public async Task<ModeloParametroViewModel>? ListarModeloParametroById(int id, string idconta)
        {
            var reg = _context.modelosparametros.Where(m => m.Id == id && m.idconta == idconta).FirstOrDefault();
            if (reg != null)
            {
                return new ModeloParametroViewModel
                {
                    Id = reg.Id,
                    IdOperacao = reg.IdOperacao,
                    IdCultura = reg.IdCultura,
                    IdModeloMaquina = reg.IdModeloMaquina,
                    Consumo = reg.Consumo,
                    Rendimento = reg.Rendimento
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListModeloParametroViewModel>> ListarModeloParametroByModelo(int idmodelo, int idcultura, int idoperacao, string idconta)
        {
            var reg = _context.modelosparametros.Where(c => (idmodelo == 0 || c.IdModeloMaquina == idmodelo) && (c.idconta == idconta) &&
                     (idoperacao == 0 || c.IdOperacao == idoperacao) && (idcultura == 0 || c.IdCultura == idcultura))
                .Include(c => c.modeloMaquina)
                .ThenInclude(c => c.modelosparametros)
                .Include(c => c.cultura)
                .ThenInclude(c => c.modelosparametro)
                .Include(c => c.operacao)
                .Select(c => new ListModeloParametroViewModel
                {
                    Consumo = c.Consumo,
                    Rendimento = c.Rendimento,
                    Id = c.Id,
                    IdModeloMaquina = c.IdModeloMaquina,
                    IdCultura = c.IdCultura,
                    IdOperacao = c.IdOperacao,
                    DescCultura = c.cultura.Descricao,
                    DescModelo = c.modeloMaquina.Descricao,
                    DescOperacao = c.operacao.Descricao
                }
                ).ToList();
            return (reg);
        }
    }
}