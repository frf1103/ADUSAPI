using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Operacao;
using FarmPlannerAPICore.Models.Operacao;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class OperacaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly OperacaoValidator _adicionarOperacaoValidator;
        private readonly ExcluirOperacaoValidator _excluirOperacaoValidator;

        public OperacaoService(FarmPlannerContext context, OperacaoValidator adicionarOperacaoValidator, ExcluirOperacaoValidator excluirOperacaoValidator)
        {
            _context = context;
            _adicionarOperacaoValidator = adicionarOperacaoValidator;
            _excluirOperacaoValidator = excluirOperacaoValidator;
        }

        public async Task<OperacaoViewModel> AdicionarOperacao(OperacaoViewModel dados)
        {
            _adicionarOperacaoValidator.ValidateAndThrow(dados);
            var Operacao = new Operacao();
            Operacao.Descricao = dados.Descricao;
            Operacao.Insumo = dados.Insumo;
            Operacao.IdTipoOperacao = dados.IdTipoOperacao;
            Operacao.CodigoExterno = dados.CodigoExterno;
            Operacao.Rendimento = dados.Rendimento;
            Operacao.Consumo = dados.Consumo;
            Operacao.idconta = dados.idconta;
            await _context.AddAsync(Operacao);
            await _context.SaveChangesAsync();
            return new OperacaoViewModel
            {
                Descricao = Operacao.Descricao,
                Insumo = Operacao.Insumo,
                IdTipoOperacao = Operacao.IdTipoOperacao,
                CodigoExterno = dados.CodigoExterno,
                Rendimento = dados.Rendimento,
                Consumo = dados.Consumo,
                idconta = dados.idconta,
                Id = Operacao.Id
            };
        }

        public async Task<OperacaoViewModel>? SalvarOperacao(int id, string idconta, OperacaoViewModel dados)
        {
            var Operacao = _context.operacoes.Find(id);
            if (Operacao != null)
            {
                Operacao.Descricao = dados.Descricao;
                Operacao.Insumo = dados.Insumo;
                Operacao.IdTipoOperacao = dados.IdTipoOperacao;
                Operacao.CodigoExterno = dados.CodigoExterno;
                Operacao.Rendimento = dados.Rendimento;
                Operacao.Consumo = dados.Consumo;
                Operacao.idconta = dados.idconta;

                _context.Update(Operacao);
                await _context.SaveChangesAsync();
                return new OperacaoViewModel
                {
                    Descricao = Operacao.Descricao,
                    Insumo = Operacao.Insumo,
                    IdTipoOperacao = Operacao.IdTipoOperacao,
                    CodigoExterno = Operacao.CodigoExterno,
                    Rendimento = Operacao.Rendimento,
                    Consumo = Operacao.Consumo,
                    Id = Operacao.Id,
                    idconta = Operacao.idconta
                };
            }
            else return null;
        }

        public async Task<OperacaoViewModel>? ExcluirOperacao(int id, string idconta)
        {
            var Operacao = _context.operacoes.Where(o => o.Id == id && o.idconta == idconta).FirstOrDefault();
            if (Operacao != null)
            {
                OperacaoViewModel dados = new OperacaoViewModel
                {
                    Id = Operacao.Id
                };
                _excluirOperacaoValidator.ValidateAndThrow(dados);
                _context.operacoes.Remove(Operacao);
                await _context.SaveChangesAsync();
                return new OperacaoViewModel
                {
                    Descricao = Operacao.Descricao,
                    Insumo = Operacao.Insumo,
                    IdTipoOperacao = Operacao.IdTipoOperacao,
                    CodigoExterno = Operacao.CodigoExterno,
                    Rendimento = Operacao.Rendimento,
                    Consumo = Operacao.Consumo,
                    Id = Operacao.Id,
                    idconta = Operacao.idconta
                };
            }
            else return null;
        }

        public async Task<OperacaoViewModel>? ListarOperacaoById(int id, string idconta)
        {
            var Operacao = _context.operacoes.Where(o => o.Id == id && o.idconta == idconta).FirstOrDefault();
            if (Operacao != null)
            {
                return new OperacaoViewModel
                {
                    Descricao = Operacao.Descricao,
                    Insumo = Operacao.Insumo,
                    IdTipoOperacao = Operacao.IdTipoOperacao,
                    CodigoExterno = Operacao.CodigoExterno,
                    Rendimento = Operacao.Rendimento,
                    Consumo = Operacao.Consumo,
                    Id = Operacao.Id,
                    idconta = Operacao.idconta
                };
            }
            else return null;
        }

        public async Task<IEnumerable<OperacaoViewModel>> ListarOperacao(string? filtro, string idconta, int idtipo)
        {
            var condicao = (Operacao m) => (m.idconta == idconta) && (idtipo == 0 || m.IdTipoOperacao == idtipo) && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.operacoes.AsQueryable();
            var Operacaos = query.Where(condicao)
                .Select(c => new OperacaoViewModel
                {
                    Descricao = c.Descricao,
                    Insumo = c.Insumo,
                    IdTipoOperacao = c.IdTipoOperacao,
                    CodigoExterno = c.CodigoExterno,
                    Rendimento = c.Rendimento,
                    Consumo = c.Consumo,
                    Id = c.Id,
                    idconta = c.idconta
                }
                ).ToList();
            return (Operacaos);
        }
    }
}