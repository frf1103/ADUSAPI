using System.ComponentModel.DataAnnotations.Schema;

namespace ADUSAPI.Entities;

public class Moeda
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Descricao { get; set; }
    public ICollection<CotacaoMoeda> cotacaoMoedas { get; set; }
}