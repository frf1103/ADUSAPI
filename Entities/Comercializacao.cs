using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class Comercializacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdSafra { get; set; }

        public int IdFazenda { get; set; }
        public int IdParceiro { get; set; }

        [Precision(18, 2)]
        public decimal Quantidade { get; set; }

        public int IdMoeda { get; set; }
        public bool Trava { get; set; }

        [Precision(18, 2)]
        public decimal CBOT { get; set; }

        [Precision(18, 2)]
        public decimal Cambio { get; set; }

        [Precision(18, 2)]
        public decimal Premio { get; set; }

        [Precision(18, 2)]
        public decimal ValorUnitario { get; set; }

        [Precision(18, 2)]
        public decimal ValorTotal { get; set; }

        public DateTime DataEntrega { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime? DataPedido { get; set; }

        [Precision(18, 2)]
        public decimal Descontos { get; set; }

        [Precision(18, 2)]
        public decimal ValorLiquido { get; set; }

        public string? NumeroContrato { get; set; }

        [Precision(18, 2)]
        public decimal? Frete { get; set; }

        [ForeignKey("IdSafra,idconta")]
        public Safra safra { get; set; }

        [ForeignKey("IdMoeda")]
        public Moeda moeda { get; set; }

        [ForeignKey("IdParceiro,idconta")]
        public Parceiro parceiro { get; set; }

        public ICollection<EntregaContrato> EntregaContrato { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        [ForeignKey("IdFazenda,idconta")]
        public Fazenda fazenda { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}