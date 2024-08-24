using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.CustosIndiretos;

using FarmPlannerAPICore.Models.CustosIndiretos;

using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Services
{
    public class OrcamentoCustoIndiretoService
    {
        private readonly FarmPlannerContext _context;
        private readonly OrcamentoCustoIndiretoValidator _adicionarOrcamentoCustoIndiretoValidator;
        private readonly ExcluirOrcamentoCustoIndiretoValidator _excluirOrcamentoCustoIndiretoValidator;

        public OrcamentoCustoIndiretoService(FarmPlannerContext context, OrcamentoCustoIndiretoValidator adicionarOrcamentoCustoIndiretoValidator, ExcluirOrcamentoCustoIndiretoValidator excluirOrcamentoCustoIndiretoValidator)
        {
            _context = context;
            _adicionarOrcamentoCustoIndiretoValidator = adicionarOrcamentoCustoIndiretoValidator;
            _excluirOrcamentoCustoIndiretoValidator = excluirOrcamentoCustoIndiretoValidator;
        }

        public async Task<OrcamentoCustoIndiretoViewModel> AdicionarOrcamentoCustoIndireto(OrcamentoCustoIndiretoViewModel dados)
        {
            _adicionarOrcamentoCustoIndiretoValidator.ValidateAndThrow(dados);
            var OrcamentoCustoIndireto = new OrcamentoCustoIndireto();
            OrcamentoCustoIndireto.idcontaCad = dados.idcontaCad;
            OrcamentoCustoIndireto.Data = dados.Data;
            OrcamentoCustoIndireto.IdSafra = dados.IdSafra;
            OrcamentoCustoIndireto.valor = dados.valor;
            OrcamentoCustoIndireto.idconta = dados.idconta;

            await _context.AddAsync(OrcamentoCustoIndireto);
            await _context.SaveChangesAsync();
            return new OrcamentoCustoIndiretoViewModel
            {
                Data = OrcamentoCustoIndireto.Data,
                Id = OrcamentoCustoIndireto.Id,
                idcontaCad = OrcamentoCustoIndireto.idcontaCad,
                valor = OrcamentoCustoIndireto.valor,
                IdSafra = OrcamentoCustoIndireto.IdSafra
            };
        }

        public async Task<OrcamentoCustoIndiretoViewModel>? SalvarOrcamentoCustoIndireto(int id, OrcamentoCustoIndiretoViewModel dados)
        {
            var OrcamentoCustoIndireto = _context.orcamentocustosindiretos.Find(id);
            if (OrcamentoCustoIndireto != null)
            {
                OrcamentoCustoIndireto.idcontaCad = dados.idcontaCad;
                OrcamentoCustoIndireto.Data = dados.Data;
                OrcamentoCustoIndireto.IdSafra = dados.IdSafra;
                OrcamentoCustoIndireto.valor = dados.valor;

                _context.Update(OrcamentoCustoIndireto);
                await _context.SaveChangesAsync();
                return new OrcamentoCustoIndiretoViewModel
                {
                    Data = OrcamentoCustoIndireto.Data,
                    Id = OrcamentoCustoIndireto.Id,
                    idcontaCad = OrcamentoCustoIndireto.idcontaCad,
                    valor = OrcamentoCustoIndireto.valor,
                    IdSafra = OrcamentoCustoIndireto.IdSafra
                };
            }
            else return null;
        }

        public async Task<OrcamentoCustoIndiretoViewModel>? ExcluirOrcamentoCustoIndireto(int id, OrcamentoCustoIndiretoViewModel dados)
        {
            _excluirOrcamentoCustoIndiretoValidator.ValidateAndThrow(dados);
            var OrcamentoCustoIndireto = _context.orcamentocustosindiretos.Find(id);
            if (OrcamentoCustoIndireto != null)
            {
                _context.orcamentocustosindiretos.Remove(OrcamentoCustoIndireto);
                await _context.SaveChangesAsync();
                return new OrcamentoCustoIndiretoViewModel
                {
                    Data = OrcamentoCustoIndireto.Data,
                    Id = OrcamentoCustoIndireto.Id,
                    idcontaCad = OrcamentoCustoIndireto.idcontaCad,
                    valor = OrcamentoCustoIndireto.valor,
                    IdSafra = OrcamentoCustoIndireto.IdSafra
                };
            }
            else return null;
        }

        public async Task<OrcamentoCustoIndiretoViewModel>? ListarOrcamentoCustoIndiretoById(int id)
        {
            var OrcamentoCustoIndireto = _context.orcamentocustosindiretos.Find(id);
            if (OrcamentoCustoIndireto != null)
            {
                return new OrcamentoCustoIndiretoViewModel
                {
                    Data = OrcamentoCustoIndireto.Data,
                    Id = OrcamentoCustoIndireto.Id,
                    idcontaCad = OrcamentoCustoIndireto.idcontaCad,
                    valor = OrcamentoCustoIndireto.valor,
                    IdSafra = OrcamentoCustoIndireto.IdSafra
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListOrcamentoCustoIndiretoViewModel>> ListarOrcamentoCustoIndireto(int idcontacad, string idconta, int idsafra, DateTime? dini, DateTime? dfim)
        {
            //  var condicao = (OrcamentoCustoIndireto m) => (m.idconta == idconta) && (idtipo == 0 || m.IdTipoOrcamentoCustoIndireto == idtipo) && (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.orcamentocustosindiretos.AsQueryable()
                .Include(x => x.safra).Include(x => x.conta).Include(x => x.safra.anoAgricola.organizacao);
            var OrcamentoCustoIndiretos = query.Where(m => m.safra.anoAgricola.organizacao.idconta == idconta &&
            m.Data >= dini && m.Data <= dfim && (idcontacad == 0 || m.idcontaCad == idcontacad) && (idsafra == 0 || m.IdSafra == idsafra))

                .Select(c => new ListOrcamentoCustoIndiretoViewModel
                {
                    Data = c.Data,
                    Id = c.Id,
                    idcontaCad = c.idcontaCad,
                    valor = c.valor,
                    IdSafra = c.IdSafra,
                    descconta = c.contacad.Descricao,
                    descsafra = c.safra.Descricao
                }
                ).ToList();
            return (OrcamentoCustoIndiretos);
        }
    }
}