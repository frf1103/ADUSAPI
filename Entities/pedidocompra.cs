using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class PedidoCompra
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int idsafra { get; set; }
        public int idfazenda { get; set; }
        public int idfornecedor { get; set; }
        public int idmoeda { get; set; }
        public DateTime vencimento { get; set; }

        [Precision(18, 2)]
        public decimal total { get; set; }

        [MaxLength(200)]
        public string? observacao { get; set; }

        public string idconta { get; set; }
        public string uid { get; set; }
        public DateTime datains { get; set; }
        public DateTime? dataup { get; set; }

        public DateTime datapedido { get; set; }

        [MaxLength(20)]
        public string? pedidofornecedor { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        [ForeignKey("idsafra,idconta")]
        public Safra safra { get; set; }

        [ForeignKey("idfornecedor,idconta")]
        public Parceiro parceiro { get; set; }

        [ForeignKey("idfazenda,idconta")]
        public Fazenda fazenda { get; set; }

        [ForeignKey("idmoeda")]
        public Moeda moeda { get; set; }

        public ICollection<ProdutoCompra>? produtos { get; set; }
    }
}