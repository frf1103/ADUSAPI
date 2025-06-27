using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADUSAPICore.Models.Checkout;
using ADUSAPI.Context;
using ADUSAPI.Entities;

namespace ADUSAPI.Services
{
    public class LogCheckoutService
    {
        private readonly ADUSContext _context;

        public LogCheckoutService(ADUSContext context)
        {
            _context = context;
        }

        public async Task Adicionar(LogCheckoutViewModel vm)
        {
            var entity = new LogCheckout
            {
                NomeCliente = vm.NomeCliente,
                IpOrigem = vm.IpOrigem,
                PayloadEnviado = vm.PayloadEnviado,
                StatusHttp = vm.StatusHttp,
                TipoOperacao = vm.TipoOperacao,
                UrlRequisicao = vm.UrlRequisicao,
                Erro = vm.Erro,
                RetornoApi = vm.RetornoApi,
                DataHora = DateTime.Now
            };

            _context.logscheckout.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LogCheckoutViewModel>> Listar(DateTime ini, DateTime fim, string filtro)
        {
            return await _context.logscheckout
                .Where(x => x.DataHora >= ini && x.DataHora <= fim
                && (string.IsNullOrWhiteSpace(filtro) || x.NomeCliente.ToUpper().Contains(filtro.ToUpper())))
                .AsNoTracking()
                .OrderByDescending(x => x.DataHora)
                .Select(x => new LogCheckoutViewModel
                {
                    Id = x.Id,
                    NomeCliente = x.NomeCliente,
                    IpOrigem = x.IpOrigem,
                    UrlRequisicao = x.UrlRequisicao,
                    TipoOperacao = x.TipoOperacao,
                    PayloadEnviado = x.PayloadEnviado,
                    StatusHttp = x.StatusHttp,
                    RetornoApi = x.RetornoApi,
                    Erro = x.Erro,
                    DataHora = x.DataHora
                }).ToListAsync();
        }

        public async Task<LogCheckoutViewModel> GetById(int id)
        {
            var entity = await _context.logscheckout.FindAsync(id);
            if (entity == null) return null;

            return new LogCheckoutViewModel
            {
                Id = entity.Id,
                NomeCliente = entity.NomeCliente,
                IpOrigem = entity.IpOrigem,
                UrlRequisicao = entity.UrlRequisicao,
                TipoOperacao = entity.TipoOperacao,
                PayloadEnviado = entity.PayloadEnviado,
                StatusHttp = entity.StatusHttp,
                RetornoApi = entity.RetornoApi,
                Erro = entity.Erro,
                DataHora = entity.DataHora
            };
        }

        public async Task Excluir(int id)
        {
            var entity = await _context.logscheckout.FindAsync(id);
            if (entity != null)
            {
                _context.logscheckout.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}