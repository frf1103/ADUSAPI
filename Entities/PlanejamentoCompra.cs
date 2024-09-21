using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class PlanejamentoCompra
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdFazenda { get; set; }

        public int IdSafra { get; set; }
        public int IdPrincipio { get; set; }

        [Precision(18, 2)]
        public decimal QtdNecessaria { get; set; }

        [Precision(18, 2)]
        public decimal QtdEstoque { get; set; }

        [Precision(18, 2)]
        public decimal QtdComprar { get; set; }

        [Precision(18, 2)]
        public decimal QtdComprada { get; set; }

        [Precision(18, 2)]
        public decimal Saldo { get; set; }

        [ForeignKey("IdSafra,idconta")]
        public Safra safra { get; set; }

        [ForeignKey("IdPrincipio")]
        public PrincipioAtivo principio { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }

        [ForeignKey("IdFazenda")]
        public Fazenda fazenda { get; set; }
    }
}