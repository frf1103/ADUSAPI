using ADUSAPI.Context;

using ADUSAPI.Entities;

using ADUSAPICore.Models;

using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Services
{
    public class WebhookAsaasService
    {
        private readonly ADUSContext _context;

        public WebhookAsaasService(ADUSContext context)
        {
            _context = context;
        }

        public async Task Adicionar(WebhookAsaasViewModel viewModel)
        {
            var entity = new WebhookAsaas
            {
                Evento = viewModel.Evento,
                PaymentId = viewModel.PaymentId,
                SubscriptionId = viewModel.SubscriptionId,
                CustomerId = viewModel.CustomerId,
                Status = viewModel.Status,
                BillingType = viewModel.BillingType,
                Valor = viewModel.Valor,
                DataVencimento = viewModel.DataVencimento,
                JsonCompleto = viewModel.JsonCompleto
            };

            _context.webhookAsaas.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WebhookAsaas>> ListarTodos()
        {
            return await _context.webhookAsaas.OrderByDescending(x => x.DataRecebimento).ToListAsync();
        }

        public async Task<WebhookAsaas> BuscarPorId(string id)
        {
            return await _context.webhookAsaas.FindAsync(id);
        }

        public async Task Excluir(string id)
        {
            var entity = await _context.webhookAsaas.FindAsync(id);
            if (entity != null)
            {
                _context.webhookAsaas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}