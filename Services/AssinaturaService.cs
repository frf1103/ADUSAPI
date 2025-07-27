using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Migrations;
using ADUSAPI.Validators.Assinatura;
using ADUSAPICore.Models.Assinatura;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ADUSAPI.Services
{
    public class AssinaturaService
    {
        private readonly ADUSContext _context;
        private readonly AssinaturaValidator _adicionarAssinaturaValidator;
        // private readonly ExcluirAssinaturaValidator _excluirAssinaturaValidator;

        public AssinaturaService(ADUSContext context, AssinaturaValidator adicionarAssinaturaValidator)
        //, ExcluirAssinaturaValidator excluirAssinaturaValidator)
        {
            _context = context;
            _adicionarAssinaturaValidator = adicionarAssinaturaValidator;
            //_excluirAssinaturaValidator = excluirAssinaturaValidator;
        }

        public async Task<AssinaturaViewModel> AdicionarAssinatura(AssinaturaViewModel dados)
        {
            _adicionarAssinaturaValidator.ValidateAndThrow(dados);
            var conta = new Assinatura();

            conta.datavenda = dados.datavenda;
            conta.id = dados.id;
            conta.valor = dados.valor;
            conta.preco = dados.preco;
            conta.valor = dados.valor;
            conta.observacao = dados.observacao;
            conta.idformapagto = dados.idformapagto;
            conta.idparceiro = dados.idparceiro;
            conta.idplataforma = dados.idplataforma;
            conta.status = dados.status;
            conta.plataforma = dados.plataforma;
            conta.qtd = dados.qtd;
            conta.idafiliado = dados.idafiliado;

            conta.datains = DateTime.Now;
            try
            {
                await _context.AddAsync(conta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                var j = 1;
            }
            return new AssinaturaViewModel
            {
                id = conta.id,
                datavenda = conta.datavenda,
                qtd = conta.qtd,
                preco = conta.preco,
                valor = conta.valor,
                observacao = conta.observacao,
                idplataforma = conta.idplataforma,
                idparceiro = conta.idparceiro,
                idformapagto = conta.idformapagto,
                status = conta.status,
                plataforma = conta.plataforma,
                idafiliado = conta.idafiliado
            };
        }

        public async Task<AssinaturaViewModel>? SalvarAssinatura(string id, AssinaturaViewModel dados)
        {
            _adicionarAssinaturaValidator.ValidateAndThrow(dados);
            var conta = _context.assinaturas.Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                conta.datavenda = dados.datavenda;
                conta.id = dados.id;
                conta.valor = dados.valor;
                conta.preco = dados.preco;
                conta.valor = dados.valor;
                conta.observacao = dados.observacao;
                conta.idformapagto = dados.idformapagto;
                conta.idparceiro = dados.idparceiro;
                conta.plataforma = dados.plataforma;
                conta.idplataforma = dados.idplataforma;
                conta.qtd = dados.qtd;
                conta.status = dados.status;
                conta.dataup = DateTime.Now;
                conta.idafiliado = dados.idafiliado;

                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new AssinaturaViewModel
                {
                    id = conta.id,
                    datavenda = conta.datavenda,
                    qtd = conta.qtd,
                    preco = conta.preco,
                    valor = conta.valor,
                    observacao = conta.observacao,
                    idplataforma = conta.idplataforma,
                    idparceiro = conta.idparceiro,
                    idformapagto = conta.idformapagto,
                    status = conta.status,
                    plataforma = conta.plataforma,
                    idafiliado = conta.idafiliado
                };
            }
            else return null;
        }

        public async Task<AssinaturaViewModel>? CancelarAssinatura(string id, string motivo)
        {
            var conta = _context.assinaturas.Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                conta.status = ADUSAPICore.Models.Enum.StatusAssinatura.Cancelada;
                conta.observacao = conta.observacao.Trim() + " CANCELAMENTO: " + motivo;
                _context.Update(conta);
                var parcelasremove = await _context.parcelas
                .Where(p => p.idassinatura == id && p.databaixa == null)
                .ToListAsync();
                if (parcelasremove != null)
                    _context.parcelas.RemoveRange(parcelasremove);
                await _context.SaveChangesAsync();

                /*   await _context.parcelas
                   .Where(p => p.idassinatura == id && p.databaixa!=null)
                   .ExecuteDeleteAsync(); */
                return new AssinaturaViewModel
                {
                    id = conta.id,
                    datavenda = conta.datavenda,
                    qtd = conta.qtd,
                    preco = conta.preco,
                    valor = conta.valor,
                    observacao = conta.observacao,
                    idplataforma = conta.idplataforma,
                    idparceiro = conta.idparceiro,
                    idformapagto = conta.idformapagto,
                    status = conta.status,
                    plataforma = conta.plataforma,
                    idafiliado = conta.idafiliado
                };
            }
            else return null;
        }

        public async Task<AssinaturaViewModel>? ExcluirAssinatura(string id)
        {
            var conta = _context.assinaturas.Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                AssinaturaViewModel dados = new AssinaturaViewModel
                {
                    id = conta.id,
                    datavenda = conta.datavenda,
                    qtd = conta.qtd,
                    preco = conta.preco,
                    valor = conta.valor,
                    observacao = conta.observacao,
                    idplataforma = conta.idplataforma,
                    idparceiro = conta.idparceiro,
                    idformapagto = conta.idformapagto,
                    status = conta.status,
                    plataforma = conta.plataforma
                };
                //  _excluirAssinaturaValidator.ValidateAndThrow(dados);
                _context.assinaturas.Remove(conta);
                await _context.SaveChangesAsync();
                return new AssinaturaViewModel
                {
                    id = conta.id,
                    datavenda = conta.datavenda,
                    qtd = conta.qtd,
                    preco = conta.preco,
                    valor = conta.valor,
                    observacao = conta.observacao,
                    idplataforma = conta.idplataforma,
                    idparceiro = conta.idparceiro,
                    idformapagto = conta.idformapagto,
                    status = conta.status,
                    plataforma = conta.plataforma,
                    idafiliado = conta.idafiliado
                };
            }
            else return null;
        }

        public async Task<AssinaturaViewModel>? ListarAssinaturaById(string id)
        {
            var conta = await _context.assinaturas
            .Where(p => p.id == id).FirstOrDefaultAsync();
            if (conta != null)
            {
                return new AssinaturaViewModel
                {
                    id = conta.id,
                    datavenda = conta.datavenda,
                    qtd = conta.qtd,
                    preco = conta.preco,
                    valor = conta.valor,
                    observacao = conta.observacao,
                    idplataforma = conta.idplataforma,
                    idparceiro = conta.idparceiro,
                    idformapagto = conta.idformapagto,
                    status = conta.status,
                    plataforma = conta.plataforma,
                    idafiliado = conta.idafiliado
                };
            }
            else return null;
        }

        public async Task<AssinaturaContratoViewModel>? ListarAssinaturaContratoById(string id)
        {
            var conta = await _context.assinaturas.Include(a => a.parceiro).Include(a => a.parceiro.cidade)
                .Include(a => a.parceiro.uf).Include(a => a.parceiro.Representante)
            .Where(p => p.id == id).FirstOrDefaultAsync();
            if (conta != null)
            {
                return new AssinaturaContratoViewModel
                {
                    comprador = conta.parceiro.RazaoSocial,
                    enderecocomprador = conta.parceiro.Logradouro + " " + conta.parceiro.Numero + " "
                    + conta.parceiro.Complemento ?? " " + " " + conta.parceiro.Bairro,
                    cepcomprador = conta.parceiro.CEP,
                    emailcomprador = conta.parceiro.email,
                    fonecomprador = conta.parceiro.Fone1,
                    municipiocomprador = conta.parceiro.cidade.Nome,
                    ufcomprador = conta.parceiro.uf.Sigla,
                    registrocomprador = conta.parceiro.Registro,
                    qtd = conta.qtd,
                    valor = (decimal)conta.valor * 84,
                    formapagto = "84 PARCELAS DE R$" + conta.valor.ToString("N2", new CultureInfo("pt-BR")),
                    estadocivil = conta.parceiro.EstadoCivil.ToString(),
                    nomerepresentante = conta.parceiro.Representante == null ? " " : conta.parceiro.Representante.RazaoSocial,
                    cpfrepresentante = conta.parceiro.Representante == null ? " " : conta.parceiro.Representante.Registro,
                    datavenda = conta.datavenda.ToString("dd/MM/yyyy")
                };
            }
            else return null;
        }

        public async Task<AssinaturaViewModel>? ListarAssinaturaByIdPlataforma(string id)
        {
            var conta = await _context.assinaturas
            .Where(p => p.idplataforma == id).FirstOrDefaultAsync();
            if (conta != null)
            {
                return new AssinaturaViewModel
                {
                    id = conta.id,
                    datavenda = conta.datavenda,
                    qtd = conta.qtd,
                    preco = conta.preco,
                    valor = conta.valor,
                    observacao = conta.observacao,
                    idplataforma = conta.idplataforma,
                    idparceiro = conta.idparceiro,
                    idformapagto = conta.idformapagto,
                    status = conta.status,
                    plataforma = conta.plataforma,
                    idafiliado = conta.idafiliado
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListAssinaturaViewModel>> ListarAssinatura(DateTime ini, DateTime fim, string idparceiro, int status, int forma, string? filtro)
        {
            var query = _context.assinaturas
                .Include(m => m.parceiro)
                .Include(m => m.afiliado)
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(m =>
                    (m.observacao != null && m.observacao.ToUpper().Contains(filtro.ToUpper()))
                    || (m.idplataforma != null && m.idplataforma.ToUpper().Contains(filtro.ToUpper()))
                );
            }

            query = query.Where(m =>
                m.datavenda >= ini &&
                m.datavenda <= fim &&
                (idparceiro == "0" || m.idparceiro == idparceiro) &&
                (status == 3 || (int)m.status == status) &&
                (forma == 3 || (int)m.idformapagto == forma)
            );

            var contas = query.Select(c => new ListAssinaturaViewModel
            {
                id = c.id,
                datavenda = c.datavenda,
                qtd = c.qtd,
                preco = c.preco,
                valor = c.valor,
                observacao = c.observacao,
                idplataforma = c.idplataforma,
                idparceiro = c.idparceiro,
                idformapagto = c.idformapagto,
                status = c.status,
                nomeparceiro = c.parceiro.RazaoSocial,
                descforma = c.idformapagto.ToString(),
                descstatus = c.status.ToString(),
                plataforma = c.plataforma,
                idafiliado = c.idafiliado,
                nomeafiliado = c.afiliado.RazaoSocial
            }).ToList();

            return contas;
        }

        public async Task<IEnumerable<ListAssinaturaViewModel>> ListarAssinaturaByAfiliado(DateTime ini, DateTime fim, string idparceiro, int status, int forma, string idafiliado, int tipo, string? filtro)
        {
            var query = _context.assinaturas
                .Include(m => m.parceiro)
                .Include(m => m.afiliado)
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(m =>
                    (m.observacao != null && m.observacao.ToUpper().Contains(filtro.ToUpper()))
                    || (m.idplataforma != null && m.idplataforma.ToUpper().Contains(filtro.ToUpper()))
                );
            }

            query = query.Where(m =>
                m.datavenda >= ini &&
                m.datavenda <= fim &&
                (idparceiro == "0" || m.idparceiro == idparceiro) &&
                (status == 3 || (int)m.status == status) &&
                (forma == 3 || (int)m.idformapagto == forma) &&
                (
                    (tipo == 1 && m.idafiliado == idafiliado) ||
                    (tipo == 2 && m.afiliado.idcoprodutor == idafiliado) ||
                    tipo == 0
                )
            );

            var contas = query.Select(c => new ListAssinaturaViewModel
            {
                id = c.id,
                datavenda = c.datavenda,
                qtd = c.qtd,
                preco = c.preco,
                valor = c.valor,
                observacao = c.observacao,
                idplataforma = c.idplataforma,
                idparceiro = c.idparceiro,
                idformapagto = c.idformapagto,
                status = c.status,
                nomeparceiro = c.parceiro.RazaoSocial,
                descforma = c.idformapagto.ToString(),
                descstatus = c.status.ToString(),
                plataforma = c.plataforma,
                idafiliado = c.idafiliado,
                nomeafiliado = c.afiliado.RazaoSocial
            }).ToList();

            return contas;
        }
    }
}