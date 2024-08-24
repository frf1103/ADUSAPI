using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.CustosIndiretos;
using FarmPlannerAPICore.Models.CustosIndiretos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class GrupoContaService
    {
        private readonly FarmPlannerContext _context;
        private readonly GrupoContaValidator _adicionarGrupoContaValidator;
        private readonly ExcluirGrupoContaValidator _excluirGrupoContaValidator;

        public GrupoContaService(FarmPlannerContext context, GrupoContaValidator adicionarGrupoContaValidator, ExcluirGrupoContaValidator excluirGrupoContaValidator)
        {
            _context = context;
            _adicionarGrupoContaValidator = adicionarGrupoContaValidator;
            _excluirGrupoContaValidator = excluirGrupoContaValidator;
        }

        public async Task<GrupoContaViewModel> AdicionarGrupoConta(GrupoContaViewModel dados)
        {
            _adicionarGrupoContaValidator.ValidateAndThrow(dados);
            var GrupoConta = new GrupoConta();
            GrupoConta.Descricao = dados.Descricao;
            GrupoConta.IdClasseConta = dados.IdClasseConta;
            GrupoConta.IdOrganizacao = dados.IdOrganizacao;
            GrupoConta.CodigoCliente = dados.CodigoCliente;
            GrupoConta.CodigoExterno = dados.CodigoExterno;
            GrupoConta.idconta = dados.idconta;

            await _context.AddAsync(GrupoConta);
            await _context.SaveChangesAsync();
            return new GrupoContaViewModel
            {
                Descricao = GrupoConta.Descricao,
                CodigoCliente = GrupoConta.CodigoCliente,
                CodigoExterno = GrupoConta.CodigoExterno,
                IdClasseConta = GrupoConta.IdClasseConta,
                IdOrganizacao = GrupoConta.IdOrganizacao,
                Id = GrupoConta.Id
            };
        }

        public async Task<GrupoContaViewModel>? SalvarGrupoConta(int id, GrupoContaViewModel dados)
        {
            var GrupoConta = _context.gruposcontas.Find(id);
            if (GrupoConta != null)
            {
                GrupoConta.Descricao = dados.Descricao;
                GrupoConta.IdClasseConta = dados.IdClasseConta;
                GrupoConta.IdOrganizacao = dados.IdOrganizacao;
                GrupoConta.CodigoCliente = dados.CodigoCliente;
                GrupoConta.CodigoExterno = dados.CodigoExterno;

                _context.Update(GrupoConta);
                await _context.SaveChangesAsync();
                return new GrupoContaViewModel
                {
                    Descricao = GrupoConta.Descricao,
                    CodigoCliente = GrupoConta.CodigoCliente,
                    CodigoExterno = GrupoConta.CodigoExterno,
                    IdClasseConta = GrupoConta.IdClasseConta,
                    IdOrganizacao = GrupoConta.IdOrganizacao,
                    Id = GrupoConta.Id
                };
            }
            else return null;
        }

        public async Task<GrupoContaViewModel>? ExcluirGrupoConta(int id, GrupoContaViewModel dados)
        {
            _excluirGrupoContaValidator.ValidateAndThrow(dados);
            var GrupoConta = _context.gruposcontas.Find(id);
            if (GrupoConta != null)
            {
                _context.gruposcontas.Remove(GrupoConta);
                await _context.SaveChangesAsync();
                return new GrupoContaViewModel
                {
                    Descricao = GrupoConta.Descricao,
                    CodigoCliente = GrupoConta.CodigoCliente,
                    CodigoExterno = GrupoConta.CodigoExterno,
                    IdClasseConta = GrupoConta.IdClasseConta,
                    IdOrganizacao = GrupoConta.IdOrganizacao,
                    Id = GrupoConta.Id
                };
            }
            else return null;
        }

        public async Task<GrupoContaViewModel>? ListarGrupoContaById(int id)
        {
            var GrupoConta = _context.tiposoperacao.Find(id);
            if (GrupoConta != null)
            {
                return new GrupoContaViewModel
                {
                    Descricao = GrupoConta.Descricao,
                    Id = GrupoConta.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListGrupoContaViewModel>> ListarGrupoConta(int idorganizacao, int idclasse, string? filtro)
        {
            var condicao = (GrupoConta m) => (m.IdOrganizacao == idorganizacao && (idclasse == 0 || m.IdClasseConta == idclasse) && String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.gruposcontas.Include(m => m.organizacao).Include(m => m.classeConta);
            var GrupoContas = query.Where(condicao)
                .Select(c => new ListGrupoContaViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    IdClasseConta = c.IdClasseConta,
                    IdOrganizacao = c.IdOrganizacao,
                    CodigoCliente = c.CodigoCliente,
                    CodigoExterno = c.CodigoExterno,
                    DescOrganizacao = c.organizacao.Nome,
                    DescClasse = c.classeConta.Descricao
                }
                ).ToList();
            return (GrupoContas);
        }
    }
}