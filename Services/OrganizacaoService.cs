using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Organizacao;
using FarmPlannerAPICore.Models.Organizacao;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FarmPlannerAPI.Services
{
    public class OrganizacaoService
    {
        private readonly FarmPlannerContext _context;
        private readonly AdicionarOrganizacaoValidator _adicionarOrganizacaoValidator;
        private readonly EditarOrganizacaoValidator _editarOrganizacaoValidator;

        public OrganizacaoService(FarmPlannerContext context, AdicionarOrganizacaoValidator adicionarOrganizacaoValidator, EditarOrganizacaoValidator editarOrganizacaoValidator)
        {
            _context = context;
            _adicionarOrganizacaoValidator = adicionarOrganizacaoValidator;
            _editarOrganizacaoValidator = editarOrganizacaoValidator;
        }

        // Organização
        public async Task<EditarOrganizacaoViewModel> AdicionarOrganizacao(AdicionarOrganizacaoViewModel dados)
        {
            dados.Registro = FBSLIb.StringLib.Somentenumero(dados.Registro);
            _adicionarOrganizacaoValidator.ValidateAndThrow(dados);
            var conta = new Organizacao();
            conta.TipoPessoa = dados.TipoPessoa;
            conta.idconta = dados.idconta;
            conta.Mascara = dados.Mascara;
            conta.Registro = dados.Registro;
            conta.idconta = dados.idconta;
            conta.Nome = dados.Nome;
            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new EditarOrganizacaoViewModel
            {
                Nome = conta.Nome,
                Mascara = conta.Mascara,
                Id = conta.Id,
                Registro = conta.Registro,
                TipoPessoa = conta.TipoPessoa,
                idconta = conta.idconta
            };
        }

        public async Task<EditarOrganizacaoViewModel>? SalvarOrganizacao(string idconta, int id, EditarOrganizacaoViewModel dados)
        {
            var conta = _context.organizacoes.Where(o => (o.idconta == idconta && o.Id == id)).FirstOrDefault();
            if (conta != null)
            {
                dados.Registro = FBSLIb.StringLib.Somentenumero(dados.Registro);
                _editarOrganizacaoValidator.ValidateAndThrow(dados);
                conta.TipoPessoa = dados.TipoPessoa;
                conta.idconta = dados.idconta;
                conta.Mascara = dados.Mascara;
                conta.Registro = dados.Registro;
                conta.Nome = dados.Nome;

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new EditarOrganizacaoViewModel
                {
                    Nome = conta.Nome,
                    Mascara = conta.Mascara,
                    Id = conta.Id,
                    Registro = conta.Registro,
                    TipoPessoa = conta.TipoPessoa,
                    idconta = conta.idconta
                };
            }
            else return null;
        }

        public async Task<EditarOrganizacaoViewModel>? ExcluirOrganizacao(int id, string idconta)
        {
            var conta = _context.organizacoes.Where(m => (m.idconta == idconta && m.Id == id)).FirstOrDefault();
            if (conta != null)
            {
                _context.organizacoes.Remove(conta);
                await _context.SaveChangesAsync();
                return new EditarOrganizacaoViewModel
                {
                    Nome = conta.Nome,
                    Mascara = conta.Mascara,
                    Id = conta.Id,
                    Registro = conta.Registro,
                    TipoPessoa = conta.TipoPessoa,
                    idconta = conta.idconta
                };
            }
            else return null;
        }

        public async Task<EditarOrganizacaoViewModel>? ListarOrganizacaoById(string idconta, int id)
        {
            var conta = _context.organizacoes.Where(o => (o.idconta == idconta && o.Id == id))
                .Include(x => x.organizacaoUsuarios)
                .FirstOrDefault();
            if (conta != null)
            {
                return new EditarOrganizacaoViewModel
                {
                    Nome = conta.Nome,
                    Mascara = conta.Mascara,
                    Id = conta.Id,
                    Registro = conta.Registro,
                    TipoPessoa = conta.TipoPessoa,
                    idconta = conta.idconta,
                    Usuarios = conta.organizacaoUsuarios.Select(o => new OrganizacaoUsuarioViewModel
                    {
                        id = o.id,
                        idorganizacao = o.idorganizacao,
                        uid = o.uid,
                        nomeusuario = " "
                    }).ToList()
                };
            }
            else return null;
        }

        public async Task<IEnumerable<EditarOrganizacaoViewModel>> ListarOrganizacao(string? filtro)
        {
            var condicao = (Organizacao m) => ((String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper())) || (String.IsNullOrWhiteSpace(filtro) || m.Registro.ToUpper().Contains(filtro.ToUpper())));
            var query = _context.organizacoes.AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new EditarOrganizacaoViewModel
                {
                    Nome = c.Nome,
                    Mascara = c.Mascara,
                    Id = c.Id,
                    Registro = c.Registro,
                    TipoPessoa = c.TipoPessoa,
                    idconta = c.idconta
                }
                ).ToList();
            return (contas);
        }

        public async Task<IEnumerable<EditarOrganizacaoViewModel>> ListarOrganizacaoByConta(string idconta, string? filtro)
        {
            var condicao = (Organizacao m) => (m.idconta == idconta) && ((String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper())) || (String.IsNullOrWhiteSpace(filtro) || m.Registro.ToUpper().Contains(filtro.ToUpper())));
            var query = _context.organizacoes
                //.Include(x => x.organizacaoUsuarios)
                .AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new EditarOrganizacaoViewModel
                {
                    Nome = c.Nome,
                    Mascara = c.Mascara,
                    Id = c.Id,
                    Registro = c.Registro,
                    TipoPessoa = c.TipoPessoa,
                    idconta = c.idconta,
                    desctipo = c.TipoPessoa.ToString()
                }
                ).ToList();
            return (contas);
        }

        // OrganizaaoUsuario

        public async Task<IEnumerable<OrganizacaoUsuarioViewModel>> ListarOrganizacaoByUid(string uid)
        {
            var condicao = (OrganizacaoUsuario m) => (m.uid == uid);
            var query = _context.organizacaoUsuarios
                .Include(x => x.organizacao)
                .AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new OrganizacaoUsuarioViewModel
                {
                    id = c.id,
                    idorganizacao = c.idorganizacao,
                    uid = c.uid,
                    descorg = c.organizacao.Nome
                }
                ).ToList();
            return (contas);
        }

        public async Task<IEnumerable<OrganizacaoUsuarioViewModel>> ListarUsuariosByOrg(int idorg)
        {
            var condicao = (OrganizacaoUsuario m) => (m.idorganizacao == idorg);
            var query = _context.organizacaoUsuarios
                //.Include(x => x.organizacaoUsuarios)
                .AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new OrganizacaoUsuarioViewModel
                {
                    id = c.id,
                    idorganizacao = c.idorganizacao,
                    uid = c.uid
                }
                ).ToList();
            return (contas);
        }

        public async Task<EditarOrganizacaoUsuarioViewModel> AdicionarOrganizacaoUsuario(EditarOrganizacaoUsuarioViewModel dados)
        {
            // _adicionarOrganizacaoValidator.ValidateAndThrow(dados);
            var conta = new OrganizacaoUsuario();
            conta.idorganizacao = dados.idorganizacao;
            conta.uid = dados.uid;

            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new EditarOrganizacaoUsuarioViewModel
            {
                uid = conta.uid,
                idorganizacao = conta.idorganizacao,
                id = conta.id
            };
        }

        public async Task<EditarOrganizacaoUsuarioViewModel>? SalvarOrganizacaoUsuario(int id, EditarOrganizacaoUsuarioViewModel dados)
        {
            //   _editarOrganizacaoValidator.ValidateAndThrow(dados);
            var conta = _context.organizacaoUsuarios.Where(o => (o.id == id)).FirstOrDefault();
            if (conta != null)
            {
                conta.idorganizacao = dados.idorganizacao;
                conta.uid = dados.uid;

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new EditarOrganizacaoUsuarioViewModel
                {
                    uid = conta.uid,
                    idorganizacao = conta.idorganizacao,
                    id = conta.id
                };
            }
            else return null;
        }

        public async Task<EditarOrganizacaoUsuarioViewModel>? ExcluirOrganizacaoUsuario(int id)
        {
            var conta = _context.organizacaoUsuarios.Where(m => (m.id == id)).FirstOrDefault();
            if (conta != null)
            {
                _context.organizacaoUsuarios.Remove(conta);
                await _context.SaveChangesAsync();
                return new EditarOrganizacaoUsuarioViewModel
                {
                    uid = conta.uid,
                    idorganizacao = conta.idorganizacao,
                    id = conta.id
                };
            }
            else return null;
        }
    }
}