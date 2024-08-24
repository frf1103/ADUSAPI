using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Maquinas;

using FarmPlannerAPICore.Models.Maquinas;

using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class MarcaMaquinaService
    {
        private readonly FarmPlannerContext _context;
        private readonly MarcaMaquinaValidator _adicionarMarcaValidator;
        private readonly ExcluirMarcaMaquinaValidator _excluirMarcaValidator;

        public MarcaMaquinaService(FarmPlannerContext context, MarcaMaquinaValidator adicionarMarcaValidator, ExcluirMarcaMaquinaValidator excluirMarcaValidator)
        {
            _context = context;
            _adicionarMarcaValidator = adicionarMarcaValidator;
            _excluirMarcaValidator = excluirMarcaValidator;
        }

        public async Task<MarcaMaquinaViewModel> AdicionarMarcaMaquina(MarcaMaquinaViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);
            var Marca = new MarcaMaquina();
            Marca.Descricao = dados.Descricao;

            await _context.AddAsync(Marca);
            await _context.SaveChangesAsync();
            return new MarcaMaquinaViewModel
            {
                Descricao = Marca.Descricao,

                Id = Marca.Id,
            };
        }

        public async Task<MarcaMaquinaViewModel>? SalvarMarcaMaquina(int id, MarcaMaquinaViewModel dados)
        {
            var Marca = _context.marcasmaquinas.Find(id);
            if (Marca != null)
            {
                Marca.Descricao = dados.Descricao;

                _context.Update(Marca);
                await _context.SaveChangesAsync();
                return new MarcaMaquinaViewModel
                {
                    Descricao = Marca.Descricao,
                    Id = Marca.Id
                };
            }
            else return null;
        }

        public async Task<MarcaMaquinaViewModel>? ExcluirMarcaMaquina(int id)
        {
            var Marca = _context.marcasmaquinas.Find(id);
            if (Marca != null)
            {
                MarcaMaquinaViewModel dados = new MarcaMaquinaViewModel
                {
                    Descricao = Marca.Descricao,
                    Id = Marca.Id
                };
                _excluirMarcaValidator.ValidateAndThrow(dados);

                _context.marcasmaquinas.Remove(Marca);
                await _context.SaveChangesAsync();
                return new MarcaMaquinaViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id
                };
            }
            else return null;
        }

        public async Task<MarcaMaquinaViewModel>? ListarMarcaMaquinaById(int id)
        {
            var Marca = _context.marcasmaquinas.Find(id);
            if (Marca != null)
            {
                return new MarcaMaquinaViewModel
                {
                    Descricao = Marca.Descricao,
                    Id = Marca.Id
                };
            }
            else return null;
        }

        public async Task<IEnumerable<MarcaMaquinaViewModel>> ListarMarcaMaquina(string? filtro)
        {
            var condicao = (MarcaMaquina m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            var query = _context.marcasmaquinas.AsQueryable();
            var Marcas = query.Where(condicao)
                .Select(c => new MarcaMaquinaViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao
                }
                ).ToList();
            return (Marcas);
        }
    }
}