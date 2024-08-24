using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Fazenda;
using FarmPlannerAPICore.Models.Fazenda;
using FluentValidation;
using FarmPlannerAPI.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FarmPlannerAPI.Services
{
    public class FazendaService
    {
        private readonly FarmPlannerContext _context;
        private readonly FazendaValidator _adicionarFazendaValidator;
        private readonly ExcluirFazendaValidator _excluirFazendaValidator;

        public FazendaService(FarmPlannerContext context, FazendaValidator adicionarFazendaValidator, ExcluirFazendaValidator excluirFazendaValidator)
        {
            _context = context;
            _adicionarFazendaValidator = adicionarFazendaValidator;
            _excluirFazendaValidator = excluirFazendaValidator;
        }

        public async Task<EditarFazendaViewModel> AdicionarFazenda(EditarFazendaViewModel dados)
        {
            _adicionarFazendaValidator.ValidateAndThrow(dados);
            var Fazenda = new Fazenda();
            Fazenda.TipoArrenda = dados.TipoArrenda;
            Fazenda.IdCultura = dados.IdCultura;
            Fazenda.CodigoExterno = dados.CodigoExterno;
            Fazenda.Descricao = dados.Descricao;
            Fazenda.IdMunicipio = dados.IdMunicipio;
            Fazenda.IdRegiao = dados.IdRegiao;
            Fazenda.IdOrganizacao = dados.IdOrganizacao;
            Fazenda.ValorArrendamento = dados.ValorArrendamento;
            Fazenda.IdUF = dados.IdUF;
            Fazenda.Tipo = dados.Tipo;
            Fazenda.idconta = dados.idconta;
            await _context.AddAsync(Fazenda);
            await _context.SaveChangesAsync();
            return new EditarFazendaViewModel
            {
                TipoArrenda = dados.TipoArrenda,
                IdCultura = dados.IdCultura,
                CodigoExterno = dados.CodigoExterno,
                Descricao = dados.Descricao,
                IdMunicipio = dados.IdMunicipio,
                IdRegiao = dados.IdRegiao,
                IdOrganizacao = dados.IdOrganizacao,
                ValorArrendamento = dados.ValorArrendamento,
                Tipo = dados.Tipo,
                IdUF = dados.IdUF,
                Id = Fazenda.Id
            };
        }

        public async Task<EditarFazendaViewModel>? SalvarFazenda(int id, string idconta, EditarFazendaViewModel dados)
        {
            _adicionarFazendaValidator.ValidateAndThrow(dados);
            var Fazenda = _context.fazendas.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (Fazenda != null)
            {
                Fazenda.TipoArrenda = dados.TipoArrenda;
                Fazenda.IdCultura = dados.IdCultura;
                Fazenda.CodigoExterno = dados.CodigoExterno;
                Fazenda.Descricao = dados.Descricao;
                Fazenda.IdMunicipio = dados.IdMunicipio;
                Fazenda.IdRegiao = dados.IdRegiao;
                Fazenda.IdOrganizacao = dados.IdOrganizacao;
                Fazenda.ValorArrendamento = dados.ValorArrendamento;
                Fazenda.IdUF = dados.IdUF;
                Fazenda.Tipo = dados.Tipo;
                _context.Update(Fazenda);
                await _context.SaveChangesAsync();
                return new EditarFazendaViewModel
                {
                    TipoArrenda = dados.TipoArrenda,
                    IdCultura = dados.IdCultura,
                    CodigoExterno = dados.CodigoExterno,
                    Descricao = dados.Descricao,
                    IdMunicipio = dados.IdMunicipio,
                    IdRegiao = dados.IdRegiao,
                    IdOrganizacao = dados.IdOrganizacao,
                    ValorArrendamento = dados.ValorArrendamento,
                    idconta = dados.idconta,
                    Tipo = dados.Tipo,
                    IdUF = dados.IdUF,
                    Id = Fazenda.Id
                };
            }
            else return null;
        }

        public async Task<EditarFazendaViewModel>? ExcluirFazenda(int id, string idconta)
        {
            var Fazenda = _context.fazendas.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (Fazenda != null)
            {
                EditarFazendaViewModel dados = new EditarFazendaViewModel
                {
                    Id = Fazenda.Id
                };
                _excluirFazendaValidator.ValidateAndThrow(dados);
                _context.fazendas.Remove(Fazenda);
                await _context.SaveChangesAsync();
                return new EditarFazendaViewModel
                {
                    TipoArrenda = dados.TipoArrenda,
                    IdCultura = dados.IdCultura,
                    CodigoExterno = dados.CodigoExterno,
                    Descricao = dados.Descricao,
                    IdMunicipio = dados.IdMunicipio,
                    IdRegiao = dados.IdRegiao,
                    IdOrganizacao = dados.IdOrganizacao,
                    ValorArrendamento = dados.ValorArrendamento,
                    Tipo = dados.Tipo,
                    IdUF = dados.IdUF,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<EditarFazendaViewModel>? ListarFazendaById(string idconta, int id)
        {
            var Fazenda = _context.fazendas.Where(m => m.idconta == idconta && m.Id == id).FirstOrDefault();
            if (Fazenda != null)
            {
                return new EditarFazendaViewModel
                {
                    TipoArrenda = Fazenda.TipoArrenda,
                    IdCultura = Fazenda.IdCultura,
                    CodigoExterno = Fazenda.CodigoExterno,
                    Descricao = Fazenda.Descricao,
                    IdMunicipio = Fazenda.IdMunicipio,
                    IdRegiao = Fazenda.IdRegiao,
                    IdOrganizacao = Fazenda.IdOrganizacao,
                    ValorArrendamento = Fazenda.ValorArrendamento,
                    Tipo = Fazenda.Tipo,
                    IdUF = Fazenda.IdUF,
                    Id = Fazenda.Id,
                    idconta = Fazenda.idconta
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListFazendaViewModel>> ListarFazenda(int idorganizacao, int idregiao, string idconta, string? filtro)
        {
            var Fazendas = _context.fazendas.Where(m => ((idregiao == 0 || m.IdRegiao == idregiao) && m.IdOrganizacao == idorganizacao && m.idconta == idconta)
                 && (String.IsNullOrWhiteSpace(filtro)
                 || m.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Include(f => f.regiao)
                .Include(f => f.cultura)
                .Include(f => f.uF)
                .Include(f => f.municipio)
                .Select(c => new ListFazendaViewModel
                {
                    TipoArrenda = c.TipoArrenda,
                    IdCultura = c.IdCultura,
                    CodigoExterno = c.CodigoExterno,
                    Descricao = c.Descricao,
                    IdMunicipio = c.IdMunicipio,
                    IdRegiao = c.IdRegiao,
                    IdOrganizacao = c.IdOrganizacao,
                    ValorArrendamento = c.ValorArrendamento,
                    Tipo = c.Tipo,
                    IdUF = c.IdUF,
                    Id = c.Id,
                    desccultura = c.cultura.Descricao,
                    descregiao = c.regiao.Nome,
                    desctipoarrenda = c.TipoArrenda.ToString(),
                    desctipoprop = c.Tipo.ToString(),
                    nomemunicipio = c.municipio.Nome,
                    nomeuf = c.uF.Sigla
                }
                ).ToList();
            return (Fazendas);
        }
    }
}