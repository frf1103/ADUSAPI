using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class UsuarioConta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string uid { get; set; }

        public string contaguid { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uidlog { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}