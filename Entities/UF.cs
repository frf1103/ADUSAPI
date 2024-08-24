using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class UF
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(2)]
        public string Sigla { get; set; }

        [MaxLength(2)]
        public string CodigoIBGE { get; set; }

        public ICollection<Municipio> Municipios { get; set; }
        public ICollection<Fazenda> fazendas { get; set; }
    }
}