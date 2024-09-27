using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ProdutoCompra
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int idpedido { get; set; }
        public string idconta { get; set; }
        public int idproduto { get; set; }

        [Precision(20, 2)]
        public decimal qtdcompra { get; set; }

        [Precision(18, 4)]
        public decimal preco { get; set; }

        [Precision(20, 2)]
        public decimal total { get; set; }

        [Precision(20, 2)]
        public decimal recebido { get; set; }

        [ForeignKey("idpedido,idconta")]
        public PedidoCompra pedidocompra { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        [ForeignKey("idproduto,idconta")]
        public Produto produto { get; set; }

        public string uid { get; set; }
        public DateTime datains { get; set; }
        public DateTime? dataup { get; set; }

        public ICollection<EntregaCompra>? entregas { get; set; }
    }
}