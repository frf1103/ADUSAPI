using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.PrincipioAtivo;

//using FarmPlannerAPI.Validators.PrincipioAtivo;
using FarmPlannerAPICore.Models.PrincipioAtivo;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class PrincipioAtivoService
    {
        private readonly FarmPlannerContext _context;
        private readonly PrincipioAtivoValidator _adicionarPrincipioAtivoValidator;
        private readonly ExcluirPrincipioAtivoValidator _excluirPrincipioAtivoValidator;

        public PrincipioAtivoService(FarmPlannerContext context, PrincipioAtivoValidator adicionarPrincipioAtivoValidator, ExcluirPrincipioAtivoValidator excluirPrincipioAtivoValidator)
        {
            _context = context;
            _adicionarPrincipioAtivoValidator = adicionarPrincipioAtivoValidator;
            _excluirPrincipioAtivoValidator = excluirPrincipioAtivoValidator;
        }

        public async Task<PrincipioAtivoViewModel> AdicionarPrincipioAtivo(PrincipioAtivoViewModel dados)
        {
            _adicionarPrincipioAtivoValidator.ValidateAndThrow(dados);
            var PrincipioAtivo = new PrincipioAtivo();
            PrincipioAtivo.Descricao = dados.Descricao;
            PrincipioAtivo.idconta = dados.idconta;

            await _context.AddAsync(PrincipioAtivo);
            await _context.SaveChangesAsync();
            return new PrincipioAtivoViewModel
            {
                Descricao = PrincipioAtivo.Descricao,

                Id = PrincipioAtivo.Id,
                idconta = PrincipioAtivo.idconta
            };
        }

        public async Task<PrincipioAtivoViewModel>? SalvarPrincipioAtivo(int id, string idconta, PrincipioAtivoViewModel dados)
        {
            var PrincipioAtivo = _context.principioAtivos.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (PrincipioAtivo != null)
            {
                PrincipioAtivo.Descricao = dados.Descricao;

                _context.Update(PrincipioAtivo);
                await _context.SaveChangesAsync();
                return new PrincipioAtivoViewModel
                {
                    Descricao = PrincipioAtivo.Descricao,
                    Id = PrincipioAtivo.Id,
                    idconta = PrincipioAtivo.idconta
                };
            }
            else return null;
        }

        public async Task<PrincipioAtivoViewModel>? ExcluirPrincipioAtivo(int id, string idconta)
        {
            var PrincipioAtivo = _context.principioAtivos.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (PrincipioAtivo != null)
            {
                PrincipioAtivoViewModel dados = new PrincipioAtivoViewModel
                {
                    Id = PrincipioAtivo.Id,
                    Descricao = PrincipioAtivo.Descricao
                };
                _excluirPrincipioAtivoValidator.ValidateAndThrow(dados);
                _context.principioAtivos.Remove(PrincipioAtivo);
                await _context.SaveChangesAsync();
                return new PrincipioAtivoViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id,
                    idconta = PrincipioAtivo.idconta
                };
            }
            else return null;
        }

        public async Task<PrincipioAtivoViewModel>? ListarPrincipioAtivoById(int id, string idconta)
        {
            var PrincipioAtivo = _context.principioAtivos.Where(p => p.Id == id && p.idconta == idconta).FirstOrDefault();
            if (PrincipioAtivo != null)
            {
                return new PrincipioAtivoViewModel
                {
                    Descricao = PrincipioAtivo.Descricao,
                    Id = PrincipioAtivo.Id,
                    idconta = PrincipioAtivo.idconta
                };
            }
            else return null;
        }

        public async Task<IEnumerable<PrincipioAtivoViewModel>> ListarPrincipioAtivo(string idconta, string? filtro)
        {
            var condicao = (PrincipioAtivo m) => m.idconta == idconta && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.principioAtivos.AsQueryable();
            var PrincipioAtivos = query.Where(condicao)
                .Select(c => new PrincipioAtivoViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    idconta = c.idconta
                }
                ).ToList();
            return (PrincipioAtivos);
        }
    }
}