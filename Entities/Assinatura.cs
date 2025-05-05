using ADUSAPI.Entities.Enum.ADUSAPI.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using ADUSAPICore.Models.Enum;

namespace ADUSAPI.Entities
{
    public class Assinatura
    {
        public string id { get; set; }

        public string idparceiro { get; set; }
        public int qtd { get; set; }

        [Precision(18, 2)]
        public double preco { get; set; }

        [Precision(18, 2)]
        public double valor { get; set; }

        public DateTime datavenda { get; set; }
        public FormaPagto idformapagto { get; set; }

        public string? idplataforma { get; set; }
        public StatusAssinatura status { get; set; }
        public Parceiro parceiro { get; set; }

        public string? observacao { get; set; }

        public string? plataforma { get; set; }
        public DateTime datains { get; set; }
        public DateTime? dataup { get; set; }

        public ICollection<Parcela>? parcelas { get; set; }
    }
}