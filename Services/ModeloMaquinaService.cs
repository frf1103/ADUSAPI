using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Entities.Enum;
using FarmPlannerAPI.Validators.Maquinas;
using FarmPlannerAPICore.Models.Maquinas;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;

namespace FarmPlannerAPI.Services
{
    public class ModeloMaquinaService
    {
        private readonly FarmPlannerContext _context;
        private readonly ModeloMaquinaValidator _adicionarMarcaValidator;
        private readonly ExcluirModeloMaquinaValidator _excluirMarcaValidator;

        public ModeloMaquinaService(FarmPlannerContext context, ModeloMaquinaValidator adicionarMarcaValidator, ExcluirModeloMaquinaValidator excluirMarcaValidator)
        {
            _context = context;
            _adicionarMarcaValidator = adicionarMarcaValidator;
            _excluirMarcaValidator = excluirMarcaValidator;
        }

        public async Task<ModeloMaquinaViewModel> AdicionarModeloMaquina(ModeloMaquinaViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);

            var Modelo = new ModeloMaquina();
            Modelo.Descricao = dados.Descricao;
            Modelo.Combustivel = dados.Combustivel;
            Modelo.IdMarca = dados.IdMarca;

            await _context.AddAsync(Modelo);
            await _context.SaveChangesAsync();
            return new ModeloMaquinaViewModel
            {
                Descricao = Modelo.Descricao,
                Id = Modelo.Id,
                Combustivel = (int)Modelo.Combustivel,
                IdMarca = Modelo.IdMarca
            };
        }

        public async Task<ModeloMaquinaViewModel>? SalvarModeloMaquina(int id, ModeloMaquinaViewModel dados)
        {
            var Modelo = _context.modelosmaquinas.Find(id);
            if (Modelo != null)
            {
                Modelo.Descricao = dados.Descricao;
                Modelo.Combustivel = dados.Combustivel;
                Modelo.IdMarca = dados.IdMarca;
                _context.Update(Modelo);
                await _context.SaveChangesAsync();
                return new ModeloMaquinaViewModel
                {
                    Descricao = Modelo.Descricao,
                    Id = Modelo.Id,
                    Combustivel = (int)Modelo.Combustivel,
                    IdMarca = Modelo.IdMarca
                };
            }
            else return null;
        }

        public async Task<ModeloMaquinaViewModel>? ExcluirModeloMaquina(int id)
        {
            var Marca = _context.modelosmaquinas.Find(id);
            if (Marca != null)
            {
                ModeloMaquinaViewModel dados = new ModeloMaquinaViewModel
                {
                    Id = Marca.Id,
                    Descricao = Marca.Descricao,
                    Combustivel = (int)Marca.Combustivel,
                    IdMarca = Marca.IdMarca
                };
                _excluirMarcaValidator.ValidateAndThrow(dados);
                _context.modelosmaquinas.Remove(Marca);
                await _context.SaveChangesAsync();
                return new ModeloMaquinaViewModel
                {
                    Descricao = dados.Descricao,
                    Id = dados.Id,
                    Combustivel = dados.Combustivel,
                    IdMarca = dados.IdMarca
                };
            }
            else return null;
        }

        public async Task<ModeloMaquinaViewModel>? ListarModeloMaquinaById(int id)
        {
            var Marca = _context.modelosmaquinas.Where(c => c.Id == id).FirstOrDefault();
            if (Marca != null)
            {
                return new ModeloMaquinaViewModel
                {
                    Descricao = Marca.Descricao,
                    Id = Marca.Id,
                    Combustivel = (int)Marca.Combustivel,
                    //DescMarca = Marca.marcaMaquina.Descricao,
                    IdMarca = Marca.IdMarca
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListModeloMaquinaViewModel>> ListarModeloMaquina(string? filtro, int idmarca)
        {
            //var condicao = (ModeloMaquina m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            //var query = _context.marcasmaquinas.AsQueryable();
            var modelos = _context
                .modelosmaquinas
                .Where(c => (idmarca == 0 || c.IdMarca == idmarca) && (String.IsNullOrWhiteSpace(filtro) || c.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Include(c => c.marcaMaquina)

                //.Where(c => String.IsNullOrWhiteSpace(filtro) || c.Descricao.ToUpper().Contains(filtro.ToUpper()))
                //var Marcas = query.Where(condicao)
                .Select(c => new ListModeloMaquinaViewModel
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Combustivel = (int)c.Combustivel,
                    DescMarca = c.marcaMaquina.Descricao,
                    DescComb = c.Combustivel.ToString(),
                    IdMarca = c.IdMarca
                }
                ).ToList();
            return (modelos);
        }
    }
}