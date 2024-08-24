using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Variedade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int IdCultura { get; set; }
        public int Ciclo { get; set; }
        public string? CodigoExterno { get; set; }
        public Cultura cultura { get; set; }
        public int IdTecnologia { get; set; }
        public Tecnologia tecnologia { get; set; }
        public ICollection<ConfigArea> configAreas { get; set; }
    }
}