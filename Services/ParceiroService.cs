using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Validators.Parceiro;
using ADUSAPICore.Models.Parceiro;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ADUSAPI.Services
{
    public class ParceiroService
    {
        private readonly ADUSContext _context;
        private readonly ParceiroValidator _adicionarParceiroValidator;
        private readonly ExcluirParceiroValidator _excluirParceiroValidator;

        public ParceiroService(ADUSContext context, ParceiroValidator adicionarParceiroValidator, ExcluirParceiroValidator excluirParceiroValidator)
        {
            _context = context;
            _adicionarParceiroValidator = adicionarParceiroValidator;
            _excluirParceiroValidator = excluirParceiroValidator;
        }

        public async Task<ParceiroViewModel> AdicionarParceiro(ParceiroViewModel dados)
        {
            _adicionarParceiroValidator.ValidateAndThrow(dados);
            var conta = new Parceiro();

            conta.uid = dados.Id;
            conta.idUF = dados.iduf;
            conta.idCidade = dados.idCidade;
            conta.EstadoCivil = dados.EstadoCivil;
            conta.Bairro = dados.Bairro;

            conta.CEP = dados.cep;
            conta.Fantasia = dados.Fantasia;
            conta.RazaoSocial = dados.RazaoSocial;
            conta.TipodePessoa = dados.TipodePessoa;
            conta.Registro = FBSLIb.StringLib.Somentenumero(dados.Registro.ToString());

            conta.Logradouro = dados.Logradouro;
            conta.Numero = dados.Numero;
            conta.Profissao = dados.Profissao;
            conta.IdRepresentante = dados.IdRepresentante;
            conta.DtNascimento = dados.DtNascimento;
            conta.Complemento = dados.Complemento;
            conta.Fone1 = dados.Fone1;
            conta.Fone2 = dados.Fone2;
            conta.observacao = dados.observacao;
            conta.email = dados.email;
            conta.Sexo = dados.Sexo;
            conta.datains = DateTime.Now;
            conta.isafiliado = dados.isafiliado;
            conta.isassinante = dados.isassinante;
            conta.isbanco = dados.isbanco;
            conta.iscoprodutor = dados.iscoprodutor;
            conta.percomissao = dados.percomissao;
            conta.idcoprodutor = dados.idcoprodutor;
            conta.urlafiliado=dados.urlafiliado;
            await _context.AddAsync(conta);
            await _context.SaveChangesAsync();
            return new ParceiroViewModel
            {
                Id = conta.uid,
                iduf = conta.idUF,
                idCidade = conta.idCidade,
                EstadoCivil = conta.EstadoCivil,
                Bairro = conta.Bairro,

                cep = conta.CEP,
                Fantasia = conta.Fantasia,
                RazaoSocial = conta.RazaoSocial,
                TipodePessoa = conta.TipodePessoa,
                Registro = conta.Registro,

                Logradouro = conta.Logradouro,
                Numero = conta.Numero,
                Profissao = conta.Profissao,
                IdRepresentante = conta.IdRepresentante,
                DtNascimento = conta.DtNascimento,
                Complemento = conta.Complemento,
                observacao = conta.observacao,
                Fone1 = conta.Fone1,
                Fone2 = conta.Fone2,
                email = conta.email,
                Sexo = conta.Sexo,
                isafiliado = conta.isafiliado,
                isassinante = conta.isassinante,
                isbanco = conta.isbanco,
                iscoprodutor = conta.iscoprodutor,
                percomissao = conta.percomissao,
                idcoprodutor = conta.idcoprodutor,
                urlafiliado=conta.urlafiliado
            };
        }

        public async Task<ParceiroViewModel>? SalvarParceiro(string id, ParceiroViewModel dados)
        {
            _adicionarParceiroValidator.ValidateAndThrow(dados);
            var conta = _context.parceiros.Where(p => p.uid == id).FirstOrDefault();
            if (conta != null)
            {
                conta.idUF = dados.iduf;
                conta.idCidade = dados.idCidade;
                conta.EstadoCivil = dados.EstadoCivil;
                conta.Bairro = dados.Bairro;

                conta.CEP = dados.cep;
                conta.Fantasia = dados.Fantasia;
                conta.RazaoSocial = dados.RazaoSocial;
                conta.TipodePessoa = dados.TipodePessoa;
                conta.Registro = FBSLIb.StringLib.Somentenumero(dados.Registro.ToString());

                conta.Logradouro = dados.Logradouro;
                conta.Numero = dados.Numero;
                conta.Profissao = dados.Profissao;
                conta.IdRepresentante = dados.IdRepresentante;
                conta.DtNascimento = dados.DtNascimento;
                conta.Complemento = dados.Complemento;
                conta.dataup = DateTime.Now;
                conta.Fone1 = dados.Fone1;
                conta.Fone2 = dados.Fone2;
                conta.observacao = dados.observacao;
                conta.email = dados.email;
                conta.Sexo = dados.Sexo;
                conta.isafiliado = dados.isafiliado;
                conta.isassinante = dados.isassinante;
                conta.isbanco = dados.isbanco;
                conta.iscoprodutor = dados.iscoprodutor;
                conta.percomissao = dados.percomissao;
                conta.idcoprodutor = dados.idcoprodutor;
                conta.urlafiliado = dados.urlafiliado;
                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new ParceiroViewModel
                {
                    Id = conta.uid,
                    iduf = conta.idUF,
                    idCidade = conta.idCidade,
                    EstadoCivil = conta.EstadoCivil,
                    Bairro = conta.Bairro,

                    cep = conta.CEP,
                    Fantasia = conta.Fantasia,
                    RazaoSocial = conta.RazaoSocial,
                    TipodePessoa = conta.TipodePessoa,
                    Registro = conta.Registro,

                    Logradouro = conta.Logradouro,
                    Numero = conta.Numero,
                    Profissao = conta.Profissao,
                    IdRepresentante = conta.IdRepresentante,
                    DtNascimento = conta.DtNascimento,
                    Complemento = conta.Complemento,
                    observacao = conta.observacao,
                    Fone1 = conta.Fone1,
                    Fone2 = conta.Fone2,
                    email = conta.email,
                    Sexo = conta.Sexo,
                    isafiliado = conta.isafiliado,
                    isassinante = conta.isassinante,
                    isbanco = conta.isbanco,
                    iscoprodutor = conta.iscoprodutor,
                    percomissao = conta.percomissao,
                    idcoprodutor = conta.idcoprodutor,
                    urlafiliado=conta.urlafiliado
                };
            }
            else return null;
        }

        public async Task<ParceiroViewModel>? ExcluirParceiro(string id)
        {
            var conta = _context.parceiros.Where(p => p.uid == id).FirstOrDefault();
            if (conta != null)
            {
                ParceiroViewModel dados = new ParceiroViewModel
                {
                    Id = conta.uid,
                    iduf = conta.idUF,
                    idCidade = conta.idCidade,
                    EstadoCivil = conta.EstadoCivil,
                    Bairro = conta.Bairro,

                    cep = conta.CEP,
                    Fantasia = conta.Fantasia,
                    RazaoSocial = conta.RazaoSocial,
                    TipodePessoa = conta.TipodePessoa,
                    Registro = conta.Registro,

                    Logradouro = conta.Logradouro,
                    Numero = conta.Numero,
                    Profissao = conta.Profissao,
                    IdRepresentante = conta.IdRepresentante,
                    DtNascimento = conta.DtNascimento,
                    Complemento = conta.Complemento,
                    observacao = conta.observacao,
                    Fone1 = conta.Fone1,
                    Fone2 = conta.Fone2,
                    email = conta.email,
                    isafiliado = conta.isafiliado,
                    isassinante = conta.isassinante,
                    isbanco = conta.isbanco,
                    iscoprodutor = conta.iscoprodutor,
                    percomissao = conta.percomissao,
                    idcoprodutor = conta.idcoprodutor,
                    urlafiliado=conta.urlafiliado
                };
                _excluirParceiroValidator.ValidateAndThrow(dados);
                _context.parceiros.Remove(conta);
                await _context.SaveChangesAsync();
                return new ParceiroViewModel
                {
                    Id = conta.uid,
                    iduf = conta.idUF,
                    idCidade = conta.idCidade,
                    EstadoCivil = conta.EstadoCivil,
                    Bairro = conta.Bairro,

                    cep = conta.CEP,
                    Fantasia = conta.Fantasia,
                    RazaoSocial = conta.RazaoSocial,
                    TipodePessoa = conta.TipodePessoa,
                    Registro = conta.Registro,

                    Logradouro = conta.Logradouro,
                    Numero = conta.Numero,
                    Profissao = conta.Profissao,
                    IdRepresentante = conta.IdRepresentante,
                    DtNascimento = conta.DtNascimento,
                    Complemento = conta.Complemento,
                    observacao = conta.observacao,
                    Fone1 = conta.Fone1,
                    Fone2 = conta.Fone2,
                    email = conta.email,
                    Sexo = conta.Sexo,
                    isafiliado = conta.isafiliado,
                    isassinante = conta.isassinante,
                    isbanco = conta.isbanco,
                    iscoprodutor = conta.iscoprodutor,
                    percomissao = conta.percomissao,
                    idcoprodutor = conta.idcoprodutor,
                    urlafiliado = conta.urlafiliado
                };
            }
            else return null;
        }

        public async Task<ParceiroViewModel>? ListarParceiroById(string id)
        {
            var conta = _context.parceiros.Include(x => x.cidade).Include(x => x.uf)
            .Where(p => p.uid == id).FirstOrDefault();
            if (conta != null)
            {
                return new ParceiroViewModel
                {
                    Id = conta.uid,

                    iduf = conta.idUF,
                    idCidade = conta.idCidade,
                    EstadoCivil = conta.EstadoCivil,
                    Bairro = conta.Bairro,

                    cep = conta.CEP,
                    Fantasia = conta.Fantasia,
                    RazaoSocial = conta.RazaoSocial,
                    TipodePessoa = conta.TipodePessoa,
                    Registro = conta.Registro,

                    Logradouro = conta.Logradouro,
                    Numero = conta.Numero,
                    Profissao = conta.Profissao,
                    IdRepresentante = conta.IdRepresentante,
                    DtNascimento = conta.DtNascimento,
                    Complemento = conta.Complemento,
                    observacao = conta.observacao,
                    Fone1 = conta.Fone1,
                    Fone2 = conta.Fone2,
                    email = conta.email,
                    Sexo = conta.Sexo,
                    isafiliado = conta.isafiliado,
                    isassinante = conta.isassinante,
                    isbanco = conta.isbanco,
                    iscoprodutor = conta.iscoprodutor,
                    percomissao = conta.percomissao,
                    idcoprodutor = conta.idcoprodutor,
                    nomecidade = conta.cidade.Nome,
                    nomeuf = conta.uf.Sigla,
                    urlafiliado=conta.urlafiliado
                };
            }
            else return null;
        }

        public async Task<ParceiroViewModel>? ListarParceiroByEmail(string id)
        {
            var conta = _context.parceiros.Include(x => x.cidade).Include(x => x.uf)
            .Where(p => p.email == id).FirstOrDefault();
            if (conta != null)
            {
                return new ParceiroViewModel
                {
                    Id = conta.uid,

                    iduf = conta.idUF,
                    idCidade = conta.idCidade,
                    EstadoCivil = conta.EstadoCivil,
                    Bairro = conta.Bairro,

                    cep = conta.CEP,
                    Fantasia = conta.Fantasia,
                    RazaoSocial = conta.RazaoSocial,
                    TipodePessoa = conta.TipodePessoa,
                    Registro = conta.Registro,

                    Logradouro = conta.Logradouro,
                    Numero = conta.Numero,
                    Profissao = conta.Profissao,
                    IdRepresentante = conta.IdRepresentante,
                    DtNascimento = conta.DtNascimento,
                    Complemento = conta.Complemento,
                    observacao = conta.observacao,
                    Fone1 = conta.Fone1,
                    Fone2 = conta.Fone2,
                    email = conta.email,
                    Sexo = conta.Sexo,
                    isafiliado = conta.isafiliado,
                    isassinante = conta.isassinante,
                    isbanco = conta.isbanco,
                    iscoprodutor = conta.iscoprodutor,
                    percomissao = conta.percomissao,
                    idcoprodutor = conta.idcoprodutor,
                    nomecidade = conta.cidade.Nome,
                    nomeuf = conta.uf.Sigla,
                    urlafiliado=conta.urlafiliado
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListParceiroViewModel>> ListarParceiro(string? filtro, bool isassinante = true, bool isbanco = true, bool iscoprodutor = true, bool isafiliado = true, string? idcoprodutor = "")
        {
            var condicao = (Parceiro m) => ((String.IsNullOrWhiteSpace(filtro) || m.RazaoSocial.ToUpper().Contains(filtro.ToUpper())) || (String.IsNullOrWhiteSpace(filtro) || m.Fantasia.ToUpper().Contains(filtro.ToUpper())) ||
            (String.IsNullOrWhiteSpace(filtro) || m.Registro.ToUpper().Contains(filtro.ToUpper()))
             && (String.IsNullOrWhiteSpace(idcoprodutor) || m.idcoprodutor == idcoprodutor)
            );
            /*
            (
                (isafiliado == true && m.isafiliado == isafiliado) &&
                (isassinante == true && isassinante == m.isassinante) &&
                (isbanco == true && isbanco == m.isbanco) &&
                (iscoprodutor == true && iscoprodutor == m.iscoprodutor)
                )
             */

            Expression<Func<Parceiro, bool>> cond = m =>
                (string.IsNullOrWhiteSpace(filtro) ||
                    EF.Functions.Like(m.RazaoSocial, $"%{filtro}%") ||
                    EF.Functions.Like(m.Fantasia, $"%{filtro}%") ||
                    EF.Functions.Like(m.Registro, $"%{filtro}%"))
                &&

                (string.IsNullOrWhiteSpace(idcoprodutor) || m.idcoprodutor == idcoprodutor)
                &&
                (
                (isafiliado && m.isafiliado == true)

                ||
                (isassinante && m.isassinante == true)

                ||
                (isbanco && m.isbanco == true)
                ||
                (iscoprodutor && m.iscoprodutor == true)

            );
            var query = _context.parceiros.Include(x => x.cidade).Include(x => x.uf).Where(cond);
            var contas = query
                .Select(c => new ListParceiroViewModel
                {
                    Id = c.uid.ToString(),
                    iduf = c.idUF,
                    idCidade = c.idCidade,
                    EstadoCivil = c.EstadoCivil,
                    Bairro = c.Bairro,
                    cep = c.CEP,
                    Fantasia = c.Fantasia,
                    RazaoSocial = c.RazaoSocial,
                    TipodePessoa = c.TipodePessoa,
                    Registro = c.Registro,
                    Logradouro = c.Logradouro,
                    Numero = (string)c.Numero,
                    Profissao = c.Profissao,
                    IdRepresentante = c.IdRepresentante,
                    DtNascimento = c.DtNascimento,
                    nomecidade = c.cidade.Nome,
                    nomeuf = c.uf.Nome,
                    Complemento = c.Complemento,
                    observacao = c.observacao,
                    Fone1 = c.Fone1,
                    Fone2 = c.Fone2,
                    email = c.email,
                    Sexo = c.Sexo,
                    descestadocivil = c.EstadoCivil.ToString(),
                    desctipo = c.TipodePessoa.ToString(),
                    isafiliado = c.isafiliado,
                    isassinante = c.isassinante,
                    isbanco = c.isbanco,
                    iscoprodutor = c.iscoprodutor,
                    percomissao = c.percomissao,
                    idcoprodutor = c.idcoprodutor,
                    urlafiliado=c.urlafiliado
                }
                ).ToList().OrderBy(c => c.RazaoSocial);
            return (contas);
        }
    }
}