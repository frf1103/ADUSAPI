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
            GrupoConta.uid = dados.uid;
            GrupoConta.datains = DateTime.Now;
            await _context.AddAsync(GrupoConta);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  grupo de conta " + GrupoConta.Id.ToString() + "/" + GrupoConta.Descricao, datalog = DateTime.Now, idconta = dados.idconta }); ;
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

        public async Task<GrupoContaViewModel>? SalvarGrupoConta(int id, string idconta, GrupoContaViewModel dados)
        {
            var GrupoConta = _context.gruposcontas.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (GrupoConta != null)
            {
                GrupoConta.Descricao = dados.Descricao;
                GrupoConta.IdClasseConta = dados.IdClasseConta;
                GrupoConta.IdOrganizacao = dados.IdOrganizacao;
                GrupoConta.CodigoCliente = dados.CodigoCliente;
                GrupoConta.CodigoExterno = dados.CodigoExterno;

                _context.Update(GrupoConta);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteracao  grupo de conta " + GrupoConta.Id.ToString() + "/" + GrupoConta.Descricao, datalog = DateTime.Now, idconta = dados.idconta }); ;
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

        public async Task<GrupoContaViewModel>? ExcluirGrupoConta(int id, string idconta, string uid)
        {
            var GrupoConta = _context.gruposcontas.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (GrupoConta != null)
            {
                var dados = new GrupoContaViewModel
                {
                    Id = GrupoConta.Id
                };
                _excluirGrupoContaValidator.ValidateAndThrow(dados);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = uid, transacao = "Exclusão  grupo de conta " + GrupoConta.Id.ToString() + "/" + GrupoConta.Descricao, datalog = DateTime.Now, idconta = idconta }); ;
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

        public async Task<GrupoContaViewModel>? ListarGrupoContaById(int id, string idconta)
        {
            var GrupoConta = _context.gruposcontas.Where(x => x.Id == id && x.idconta == idconta).FirstOrDefault();
            if (GrupoConta != null)
            {
                return new GrupoContaViewModel
                {
                    Descricao = GrupoConta.Descricao,
                    Id = GrupoConta.Id,
                    CodigoCliente=GrupoConta.CodigoCliente,
                    CodigoExterno=GrupoConta.CodigoExterno,
                    IdClasseConta=GrupoConta.IdClasseConta,
                    IdOrganizacao=GrupoConta.IdOrganizacao,
                    idconta=GrupoConta.idconta,
                    uid=GrupoConta.uid

                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListGrupoContaViewModel>> ListarGrupoConta(int idorganizacao, int idclasse, string idconta, string? filtro)
        {
            var condicao = (GrupoConta m) => (m.idconta == idconta && m.IdOrganizacao == idorganizacao && (idclasse == 0 || m.IdClasseConta == idclasse));
            //&& String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
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