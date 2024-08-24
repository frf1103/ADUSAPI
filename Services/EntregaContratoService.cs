using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Comercializacao;
using FarmPlannerAPICore.Models.Comercializacao;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class EntregaContratoService
    {
        private readonly FarmPlannerContext _context;
        private readonly EntregaContratoValidator _adicionarEntregaContratoValidator;
        private readonly ExcluirEntregaContratoValidator _excluirEntregaContratoValidator;

        public EntregaContratoService(FarmPlannerContext context, EntregaContratoValidator adicionarEntregaContratoValidator, ExcluirEntregaContratoValidator excluirEntregaContratoValidator)
        {
            _context = context;
            _adicionarEntregaContratoValidator = adicionarEntregaContratoValidator;
            _excluirEntregaContratoValidator = excluirEntregaContratoValidator;
        }

        public async Task<EntregaContratoViewModel> AdicionarEntregaContrato(EntregaContratoViewModel dados)
        {
            _adicionarEntregaContratoValidator.ValidateAndThrow(dados);
            var EntregaContrato = new EntregaContrato();
            EntregaContrato.DataEntrega = dados.DataEntrega;
            EntregaContrato.Quantidade = dados.Quantidade;
            EntregaContrato.Documento = dados.Documento;
            EntregaContrato.IdComercializacao = dados.IdComercializacao;
            EntregaContrato.idconta = dados.idconta;

            await _context.AddAsync(EntregaContrato);
            await _context.SaveChangesAsync();
            return new EntregaContratoViewModel
            {
                DataEntrega = EntregaContrato.DataEntrega,
                Documento = EntregaContrato.Documento,
                IdComercializacao = EntregaContrato.IdComercializacao,
                Quantidade = EntregaContrato.Quantidade,
                Id = EntregaContrato.Id
            };
        }

        public async Task<EntregaContratoViewModel>? SalvarEntregaContrato(int id, EntregaContratoViewModel dados)
        {
            _adicionarEntregaContratoValidator.ValidateAndThrow(dados);
            var EntregaContrato = _context.entregaContratos.Find(id);
            if (EntregaContrato != null)
            {
                EntregaContrato.DataEntrega = dados.DataEntrega;
                EntregaContrato.Quantidade = dados.Quantidade;
                EntregaContrato.Documento = dados.Documento;
                EntregaContrato.IdComercializacao = dados.IdComercializacao;

                _context.Update(EntregaContrato);
                await _context.SaveChangesAsync();
                return new EntregaContratoViewModel
                {
                    DataEntrega = EntregaContrato.DataEntrega,
                    Documento = EntregaContrato.Documento,
                    IdComercializacao = EntregaContrato.IdComercializacao,
                    Quantidade = EntregaContrato.Quantidade,
                    Id = EntregaContrato.Id
                };
            }
            else return null;
        }

        public async Task<EntregaContratoViewModel>? ExcluirEntregaContrato(int id, EntregaContratoViewModel dados)
        {
            _excluirEntregaContratoValidator.ValidateAndThrow(dados);
            var EntregaContrato = _context.entregaContratos.Find(id);
            if (EntregaContrato != null)
            {
                _context.entregaContratos.Remove(EntregaContrato);
                await _context.SaveChangesAsync();
                return new EntregaContratoViewModel
                {
                    DataEntrega = EntregaContrato.DataEntrega,
                    Documento = EntregaContrato.Documento,
                    IdComercializacao = EntregaContrato.IdComercializacao,
                    Quantidade = EntregaContrato.Quantidade,
                    Id = EntregaContrato.Id
                };
            }
            else return null;
        }

        public async Task<EntregaContratoViewModel>? ListarEntregaContratoById(int id)
        {
            var EntregaContrato = _context.entregaContratos.Find(id);
            if (EntregaContrato != null)
            {
                return new EntregaContratoViewModel
                {
                    DataEntrega = EntregaContrato.DataEntrega,
                    Documento = EntregaContrato.Documento,
                    IdComercializacao = EntregaContrato.IdComercializacao,
                    Quantidade = EntregaContrato.Quantidade,
                    Id = EntregaContrato.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListEntregaContratoViewModel>> ListarEntregaContratoByCom(int idcomercializacao, string? filtro)
        {
            var condicao = (EntregaContrato m) => (m.IdComercializacao == idcomercializacao) && (String.IsNullOrWhiteSpace(filtro) || m.Documento.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.entregaContratos.AsQueryable();
            var EntregaContratos = query.Where(condicao)
                .Select(c => new ListEntregaContratoViewModel
                {
                    DataEntrega = c.DataEntrega,
                    Documento = c.Documento,
                    IdComercializacao = c.IdComercializacao,
                    Quantidade = c.Quantidade,
                    Id = c.Id
                }
                ).ToList();
            return (EntregaContratos);
        }
    }
}