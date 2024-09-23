using FarmPlannerAPI.Entities.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string idconta { get; set; }
        public string Descricao { get; set; }
        public int IdGrupoProduto { get; set; }

        public int IdFabricante { get; set; }
        public int idunidade { get; set; }

        [ForeignKey("IdGrupoProduto")]
        public GrupoProduto grupoProduto { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        [ForeignKey("IdFabricante")]
        public Parceiro parceiro { get; set; }

        public ICollection<ProdutoOrcamento> produtoorcamento { get; set; }
        public ICollection<ProdutoPlanejado> produtosplanejados { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }

        [ForeignKey("idunidade")]
        public Unidade unidade { get; set; }

        public ICollection<ProdutoPrincipioAtivo> produtosprincipio { get; set; }

        public ICollection<ProdutoCompra>? produtospedidos { get; set; }
    }
}