using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class OrcamentoProduto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int IdSafra { get; set; }
        public int IdFazenda { get; set; }

        [ForeignKey("IdSafra,idconta")]
        public Safra safra { get; set; }

        [ForeignKey("IdFazenda,idconta")]
        public Fazenda fazenda { get; set; }

        public ICollection<ProdutoOrcamento> produtoorcamento { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }

        // public ICollection<PlanejamentoOperacao> planejamentoOperacao { get; set; }
    }
}