using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class FinanceiroConta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string idconta { get; set; }

        public DateTime emissao { get; set; }
        public DateTime vencimento { get; set; }
        public DateTime datapagto { get; set; }

        [Precision(18, 2)]
        public decimal valor { get; set; }

        [Precision(18, 2)]
        public decimal desconto { get; set; }

        [Precision(18, 2)]
        public decimal valorpago { get; set; }

        public int tipo { get; set; }

        [MaxLength(250)]
        public string obs { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}