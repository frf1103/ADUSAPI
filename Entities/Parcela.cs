using ADUSAPICore.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Entities
{
    public class Parcela
    {
        public string id { get; set; }
        public string idassinatura { get; set; }
        public int numparcela { get; set; }
        public int? idcaixa { get; set; }
        public FormaPagto idformapagto { get; set; }
        public DateTime datavencimento { get; set; }
        public DateTime? databaixa { get; set; }
        public string plataforma { get; set; }

        [Precision(18, 2)]
        public double valor { get; set; }

        [Precision(18, 2)]
        public double comissao { get; set; }

        [Precision(18, 2)]
        public double descontoplataforma { get; set; }

        [Precision(18, 2)]
        public double descontoantecipacao { get; set; }

        [Precision(18, 2)]
        public double valorliquido { get; set; }

        [Precision(18, 2)]
        public double acrescimos { get; set; }

        [Precision(18, 2)]
        public double descontos { get; set; }

        public string? observacao { get; set; }

        public string? idcheckout { get; set; }

        public string? nossonumero { get; set; }
        public Assinatura assinatura { get; set; }
        public DateTime datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}