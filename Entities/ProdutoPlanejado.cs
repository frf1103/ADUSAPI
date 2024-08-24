using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ProdutoPlanejado
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Precision(18, 2)]
        public decimal Tamanho { get; set; }

        [Precision(6, 2)]
        public decimal AreaPercent { get; set; }

        public int IdPlanejamento { get; set; }
        public int? IdPrincipioAtivo { get; set; }
        public int? IdProduto { get; set; }

        [Precision(18, 2)]
        public decimal Dosagem { get; set; }

        [Precision(18, 2)]
        public decimal TotalProduto { get; set; }

        [ForeignKey("IdPlanejamento,idconta")]
        public PlanejamentoOperacao planejamentoOperacao { get; set; }

        [ForeignKey("IdProduto,idconta")]
        public Produto? produto { get; set; }

        [ForeignKey("IdPrincipioAtivo,idconta")]
        public PrincipioAtivo? principioativo { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}