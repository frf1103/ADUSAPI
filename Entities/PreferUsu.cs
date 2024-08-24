using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class PreferUsu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string uid { get; set; }
        public int idorganizacao { get; set; }
        public int idanoagricola { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idorganizacao,idconta")]
        public Organizacao organizacao { get; set; }

        [ForeignKey("idanoagricola,idconta")]
        public AnoAgricola anoAgricola { get; set; }

        public string? uidlog { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}