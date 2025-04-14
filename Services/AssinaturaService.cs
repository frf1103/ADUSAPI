using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Validators.Assinatura;
using ADUSAPICore.Models.Assinatura;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
            conta.qtd = dados.qtd;
            conta.datains = DateTime.Now;
            await _context.AddAsync(conta);
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
                status = conta.status
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
                conta.idplataforma = dados.idplataforma;
                conta.qtd = dados.qtd;
                conta.status = dados.status;
                conta.dataup = DateTime.Now;

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
                    status = conta.status
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
                    status = conta.status
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
                    status = conta.status
                };
            }
            else return null;
        }

        public async Task<AssinaturaViewModel>? ListarAssinaturaById(string id)
        {
            var conta = _context.assinaturas
            .Where(p => p.id == id).FirstOrDefault();
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
                    status = conta.status
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListAssinaturaViewModel>> ListarAssinatura(DateTime ini, DateTime fim, string idparceiro, int status, int forma, string? filtro)
        {
            var condicao = (Assinatura m) => ((String.IsNullOrWhiteSpace(filtro) || m.observacao.ToUpper().Contains(filtro.ToUpper())) || (String.IsNullOrWhiteSpace(filtro) || (m.idplataforma ?? " ").ToUpper().Contains(filtro.ToUpper()))
            ) && (m.datavenda >= ini && m.datavenda <= fim && (idparceiro == "0" || m.idparceiro == idparceiro) && (status == 3 || (int)m.status == status) && (forma == 3 || (int)m.idformapagto == forma));
            var query = _context.assinaturas.AsQueryable();
            var contas = query.Include(m => m.parceiro).Where(condicao)
                .Select(c => new ListAssinaturaViewModel
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
                    descstatus = c.status.ToString()
                }
                ).ToList();
            return (contas);
        }
    }
}