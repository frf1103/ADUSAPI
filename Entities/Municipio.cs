using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Municipio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; }

        [MaxLength(7)]
        public string CodigoIBGE { get; set; }

        public int IdUF { get; set; }
        public UF uF { get; set; }
        public ICollection<Fazenda> fazendas { get; set; }
    }
}