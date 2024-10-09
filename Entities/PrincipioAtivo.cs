using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class PrincipioAtivo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public ICollection<ProdutoPrincipioAtivo> produtosprincipio { get; set; }

        public ICollection<ProdutoOrcamento>? produtoorcamento { get; set; }
        public ICollection<ProdutoPlanejado> produtosplanejados { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}