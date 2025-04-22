using System.ComponentModel.DataAnnotations;

namespace ADUSAPI.Entities
{
    public class CentroCusto
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Descricao { get; set; }

        public ICollection<MovimentoCaixa>? movimentoCaixas { get; set; }

        public ICollection<ParametrosGuru>? parametros { get; set; }
    }
}