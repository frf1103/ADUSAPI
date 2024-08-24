using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class AssinaturaConta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string idconta { get; set; }
        public int plano { get; set; }
        public DateTime dataassinatura { get; set; }
        public DateTime dataexpiracao { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }
    }
}