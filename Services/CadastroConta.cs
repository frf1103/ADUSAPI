using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.CustosIndiretos;
using FarmPlannerAPICore.Models.CustosIndiretos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FarmPlannerAPI.Services
{
    public class CadastroContaService
    {
        private readonly FarmPlannerContext _context;
        private readonly CadastroContaValidator _adicionarCadastroContaValidator;
        private readonly ExcluirCadastroContaValidator _excluirCadastroContaValidator;

        public CadastroContaService(FarmPlannerContext context, CadastroContaValidator adicionarCadastroContaValidator, ExcluirCadastroContaValidator excluirCadastroContaValidator)
        {
            _context = context;
            _adicionarCadastroContaValidator = adicionarCadastroContaValidator;
            _excluirCadastroContaValidator = excluirCadastroContaValidator;
        }

        public async Task<CadastroContaViewModel> AdicionarCadastroConta(CadastroContaViewModel dados)
        {
            _adicionarCadastroContaValidator.ValidateAndThrow(dados);
            var CadastroConta = new CadastroConta();
            CadastroConta.Descricao = dados.Descricao;
            CadastroConta.CodigoCliente = dados.CodigoCliente;
            CadastroConta.CodigoExterno = dados.CodigoExterno;
            CadastroConta.IdGrupoConta = dados.IdGrupoConta;
            CadastroConta.idconta = dados.idconta;

            await _context.AddAsync(CadastroConta);
            await _context.SaveChangesAsync();
            return new CadastroContaViewModel
            {
                Descricao = CadastroConta.Descricao,
                Id = CadastroConta.Id,
                CodigoCliente = CadastroConta.CodigoCliente,
                CodigoExterno = CadastroConta.CodigoExterno,
                IdGrupoConta = CadastroConta.IdGrupoConta
            };
        }

        public async Task<CadastroContaViewModel>? SalvarCadastroConta(int id, CadastroContaViewModel dados)
        {
            var CadastroConta = _context.cadastrocontas.Find(id);
            if (CadastroConta != null)
            {
                CadastroConta.Descricao = dados.Descricao;
                CadastroConta.CodigoCliente = dados.CodigoCliente;
                CadastroConta.CodigoExterno = dados.CodigoExterno;
                CadastroConta.IdGrupoConta = dados.IdGrupoConta;

                _context.Update(CadastroConta);
                await _context.SaveChangesAsync();
                return new CadastroContaViewModel
                {
                    Descricao = CadastroConta.Descricao,
                    Id = CadastroConta.Id,
                    CodigoCliente = CadastroConta.CodigoCliente,
                    CodigoExterno = CadastroConta.CodigoExterno,
                    IdGrupoConta = CadastroConta.IdGrupoConta
                };
            }
            else return null;
        }

        public async Task<CadastroContaViewModel>? ExcluirCadastroConta(int id, CadastroContaViewModel dados)
        {
            _excluirCadastroContaValidator.ValidateAndThrow(dados);
            var CadastroConta = _context.cadastrocontas.Find(id);
            if (CadastroConta != null)
            {
                _context.cadastrocontas.Remove(CadastroConta);
                await _context.SaveChangesAsync();
                return new CadastroContaViewModel
                {
                    Descricao = CadastroConta.Descricao,
                    Id = CadastroConta.Id,
                    CodigoCliente = CadastroConta.CodigoCliente,
                    CodigoExterno = CadastroConta.CodigoExterno,
                    IdGrupoConta = CadastroConta.IdGrupoConta
                };
            }
            else return null;
        }

        public async Task<CadastroContaViewModel>? ListarCadastroContaById(int id)
        {
            var CadastroConta = _context.cadastrocontas.Find(id);
            if (CadastroConta != null)
            {
                return new CadastroContaViewModel
                {
                    Descricao = CadastroConta.Descricao,
                    Id = CadastroConta.Id,
                    CodigoCliente = CadastroConta.CodigoCliente,
                    CodigoExterno = CadastroConta.CodigoExterno,
                    IdGrupoConta = CadastroConta.IdGrupoConta
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListCadastroContaViewModel>> ListarCadastroContaByOrg(int idorganizacao, string? filtro)
        {
            var condicao = (CadastroConta m) => (m.grupoConta.IdOrganizacao == idorganizacao) && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var CadastroContas = _context.cadastrocontas.Include(m => m.grupoConta).Where(condicao)
                .Select(c => new ListCadastroContaViewModel
                {
                    Descricao = c.Descricao,
                    Id = c.Id,
                    CodigoCliente = c.CodigoCliente,
                    CodigoExterno = c.CodigoExterno,
                    IdGrupoConta = c.IdGrupoConta,
                    Descgrupo = c.grupoConta.Descricao
                }
                ).ToList();
            return (CadastroContas);
        }
    }
}