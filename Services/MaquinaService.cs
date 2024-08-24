using FarmPlannerAPI.Context;
using FarmPlannerAPI.Entities.Enum;
using FarmPlannerAPI.Entities;
using FarmPlannerAPI.Validators.Maquinas;
using FarmPlannerAPICore.Models.Maquinas;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace FarmPlannerAPI.Services
{
    public class MaquinaService
    {
        private readonly FarmPlannerContext _context;
        private readonly MaquinaValidator _adicionarMarcaValidator;
        private readonly ExcluirMaquinaValidator _excluirMarcaValidator;

        public MaquinaService(FarmPlannerContext context, MaquinaValidator adicionarMarcaValidator, ExcluirMaquinaValidator excluirMarcaValidator)
        {
            _context = context;
            _adicionarMarcaValidator = adicionarMarcaValidator;
            _excluirMarcaValidator = excluirMarcaValidator;
        }

        public async Task<MaquinaViewModel> AdicionarMaquina(MaquinaViewModel dados)
        {
            _adicionarMarcaValidator.ValidateAndThrow(dados);

            var Modelo = new Maquina();
            Modelo.Descricao = dados.Descricao;
            Modelo.IdModeloMaquina = dados.IdModeloMaquina;
            Modelo.Ano = dados.Ano;
            Modelo.CodigoExterno = dados.CodigoExterno;
            Modelo.idconta = dados.idconta;
            Modelo.idorganizacao = dados.idorganizacao;

            await _context.AddAsync(Modelo);
            await _context.SaveChangesAsync();
            return new MaquinaViewModel
            {
                Descricao = Modelo.Descricao,
                Id = Modelo.Id,
                idconta = Modelo.idconta,
                IdModeloMaquina = Modelo.IdModeloMaquina,
                Ano = Modelo.Ano,
                CodigoExterno = Modelo.CodigoExterno,
                idorganizacao = Modelo.idorganizacao
            };
        }

        public async Task<MaquinaViewModel>? SalvarMaquina(int id, string idconta, MaquinaViewModel dados)
        {
            var Modelo = _context.maquinas.Where(m => m.Id == id && m.idconta == idconta).FirstOrDefault();
            if (Modelo != null)
            {
                Modelo.Descricao = dados.Descricao;
                Modelo.IdModeloMaquina = dados.IdModeloMaquina;
                Modelo.Ano = dados.Ano;
                Modelo.CodigoExterno = dados.CodigoExterno;
                Modelo.idconta = dados.idconta;
                Modelo.idorganizacao = dados.idorganizacao;

                _context.Update(Modelo);
                await _context.SaveChangesAsync();
                return new MaquinaViewModel
                {
                    Descricao = Modelo.Descricao,
                    Id = Modelo.Id,
                    idconta = Modelo.idconta,
                    IdModeloMaquina = Modelo.IdModeloMaquina,
                    Ano = Modelo.Ano,
                    CodigoExterno = Modelo.CodigoExterno,
                    idorganizacao = Modelo.idorganizacao
                };
            }
            else return null;
        }

        public async Task<MaquinaViewModel>? ExcluirMaquina(int id, string idconta)
        {
            var Modelo = _context.maquinas.Where(m => m.Id == id && m.idconta == idconta).FirstOrDefault();
            if (Modelo != null)
            {
                MaquinaViewModel dados = new MaquinaViewModel
                {
                    Id = Modelo.Id
                };
                _excluirMarcaValidator.ValidateAndThrow(dados);
                _context.maquinas.Remove(Modelo);
                await _context.SaveChangesAsync();
                return new MaquinaViewModel
                {
                    Descricao = Modelo.Descricao,
                    Id = Modelo.Id,
                    idconta = Modelo.idconta,
                    IdModeloMaquina = Modelo.IdModeloMaquina,
                    Ano = Modelo.Ano,
                    CodigoExterno = Modelo.CodigoExterno,
                    idorganizacao = Modelo.idorganizacao
                };
            }
            else return null;
        }

        public async Task<MaquinaViewModel>? ListarMaquinaById(int id, string idconta)
        {
            var Modelo = _context.maquinas.Where(m => m.Id == id && m.idconta == idconta).FirstOrDefault();
            if (Modelo != null)
            {
                return new MaquinaViewModel
                {
                    Descricao = Modelo.Descricao,
                    Id = Modelo.Id,
                    idconta = Modelo.idconta,
                    IdModeloMaquina = Modelo.IdModeloMaquina,
                    Ano = Modelo.Ano,
                    CodigoExterno = Modelo.CodigoExterno,
                    idorganizacao = Modelo.idorganizacao
                };
            }
            else return null;
        }

        public async Task<IEnumerable<ListMaquinaViewModel>> ListarMaquina(string idconta, int idmodelo, int idorganizacao, string? filtro)
        {
            //var condicao = (Maquina m) => (String.IsNullOrWhiteSpace(filtro) || m.Descricao.ToUpper().Contains(filtro.ToUpper()));
            //var query = _context.marcasmaquinas.AsQueryable();
            var modelos = _context
                .maquinas
                .Where(c => (c.idconta == idconta) && (c.idorganizacao == idorganizacao) && (idmodelo == 0 || c.IdModeloMaquina == idmodelo) && (String.IsNullOrWhiteSpace(filtro) || c.Descricao.ToUpper().Contains(filtro.ToUpper())))
                .Include(c => c.modeloMaquina)

                //.Where(c => String.IsNullOrWhiteSpace(filtro) || c.Descricao.ToUpper().Contains(filtro.ToUpper()))
                //var Marcas = query.Where(condicao)
                .Select(c => new ListMaquinaViewModel
                {
                    idconta = c.idconta,
                    Ano = c.Ano,
                    CodigoExterno = c.CodigoExterno,
                    IdModeloMaquina = c.IdModeloMaquina,
                    DescModelo = c.modeloMaquina.Descricao,
                    Id = c.Id,
                    Descricao = c.Descricao,
                    idorganizacao = c.idorganizacao
                }
                ).ToList();
            return (modelos);
        }
    }
}