using System;

namespace ADUSAPI.Entities
{
    public class LogCheckout
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string IpOrigem { get; set; }
        public string TipoOperacao { get; set; } // Nome da operação (ex: "Tokenizacao", "CriarAssinatura")
        public string UrlRequisicao { get; set; }
        public string PayloadEnviado { get; set; }
        public string RetornoApi { get; set; }
        public DateTime DataHora { get; set; }
        public string StatusHttp { get; set; }
        public string? Erro { get; set; }
        public string? idparcela { get; set; }
        public Parcela? parcela { get; set; }
    }
}