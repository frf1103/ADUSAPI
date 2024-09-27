using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class EntregaCompra
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int idprodutopedido { get; set; }

        public int idpedido { get; set; }
        public int idproduto { get; set; }

        public int idunidentrega { get; set; }

        [Precision(18, 4)]
        public decimal conversor { get; set; }

        public DateTime dataentrega { get; set; }

        [MaxLength(60)]
        public string documento { get; set; }

        [Precision(20, 2)]
        public decimal qtd { get; set; }

        public string idconta { get; set; }
        public string uid { get; set; }
        public DateTime datains { get; set; }
        public DateTime? dataup { get; set; }

        [ForeignKey("idprodutopedido,idconta,idpedido")]
        public ProdutoCompra produtoCompra { get; set; }

        [ForeignKey("idproduto,idconta")]
        public Produto produto { get; set; }
    }
}