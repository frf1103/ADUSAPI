using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class OrganizacaoUsuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; }

        public string uid { get; set; }
        public int idorganizacao { get; set; }

        [ForeignKey("idorganizacao")]
        public Organizacao organizacao { get; set; }

        public string? uidlog { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}