using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPI.Validators.Moeda;
using ADUSAPICore.Models.Moeda;
using FluentValidation;

namespace ADUSAPI.Services
{
    public class MoedaService
    {
        private readonly ADUSContext _context;
        private readonly MoedaValidator _adicionarMoedaValidator;
        private readonly ExcluirMoedaValidator _excluirMoedaValidator;

        public MoedaService(ADUSContext context, MoedaValidator adicionarMoedaValidator, ExcluirMoedaValidator excluirMoedaValidator)
        {
            _context = context;
            _adicionarMoedaValidator = adicionarMoedaValidator;
            _excluirMoedaValidator = excluirMoedaValidator;
        }

        public async Task<MoedaViewModel> AdicionarMoeda(MoedaViewModel dados)
        {
            _adicionarMoedaValidator.ValidateAndThrow(dados);
            var Moeda = new Moeda();
            Moeda.Descricao = dados.Descricao;

            await _context.AddAsync(Moeda);
            await _context.SaveChangesAsync();
            return new MoedaViewModel
            {
                Descricao = Moeda.Descricao,

                Id = Moeda.Id,
            };
        }

        public async Task<MoedaViewModel>? SalvarMoeda(int id, MoedaViewModel dados)
        {
            var Moeda = _context.moedas.Find(id);
            if (Moeda != null)
            {
                Moeda.Descricao = dados.Descricao;

                _context.Update(Moeda);
                await _context.SaveChangesAsync();
                return new MoedaViewModel
                {
                    Descricao = Moeda.Descricao,
                    Id = Moeda.Id
                };
            }
            else return null;
        }

        public async Task<MoedaViewModel>? ExcluirMoeda(int id)
        {
            var Moeda = _context.moedas.Find(id);
            MoedaViewModel dados = new MoedaViewModel
            {
                Descricao = Moeda.Descricao,
                Id = Moeda.Id
            };

            if (Moeda != null)
            {
                _excluirMoedaValidator.ValidateAndThrow(dados);
                _context.moedas.Remove(Moeda);
                await _context.SaveChangesAsync();
                return new MoedaViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<MoedaViewModel>? ListarMoedaById(int id)
        {
            var Moeda = _context.moedas.Find(id);
            if (Moeda != null)
            {
                return new MoedaViewModel
                {
                    Descricao = Moeda.Descricao,
                    Id = Moeda.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<MoedaViewModel>> ListarMoeda(string? filtro)
        {
            var condicao = (Moeda m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.moedas.AsQueryable();
            var Moedas = query.Where(condicao)
                .Select(c => new MoedaViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao
                }
                ).ToList();
            return (Moedas);
        }
    }
}