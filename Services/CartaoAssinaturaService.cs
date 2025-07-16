using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using ADUSAPI.Context;
using ADUSAPICore.Models.Assinatura;

namespace ADUSAPI.Services
{
    public class CartaoAssinaturaService
    {
        private readonly ADUSContext _context;

        public CartaoAssinaturaService(ADUSContext context)
        {
            _context = context;
        }

        public async Task<List<CartaoAssinaturaViewModel>> ListarPorAssinaturaAsync(string idAssinatura)
        {
            var cc = await _context.cartoes
                                 .Where(c => c.IdAssinatura == idAssinatura)
                                 .ToListAsync();
            return cc.Select(x => new CartaoAssinaturaViewModel
            {
                UltimosDigitos = x.UltimosDigitos,
                IdAssinatura = x.IdAssinatura,
                Ativo = x.Ativo,
                Bandeira = x.Bandeira,
                IdToken = x.IdToken,
                Id = x.Id
            }).ToList();
        }

        public async Task<CartaoAssinaturaViewModel> ObterPorIdAsync(int id)
        {
            var cc = await _context.cartoes.FindAsync(id);
            if (cc != null)
            {
                return new CartaoAssinaturaViewModel
                {
                    IdToken = cc.IdToken,
                    Id = cc.Id,
                    Ativo = cc.Ativo,
                    Bandeira = cc.Bandeira,
                    IdAssinatura = cc.IdAssinatura,
                    UltimosDigitos = cc.UltimosDigitos
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<CartaoAssinaturaViewModel> ObterPorTokenAsync(string idtoken)
        {
            var cc = await _context.cartoes.Where(x => x.IdToken == idtoken).FirstOrDefaultAsync();
            if (cc != null)
            {
                return new CartaoAssinaturaViewModel
                {
                    IdToken = cc.IdToken,
                    Id = cc.Id,
                    Ativo = cc.Ativo,
                    Bandeira = cc.Bandeira,
                    IdAssinatura = cc.IdAssinatura,
                    UltimosDigitos = cc.UltimosDigitos
                };
            }
            else return null;
        }

        public async Task<CartaoAssinaturaViewModel> AdicionarAsync(CartaoAssinaturaViewModel cartao)
        {
            var cc = new CartaoAssinatura();
            cc.IdAssinatura = cartao.IdAssinatura;
            cc.Bandeira = cartao.Bandeira;
            cc.Ativo = cartao.Ativo;
            cc.UltimosDigitos = cartao.UltimosDigitos;
            cc.IdToken = cartao.IdToken;
            _context.cartoes.Add(cc);
            await _context.SaveChangesAsync();
            return new CartaoAssinaturaViewModel
            {
                IdToken = cc.IdToken,
                Id = cc.Id,
                Ativo = cc.Ativo,
                Bandeira = cc.Bandeira,
                IdAssinatura = cc.IdAssinatura,
                UltimosDigitos = cc.UltimosDigitos
            };
        }

        public async Task AtivarCartaoAsync(int id)
        {
            var cartao = await _context.cartoes.FindAsync(id);
            if (cartao != null)
            {
                cartao.Ativo = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task InativarCartaoAsync(int id)
        {
            var cartao = await _context.cartoes.FindAsync(id);
            if (cartao != null)
            {
                cartao.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}