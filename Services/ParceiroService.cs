using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Parceiro;
using FarmPlannerAPICore.Models.Parceiro;
using FluentValidation;
using FBSLIb;
using Microsoft.Win32;

namespace FarmPlannerAPI.Services
{
    public class ParceiroService
    {
        private readonly FarmPlannerContext _context;
        private readonly ParceiroValidator _adicionarParceiroValidator;
        private readonly ExcluirParceiroValidator _excluirParceiroValidator;

        public ParceiroService(FarmPlannerContext context, ParceiroValidator adicionarParceiroValidator, ExcluirParceiroValidator excluirParceiroValidator)
        {
            _context = context;
            _adicionarParceiroValidator = adicionarParceiroValidator;
            _excluirParceiroValidator = excluirParceiroValidator;
        }

        public async Task<ParceiroViewModel> AdicionarParceiro(ParceiroViewModel dados)
        {
            _adicionarParceiroValidator.ValidateAndThrow(dados);
            var conta = new Parceiro();
            conta.Fantasia = dados.Fantasia;
            conta.RazaoSocial = dados.RazaoSocial;
            conta.TipodePessoa = dados.TipodePessoa;
            conta.Registro = FBSLIb.StringLib.Somentenumero(dados.Registro.ToString());
            conta.Fantasia = dados.Fantasia;
            conta.idconta = dados.idconta;
            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new ParceiroViewModel
            {
                RazaoSocial = conta.RazaoSocial,
                Id = conta.Id,
                Registro = conta.Registro,
                Fantasia = conta.Fantasia,
                TipodePessoa = conta.TipodePessoa,
                idconta = conta.idconta
            };
        }

        public async Task<ParceiroViewModel>? SalvarParceiro(int id, string idconta, ParceiroViewModel dados)
        {
            _adicionarParceiroValidator.ValidateAndThrow(dados);
            var conta = _context.parceiros.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (conta != null)
            {
                conta.TipodePessoa = dados.TipodePessoa;

                conta.RazaoSocial = dados.RazaoSocial;
                conta.Registro = FBSLIb.StringLib.Somentenumero(dados.Registro.ToString());

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new ParceiroViewModel
                {
                    RazaoSocial = conta.RazaoSocial,

                    Id = conta.Id,
                    Registro = conta.Registro,
                    Fantasia = conta.Fantasia,
                    TipodePessoa = conta.TipodePessoa,
                    idconta = conta.idconta
                };
            }
            else return null;
        }

        public async Task<ParceiroViewModel>? ExcluirParceiro(int id, string idconta)
        {
            var conta = _context.parceiros.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (conta != null)
            {
                ParceiroViewModel dados = new ParceiroViewModel
                {
                    Id = conta.Id,
                    Fantasia = conta.Fantasia,
                    RazaoSocial = conta.RazaoSocial,
                    Registro = conta.Registro,
                    TipodePessoa = conta.TipodePessoa
                };
                _excluirParceiroValidator.ValidateAndThrow(dados);
                _context.parceiros.Remove(conta);
                await _context.SaveChangesAsync();
                return new ParceiroViewModel
                {
                    RazaoSocial = conta.RazaoSocial,

                    Id = conta.Id,
                    Registro = conta.Registro,
                    Fantasia = conta.Fantasia,
                    TipodePessoa = conta.TipodePessoa,
                    idconta = conta.idconta
                };
            }
            else return null;
        }

        public async Task<ParceiroViewModel>? ListarParceiroById(int id, string idconta)
        {
            var conta = _context.parceiros.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (conta != null)
            {
                return new ParceiroViewModel
                {
                    RazaoSocial = conta.RazaoSocial,

                    Id = conta.Id,
                    Registro = conta.Registro,
                    Fantasia = conta.Fantasia,
                    TipodePessoa = conta.TipodePessoa,
                    idconta = conta.idconta
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListParceiroViewModel>> ListarParceiro(string idconta, string? filtro)
        {
            var condicao = (Parceiro m) => (m.idconta == idconta) && ((String.IsNullOrWhiteSpace(filtro) || m.RazaoSocial.ToUpper().Contains(filtro.ToUpper())) || (String.IsNullOrWhiteSpace(filtro) || m.Fantasia.ToUpper().Contains(filtro.ToUpper())) ||
            (String.IsNullOrWhiteSpace(filtro) || m.Registro.ToUpper().Contains(filtro.ToUpper())));
            var query = _context.parceiros.AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new ListParceiroViewModel
                {
                    RazaoSocial = c.RazaoSocial,
                    Fantasia = c.Fantasia,
                    Id = c.Id,
                    Registro = c.Registro,
                    TipodePessoa = c.TipodePessoa,
                    desctipo = c.TipodePessoa.ToString(),
                    idconta = c.idconta
                }
                ).ToList();
            return (contas);
        }
    }
}