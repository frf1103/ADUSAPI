using FarmPlannerAPI.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ProdutoOrcamento
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdOrcamento { get; set; }
        public int TipoProdutoOrc { get; set; }
        public int? IdPrincipioAtivo { get; set; }
        public int IdProduto { get; set; }

        [Precision(18, 4)]
        public decimal PrecoUnitario { get; set; }

        public DateTime? DataPreco { get; set; }

        [ForeignKey("IdOrcamento")]
        public OrcamentoProduto orcamentoProduto { get; set; }

        [ForeignKey("IdPrincipioAtivo,idconta")]
        public PrincipioAtivo? princativo { get; set; }

        [ForeignKey("IdProduto")]
        public Produto produto { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}