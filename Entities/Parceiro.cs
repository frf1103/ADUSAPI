using FarmPlannerAPI.Entities.Enum.FarmPlannerAPI.Entities.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Parceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public TipodePessoa TipodePessoa { get; set; }
        public string Registro { get; set; }

        public ICollection<Produto> Produtos { get; set; }
        public ICollection<Comercializacao> comercializacao { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}