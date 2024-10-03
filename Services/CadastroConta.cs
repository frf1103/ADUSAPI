using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.CustosIndiretos;
using FarmPlannerAPICore.Models.CustosIndiretos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;

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
            CadastroConta.uid = dados.uid;
            CadastroConta.datains = DateTime.Now;
            await _context.AddAsync(CadastroConta);
            await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Inclusão  da conta " + CadastroConta.Id.ToString() + "/" + CadastroConta.Descricao, datalog = DateTime.Now, idconta = dados.idconta }); ;
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

        public async Task<CadastroContaViewModel>? SalvarCadastroConta(int id, string idconta, CadastroContaViewModel dados)
        {
            var CadastroConta = _context.cadastrocontas.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (CadastroConta != null)
            {
                CadastroConta.Descricao = dados.Descricao;
                CadastroConta.CodigoCliente = dados.CodigoCliente;
                CadastroConta.CodigoExterno = dados.CodigoExterno;
                CadastroConta.IdGrupoConta = dados.IdGrupoConta;
                CadastroConta.IdGrupoConta = dados.IdGrupoConta;
                CadastroConta.idconta = dados.idconta;
                CadastroConta.uid = dados.uid;
                CadastroConta.dataup = DateTime.Now;

                _context.Update(CadastroConta);
                await _context.farmPlannerLogs.AddAsync(new FarmPlannerLog { uid = dados.uid, transacao = "Alteracao  da conta " + CadastroConta.Id.ToString() + "/" + CadastroConta.Descricao, datalog = DateTime.Now, idconta = dados.idconta }); ;
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

        public async Task<CadastroContaViewModel>? ExcluirCadastroConta(int id, string idconta, string uid)
        {
            var CadastroConta = _context.cadastrocontas.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
            if (CadastroConta != null)
            {
                CadastroContaViewModel dados = new CadastroContaViewModel
                {
                    Id = CadastroConta.Id
                };
                _excluirCadastroContaValidator.ValidateAndThrow(dados);
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

        public async Task<CadastroContaViewModel>? ListarCadastroContaById(int id, string idconta)
        {
            var CadastroConta = _context.cadastrocontas.Where(x => x.idconta == idconta && x.Id == id).FirstOrDefault();
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

        public async Task<IEnumerable<ListCadastroContaViewModel>> ListarCadastroContaByOrg(int idorganizacao, int idclasse, int idgrupo, string idconta, string? filtro)
        {
            var condicao = (CadastroConta m) => (m.IdGrupoConta == idgrupo && (m.grupoConta.IdClasseConta == idclasse || idclasse == 0) && m.idconta == idconta && m.grupoConta.IdOrganizacao == idorganizacao) && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
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

        /*

        public async Task<IEnumerable<ListCadastroContaViewModel>> ListaContasByFiltro(int idorganizacao, string idconta, string? filtro)
        {
            var condicao = (ClasseConta m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var cond1=(GrupoConta g)=> (String.IsNullOrWhiteSpace(filtro) || );
            var cond2= (CadastroConta c) => (String.IsNullOrWhiteSpace(filtro) || c.Descricao.ToUpper().Contains(filtro.ToUpper()));

            var classeconta = _context.classescontas.Include(c => c.grupoContas).
                Where(g => g.grupoContas.Any(g.Descricao.ToUpper().Contains(filtro.ToUpper())));

                .Select(c => new ClasseConta
                {
                    Descricao = c.Descricao,
                    Id = c.Id,
                    CodigoCliente = c.CodigoCliente,
                    CodigoExterno = c.CodigoExterno,
                    IdGrupoConta = c.IdGrupoConta,
                    Descgrupo = c.grupoConta.Descricao
                }
                ).ToList();
            return (classeconta);
        }
        */
    }
}