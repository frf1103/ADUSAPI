using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ProdutoPrincipioAtivo
    {
        public string idconta { get; set; }
        public int idproduto { get; set; }
        public int idprincipio { get; set; }

        [ForeignKey("idproduto")]
        public Produto produtoproduto { get; set; }

        [ForeignKey("idprincipio")]
        public PrincipioAtivo principioAtivo { get; set; }

        [Precision(18, 4)]
        public decimal quantidade { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }
    }
}