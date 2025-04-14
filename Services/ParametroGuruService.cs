using ADUSAPI.Context;
using ADUSAPI.Entities;

using ADUSAPICore.Models;

using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Services
{
    public class ParametrosGuruService
    {
        private readonly ADUSContext _context;

        public ParametrosGuruService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<ParametrosGuruViewModel>? SalvarParametrosGuru(int id, ParametrosGuruViewModel dados)
        {
            var conta = _context.parametrosguru.Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                conta.ultdata = dados.ultdata;
                conta.token = dados.token;
                conta.urlsub = dados.urlsub;
                conta.urltransac = dados.urltransac;
                _context.Update(conta);
                await _context.SaveChangesAsync();
                return new ParametrosGuruViewModel
                {
                    id = conta.id,
                    ultdata = conta.ultdata,
                    urltransac = conta.urltransac,
                    urlsub = conta.urlsub,
                    token = conta.token
                };
            }
            else return null;
        }

        public async Task<ParametrosGuruViewModel>? ListarParametrosGuruById(int id)
        {
            var conta = _context.parametrosguru
            .Where(p => p.id == id).FirstOrDefault();
            if (conta != null)
            {
                return new ParametrosGuruViewModel
                {
                    id = conta.id,
                    ultdata = conta.ultdata,
                    urltransac = conta.urltransac,
                    urlsub = conta.urlsub,
                    token = conta.token
                };
            }
            else return null;
        }
    }
}