using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Talhao;
using FarmPlannerAPICore.Models.Talhao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class TalhaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly TalhaoValidator _adicionarTalhaoValidator;
        private readonly ExcluirTalhaoValidator _excluirTalhaoValidator;

        public TalhaoService(FarmPlannerContext context, TalhaoValidator adicionarTalhaoValidator, ExcluirTalhaoValidator excluirTalhaoValidator)
        {
            _context = context;
            _adicionarTalhaoValidator = adicionarTalhaoValidator;
            _excluirTalhaoValidator = excluirTalhaoValidator;
        }

        public async Task<EditarTalhaoViewModel> AdicionarTalhao(EditarTalhaoViewModel dados)
        {
            _adicionarTalhaoValidator.ValidateAndThrow(dados);
            var Talhao = new Talhao();
            Talhao.IdFazenda = dados.IdFazenda;
            Talhao.IdAnoAgricola = dados.IdAnoAgricola;
            Talhao.AreaProdutiva = dados.AreaProdutiva;
            Talhao.Descricao = dados.Descricao;
            Talhao.TipoArea = dados.TipoArea;
            Talhao.CodigoExterno = dados.CodigoExterno;
            Talhao.idconta = dados.idconta;
            Talhao.uid = dados.uid;
            Talhao.datains = DateTime.Now;
            await _context.AddAsync(Talhao);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão do Talhão " + Talhao.Descricao.Trim(), datalog = DateTime.Now, idconta = dados.idconta });
            await _context.SaveChangesAsync();
            return new EditarTalhaoViewModel
            {
                TipoArea = Talhao.TipoArea,
                IdFazenda = Talhao.IdFazenda,
                IdAnoAgricola = dados.IdAnoAgricola,
                Descricao = dados.Descricao,
                AreaProdutiva = dados.AreaProdutiva,
                Id = Talhao.Id,
                CodigoExterno = dados.CodigoExterno
            };
        }

        public async Task<EditarTalhaoViewModel>? SalvarTalhao(int id, string idconta, EditarTalhaoViewModel dados)
        {
            _adicionarTalhaoValidator.ValidateAndThrow(dados);
            var Talhao = _context.talhoes.Where(t => t.idconta == idconta && t.Id == id).FirstOrDefault();
            if (Talhao != null)
            {
                Talhao.IdFazenda = dados.IdFazenda;
                Talhao.IdAnoAgricola = dados.IdAnoAgricola;
                Talhao.AreaProdutiva = dados.AreaProdutiva;
                Talhao.Descricao = dados.Descricao;
                Talhao.TipoArea = dados.TipoArea;
                Talhao.CodigoExterno = dados.CodigoExterno;
                Talhao.dataup = DateTime.Now;
                Talhao.uid = dados.uid;
                _context.Update(Talhao);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteração do Talhão " + Talhao.Descricao.Trim(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new EditarTalhaoViewModel
                {
                    TipoArea = Talhao.TipoArea,
                    IdFazenda = Talhao.IdFazenda,
                    IdAnoAgricola = dados.IdAnoAgricola,
                    Descricao = dados.Descricao,
                    AreaProdutiva = dados.AreaProdutiva,
                    Id = Talhao.Id,
                    CodigoExterno = dados.CodigoExterno
                };
            }
            else return null;
        }

        public async Task<EditarTalhaoViewModel>? ExcluirTalhao(int id, string idconta, string uid)
        {
            ;
            var Talhao = _context.talhoes.Where(t => t.idconta == idconta && t.Id == id).FirstOrDefault();
            if (Talhao != null)
            {
                EditarTalhaoViewModel dados = new EditarTalhaoViewModel
                {
                    Id = Talhao.Id
                };
                _excluirTalhaoValidator.ValidateAndThrow(dados);
                _context.talhoes.Remove(Talhao);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão do Talhão " + Talhao.Descricao.Trim(), datalog = DateTime.Now, idconta = idconta });
                await _context.SaveChangesAsync();
                return new EditarTalhaoViewModel
                {
                    TipoArea = Talhao.TipoArea,
                    IdFazenda = Talhao.IdFazenda,
                    IdAnoAgricola = dados.IdAnoAgricola,
                    Descricao = dados.Descricao,
                    AreaProdutiva = dados.AreaProdutiva,
                    CodigoExterno = dados.CodigoExterno
                };
            }
            else return null;
        }

        public async Task<EditarTalhaoViewModel>? ListarTalhaoById(int id, string idconta)
        {
            var Talhao = _context.talhoes.Where(t => t.Id == id && t.idconta == idconta).FirstOrDefault();
            if (Talhao != null)
            {
                return new EditarTalhaoViewModel
                {
                    TipoArea = Talhao.TipoArea,
                    IdFazenda = Talhao.IdFazenda,
                    IdAnoAgricola = Talhao.IdAnoAgricola,
                    Descricao = Talhao.Descricao,
                    AreaProdutiva = Talhao.AreaProdutiva,
                    CodigoExterno = Talhao.CodigoExterno,
                    Id = Talhao.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<EditarTalhaoViewModel>> ListarTalhaoByFazenda(int idfazenda, string idconta, int idano, string? filtro)
        {
            var condicao = (Talhao m) => m.idconta == idconta && m.IdFazenda == idfazenda && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.talhoes.AsQueryable();
            var Talhoes = query.Where(condicao)
                .Select(c => new EditarTalhaoViewModel
                {
                    TipoArea = c.TipoArea,
                    IdFazenda = c.IdFazenda,
                    IdAnoAgricola = c.IdAnoAgricola,
                    Descricao = c.Descricao,
                    AreaProdutiva = c.AreaProdutiva,
                    CodigoExterno = c.CodigoExterno,
                    Id = c.Id
                }
                ).ToList();
            return (Talhoes);
        }

        public async Task<IEnumerable<EditarTalhaoViewModel>> ListarTalhaoDisponivel(int idfazenda, string idconta, int idano)
        {
            var result = _context.talhoes.Where(t => t.idconta == idconta && t.IdAnoAgricola == idano && t.IdFazenda == idfazenda)
            .GroupJoin(
                _context.configareas,
                talhao => talhao.Id,
                configArea => configArea.IdTalhao,
                (talhao, configAreas) => new
                {
                    Talhao = talhao,
                    AreaConfigurada = configAreas.Sum(ca => (decimal?)ca.Area) ?? 0
                }
            )
            .Select(g => new EditarTalhaoViewModel
            {
                Id = g.Talhao.Id,
                Descricao = g.Talhao.Descricao,
                areaDisp = g.Talhao.AreaProdutiva - g.AreaConfigurada,
                AreaProdutiva = g.Talhao.AreaProdutiva,
                TipoArea = g.Talhao.TipoArea,
                IdFazenda = g.Talhao.IdFazenda,
                IdAnoAgricola = g.Talhao.IdAnoAgricola,
                CodigoExterno = g.Talhao.CodigoExterno
            })
            //.Where(g => g.areaDisp > 0)
            .ToList();
            if (result != null)
            {
                return ((IEnumerable<EditarTalhaoViewModel>)result);
            }
            else
            {
                return null;
            }
        }
    }
}