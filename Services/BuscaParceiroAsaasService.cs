using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ADUSAPI.Entities;
using ADUSAPI.Shared;
using ADUSAPI.Context;
using Microsoft.Extensions.Options;

namespace ADUSAPI.Services
{
    public class BuscarParceiroPorCustomerAsaasService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ASAASSettings _asaasSettings;
        private readonly ADUSContext _context;

        public BuscarParceiroPorCustomerAsaasService(IHttpClientFactory httpClientFactory, IOptions<ASAASSettings> asaasSettings, ADUSContext context)
        {
            _httpClientFactory = httpClientFactory;
            _asaasSettings = asaasSettings.Value;
            _context = context;
        }

        public async Task<string> BuscarIdParceiroPorCustomerId(string customerId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_asaasSettings.urlparceiro}/{customerId}");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("access_token", _asaasSettings.access_token);
            request.Headers.Add("User-Agent", _asaasSettings.useragent);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao buscar cliente Asaas: {response.StatusCode}");
            }

            var body = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(body);
            if (!jsonDoc.RootElement.TryGetProperty("cpfCnpj", out var cpfCnpjElement))
            {
                // throw new Exception("CPF/CNPJ não encontrado no retorno do Asaas.");
                return null;
            }

            string cpfCnpj = cpfCnpjElement.GetString();

            // Remove máscara se necessário
            cpfCnpj = FBSLIb.StringLib.Somentenumero(cpfCnpj);

            // Consultar na sua base ADUS
            var parceiro = await _context.parceiros
                .Where(p => p.Registro == cpfCnpj)
                .FirstOrDefaultAsync();

            if (parceiro == null)
            {
                throw new Exception($"Parceiro com CPF/CNPJ {cpfCnpj} não encontrado na base local.");
            }

            return parceiro.uid;
        }
    }
}