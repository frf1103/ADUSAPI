using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.ProdutoPlanejado;
using FarmPlannerAPICore.Models.ProdutoPlanejado;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FarmPlannerAPI.Services
{
    public class ProdutoPlanejadoService
    {
        private readonly FarmPlannerContext _context;
        private readonly ProdutoPlanejadoValidator _adicionarProdutoPlanejadoValidator;
        private readonly ExcluirProdutoPlanejadoValidator _excluirProdutoPlanejadoValidator;

        public ProdutoPlanejadoService(FarmPlannerContext context, ProdutoPlanejadoValidator adicionarProdutoPlanejadoValidator, ExcluirProdutoPlanejadoValidator excluirProdutoPlanejadoValidator)
        {
            _context = context;
            _adicionarProdutoPlanejadoValidator = adicionarProdutoPlanejadoValidator;
            _excluirProdutoPlanejadoValidator = excluirProdutoPlanejadoValidator;
        }

        public async Task<(ProdutoPlanejadoViewModel, List<string> listerros)> AdicionarProdutoPlanejado(ProdutoPlanejadoViewModel dados)
        {
            var validationErrors = new List<ValidationResult>();
            _adicionarProdutoPlanejadoValidator.ValidateAndThrow(dados);

            var errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
            if (errorMessages.Count == 0)
            {
                var ProdutoPlanejado = new ProdutoPlanejado();

                ProdutoPlanejado.IdProduto = dados.IdProduto;
                ProdutoPlanejado.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                ProdutoPlanejado.Dosagem = dados.Dosagem;
                ProdutoPlanejado.AreaPercent = dados.AreaPercent;
                ProdutoPlanejado.Tamanho = dados.Tamanho;
                ProdutoPlanejado.TotalProduto = dados.TotalProduto;
                ProdutoPlanejado.IdPlanejamento = dados.IdPlanejamento;
                ProdutoPlanejado.idconta = dados.idconta;
                ProdutoPlanejado.uid = dados.uid;
                ProdutoPlanejado.datains = DateTime.Now;

                await _context.AddAsync(ProdutoPlanejado);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  produto do Planejamento de operacoes " + ProdutoPlanejado.IdPlanejamento.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString() + "/" + ProdutoPlanejado.IdPrincipioAtivo ?? 0.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return (new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                }, null);
            }
            else
            {
                errorMessages = validationErrors.Select(e => e.ErrorMessage).ToList();
                return (null, errorMessages);
            }
        }

        public async Task<ProdutoPlanejadoViewModel>? SalvarProdutoPlanejado(int id, ProdutoPlanejadoViewModel dados)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Find(id);
            if (ProdutoPlanejado != null)
            {
                ProdutoPlanejado.IdProduto = dados.IdProduto;
                ProdutoPlanejado.IdPrincipioAtivo = dados.IdPrincipioAtivo;
                ProdutoPlanejado.Dosagem = dados.Dosagem;
                ProdutoPlanejado.AreaPercent = dados.AreaPercent;
                ProdutoPlanejado.Tamanho = dados.Tamanho;
                ProdutoPlanejado.TotalProduto = dados.TotalProduto;
                ProdutoPlanejado.IdPlanejamento = dados.IdPlanejamento;
                ProdutoPlanejado.uid = dados.uid;
                ProdutoPlanejado.dataup = DateTime.Now;

                _context.Update(ProdutoPlanejado);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Altereção do  produto do Planejamento de operacoes " + ProdutoPlanejado.IdPlanejamento.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString() + "/" + ProdutoPlanejado.IdPrincipioAtivo ?? 0.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<ProdutoPlanejadoViewModel>? ExcluirProdutoPlanejado(int id, string uid, string idconta)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (ProdutoPlanejado != null)
            {
                ProdutoPlanejadoViewModel dados = new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id
                };
                _excluirProdutoPlanejadoValidator.ValidateAndThrow(dados);
                _context.produtoplanejados.Remove(ProdutoPlanejado);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exlusão do  produto do Planejamento de operacoes " + ProdutoPlanejado.IdPlanejamento.ToString() + "/" + ProdutoPlanejado.IdProduto ?? 0.ToString() + "/" + ProdutoPlanejado.IdPrincipioAtivo ?? 0.ToString(), datalog = DateTime.Now, idconta = dados.idconta });
                await _context.SaveChangesAsync();
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<ProdutoPlanejadoViewModel>? ListarProdutoPlanejadoById(int id, string idconta)
        {
            var ProdutoPlanejado = _context.produtoplanejados.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (ProdutoPlanejado != null)
            {
                return new ProdutoPlanejadoViewModel
                {
                    Id = ProdutoPlanejado.Id,
                    IdProduto = ProdutoPlanejado.IdProduto,
                    IdPrincipioAtivo = ProdutoPlanejado.IdPrincipioAtivo,
                    Dosagem = ProdutoPlanejado.Dosagem,
                    AreaPercent = ProdutoPlanejado.AreaPercent,
                    Tamanho = ProdutoPlanejado.Tamanho,
                    TotalProduto = ProdutoPlanejado.TotalProduto,
                    IdPlanejamento = ProdutoPlanejado.IdPlanejamento
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListProdutoPlanejadoViewModel>> ListarProdutoPlanejadoByPlanejamento(int idplanejamento, string idconta)
        {
            var query = _context.produtoplanejados
                .Include(m => m.produto).Include(m => m.principioativo)
                .Where(m => (m.IdPlanejamento == idplanejamento && m.idconta == idconta));
            var ProdutoPlanejados = query
                .Select(c => new ListProdutoPlanejadoViewModel
                {
                    Id = c.Id,
                    IdProduto = c.IdProduto,
                    IdPrincipioAtivo = c.IdPrincipioAtivo,
                    Dosagem = c.Dosagem,
                    AreaPercent = c.AreaPercent,
                    Tamanho = c.Tamanho,
                    TotalProduto = c.TotalProduto,
                    IdPlanejamento = c.IdPlanejamento,
                    descprincativo = c.principioativo.Descricao,
                    descproduto = c.produto.Descricao,
                    descricao = (c.IdProduto == null) ? c.principioativo.Descricao : c.produto.Descricao,
                    idcodigo = (int)((c.IdProduto == null) ? c.IdPrincipioAtivo : c.IdProduto),
                    idtipo = (c.IdProduto == null) ? "PA" : "PR"
                }
                ).ToList();
            return (ProdutoPlanejados);
        }
    }
}