using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Conta;
using FarmPlannerAPICore.Models.Conta;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FarmPlannerAPI.Services
{
    public class ContaService
    {
        private readonly FarmPlannerContext _context;
        private readonly AdicionarContaValidator _adicionarContaValidator;

        public ContaService(FarmPlannerContext context, AdicionarContaValidator adicionarContaValidator)
        {
            _context = context;
            _adicionarContaValidator = adicionarContaValidator;
        }

        public async Task<EditarContaViewModel> AdicionarConta(AdicionarUsuarioConta dados)
        {
            _adicionarContaValidator.ValidateAndThrow(dados);
            var conta = new Conta();
            conta.Id = Guid.NewGuid().ToString();
            conta.Email = dados.Email;
            conta.Nome = dados.Nome;
            conta.CPF = dados.CPF;
            conta.uid = dados.uid;
            conta.telefone = dados.telefone;
            conta.ativa = dados.ativa;
            conta.representanteid = dados.representanteid;

            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new EditarContaViewModel
            {
                Nome = conta.Nome,
                Email = conta.Email,
                Id = conta.Id,
                CPF = conta.CPF,
                telefone = conta.telefone,
                contaguid = conta.Id,
                ativa = conta.ativa
            };
        }

        public async Task<UsuarioContaViewModel> AdicionarUsuarioConta(UsuarioContaViewModel dados)
        {
            var conta = new UsuarioConta();
            conta.idconta = dados.idconta;
            conta.contaguid = dados.contaguid;
            conta.uid = dados.uid;
            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new UsuarioContaViewModel
            {
                idconta = dados.idconta,
                contaguid = dados.contaguid,
                uid = dados.uid
            };
        }

        public async Task<UsuarioContaViewModel>? GetContaByUid(string uid)
        {
            var conta = _context.usuarioscontas.Where(u => u.uid == uid).FirstOrDefault();
            if (conta != null)
            {
                return new UsuarioContaViewModel
                {
                    contaguid = conta.contaguid,
                    uid = conta.uid,
                    idconta = conta.idconta
                };
            }
            else return null;
        }

        public async Task<EditarContaViewModel>? SalvarConta(string id, EditarContaViewModel dados)
        {
            //      _adicionarContaValidator.ValidateAndThrow(dados);
            var conta = _context.contas.Find(id);
            if (conta != null)
            {
                conta.Email = dados.Email;
                conta.Nome = dados.Nome;
                conta.CPF = dados.CPF;
                conta.uid = dados.uid;
                conta.ativa = dados.ativa;
                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new EditarContaViewModel
                {
                    Nome = conta.Nome,
                    Email = conta.Email,
                    Id = conta.Id,
                    CPF = conta.CPF,
                    telefone = conta.telefone,
                    contaguid = conta.Id,
                    ativa = conta.ativa
                };
            }
            else return null;
        }

        public async Task<EditarContaViewModel>? ExcluirConta(string id, EditarContaViewModel dados)
        {
            var conta = _context.contas.Find(id);
            if (conta != null)
            {
                _context.contas.Remove(conta);
                await _context.SaveChangesAsync();
                return new EditarContaViewModel
                {
                    Nome = dados.Nome,
                    Email = dados.Email,
                    CPF = dados.CPF,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<EditarContaViewModel>? ListarContaById(string id)
        {
            var conta = _context.contas.Find(id);
            if (conta != null)
            {
                return new EditarContaViewModel
                {
                    Nome = conta.Nome,
                    Email = conta.Email,
                    CPF = conta.CPF,
                    Id = conta.Id,
                    uid = conta.uid,
                    contaguid = conta.Id,
                    ativa = conta.ativa,
                    telefone = conta.telefone
                };
            }
            else return null;
        }

        public async Task<EditarContaViewModel>? ListarContaByUId(string uid)
        {
            var conta = _context.contas.Where(c => c.uid == uid).FirstOrDefault();
            if (conta != null)
            {
                return new EditarContaViewModel
                {
                    Nome = conta.Nome,
                    Email = conta.Email,
                    CPF = conta.CPF,
                    Id = conta.Id,
                    contaguid = conta.Id,
                    uid = conta.uid,
                    ativa = conta.ativa
                };
            }
            else return null;
        }

        /*
                public async Task<EditarContaViewModel>? ListarContaByGUId(string uid)
                {
                    var conta = _context.contas.Where(c => c.ContaGuid == uid).FirstOrDefault();
                    if (conta != null)
                    {
                        return new EditarContaViewModel
                        {
                            Nome = conta.Nome,
                            Email = conta.Email,
                            CPF = conta.CPF,
                            Id = conta.Id,
                            contaguid = Convert.ToString(conta.ContaGuid),
                            uid = conta.uid
                        };
                    }
                    else return null;
                }
        */

        public async Task<IEnumerable<EditarContaViewModel>> ListarConta(string? filtro)
        {
            var condicao = (Conta m) => (String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.contas.AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new EditarContaViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    CPF = c.CPF,
                    contaguid = c.Id,
                    uid = c.uid,
                    ativa = c.ativa
                }
                ).ToList();
            return (contas);
        }

        public async Task<IEnumerable<EditarContaViewModel>> ListarContaByRep(string? filtro, string repid)
        {
            var condicao = (Conta m) => (m.representanteid == repid) && (String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.contas.AsQueryable();
            var contas = query.Where(condicao)
                .Select(c => new EditarContaViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    CPF = c.CPF,
                    contaguid = c.Id,
                    uid = c.uid,
                    ativa = c.ativa
                }
                ).ToList();
            return (contas);
        }

        public async Task<IEnumerable<UsuarioContaViewModel>> ListUsuariosByConta(string contaguid)
        {
            var query = _context.usuarioscontas.AsQueryable();
            var contas = query.Where(c => c.contaguid == contaguid)
                .Select(c => new UsuarioContaViewModel
                {
                    contaguid = c.contaguid,
                    uid = c.uid,
                    idconta = c.idconta
                }
                ).ToList();
            return (contas);
        }

        public async Task<IEnumerable<FinanceiroContaViewModel>> ListFinanceiroByConta(string contaguid)
        {
            var query = _context.financeiroContas.AsQueryable();
            var contas = query.Where(c => c.idconta == contaguid)
                .Select(c => new FinanceiroContaViewModel
                {
                    idconta = c.idconta,
                    datapagto = c.datapagto,
                    desconto = c.desconto,
                    emissao = c.emissao,
                    id = c.id,
                    tipo = c.tipo,
                    obs = c.obs,
                    valor = c.valor,
                    valorpago = c.valorpago,
                    vencimento = c.vencimento
                }
                ).ToList();
            return (contas);
        }

        public async Task<FinanceiroContaViewModel> ListFinanceiroById(int id)
        {
            var query = _context.financeiroContas.AsQueryable();
            var contas = query.Where(c => c.id == id)
                .Select(c => new FinanceiroContaViewModel
                {
                    idconta = c.idconta,
                    datapagto = c.datapagto,
                    desconto = c.desconto,
                    emissao = c.emissao,
                    id = c.id,
                    tipo = c.tipo,
                    obs = c.obs,
                    valor = c.valor,
                    valorpago = c.valorpago,
                    vencimento = c.vencimento
                }
                ).FirstOrDefault();
            return (contas);
        }

        public async Task<FinanceiroContaViewModel> AdicionarFinanceiroConta(FinanceiroContaViewModel dados)
        {
            var conta = new FinanceiroConta();
            conta.tipo = dados.tipo;
            conta.emissao = dados.emissao;
            conta.valor = dados.valor;
            conta.valorpago = dados.valorpago;
            conta.datapagto = dados.datapagto;
            conta.idconta = dados.idconta;
            conta.obs = dados.obs;
            conta.desconto = dados.desconto;
            conta.vencimento = dados.vencimento;

            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new FinanceiroContaViewModel
            {
                idconta = conta.idconta,
                datapagto = conta.datapagto,
                desconto = conta.desconto,
                emissao = conta.emissao,
                id = conta.id,
                tipo = conta.tipo,
                obs = conta.obs,
                valor = conta.valor,
                valorpago = conta.valorpago,
                vencimento = conta.vencimento
            };
        }

        public async Task<FinanceiroContaViewModel> SalvarFinanceiroConta(int id, FinanceiroContaViewModel dados)
        {
            var conta = _context.financeiroContas.Find(id);
            if (conta != null)
            {
                conta.tipo = dados.tipo;
                conta.emissao = dados.emissao;
                conta.valor = dados.valor;
                conta.valorpago = dados.valorpago;
                conta.datapagto = dados.datapagto;
                conta.idconta = dados.idconta;
                conta.obs = dados.obs;
                conta.desconto = dados.desconto;
                conta.vencimento = dados.vencimento;

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new FinanceiroContaViewModel
                {
                    idconta = conta.idconta,
                    datapagto = conta.datapagto,
                    desconto = conta.desconto,
                    emissao = conta.emissao,
                    id = conta.id,
                    tipo = conta.tipo,
                    obs = conta.obs,
                    valor = conta.valor,
                    valorpago = conta.valorpago,
                    vencimento = conta.vencimento
                };
            }
            else return null;
        }

        public async Task<IEnumerable<AssinaturaContaViewModel>> ListAssinaturaByConta(string contaguid)
        {
            var query = _context.assinaturaContas.AsQueryable();
            var contas = query.Where(c => c.idconta == contaguid)
                .Select(c => new AssinaturaContaViewModel
                {
                    idconta = c.idconta,
                    id = c.id,
                    dataassinatura = c.dataassinatura,
                    dataexpiracao = c.dataexpiracao,
                    plano = c.plano
                }
                ).ToList();
            return (contas);
        }

        public async Task<AssinaturaContaViewModel> ListAssinaturaById(int id)
        {
            var query = _context.assinaturaContas.AsQueryable();
            var contas = query.Where(c => c.id == id)
                .Select(c => new AssinaturaContaViewModel
                {
                    idconta = c.idconta,
                    id = c.id,
                    dataassinatura = c.dataassinatura,
                    dataexpiracao = c.dataexpiracao,
                    plano = c.plano
                }
                ).FirstOrDefault();
            return (contas);
        }

        public async Task<AssinaturaContaViewModel> AdicionarAssinaturaConta(AssinaturaContaViewModel dados)
        {
            var conta = new AssinaturaConta();
            conta.dataassinatura = dados.dataassinatura;
            conta.plano = dados.plano;
            conta.dataexpiracao = dados.dataexpiracao;
            conta.idconta = dados.idconta;
            conta.plano = dados.plano;
            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new AssinaturaContaViewModel
            {
                idconta = conta.idconta,
                id = conta.id,
                dataassinatura = conta.dataassinatura,
                dataexpiracao = conta.dataexpiracao,
                plano = conta.plano
            };
        }

        public async Task<AssinaturaContaViewModel> SalvarAssinaturaConta(int id, AssinaturaContaViewModel dados)
        {
            var conta = _context.assinaturaContas.Find(id);
            if (conta != null)
            {
                conta.dataassinatura = dados.dataassinatura;
                conta.plano = dados.plano;
                conta.dataexpiracao = dados.dataexpiracao;
                conta.idconta = dados.idconta;
                conta.plano = dados.plano;

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new AssinaturaContaViewModel
                {
                    idconta = conta.idconta,
                    id = conta.id,
                    dataassinatura = conta.dataassinatura,
                    dataexpiracao = conta.dataexpiracao,
                    plano = conta.plano
                };
            }
            else return null;
        }
    }
}