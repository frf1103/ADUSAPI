using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Conta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string? ContaGuid { get; set; }

        public string? uid { get; set; }
        public string? telefone { get; set; }

        public string? representanteid { get; set; }
        public bool? ativa { get; set; } = true;

        public ICollection<Organizacao> organizacoes { get; set; }

        public ICollection<Produto> produtos { get; set; }

        public ICollection<Operacao> operacoes { get; set; }

        public ICollection<UsuarioConta> usuarios { get; set; }

        public ICollection<AnoAgricola> anoAgricolas { get; set; }

        public ICollection<CadastroConta> cadastroContas { get; set; }
        public ICollection<Comercializacao> comercializas { get; set; }
        public ICollection<EntregaContrato> entregaContrato { get; set; }
        public ICollection<Fazenda> fazendas { get; set; }
        public ICollection<GrupoConta> grupoContas { get; set; }
        public ICollection<Maquina> maquinas { get; set; }
        public ICollection<MaquinaParametro> maquinaParametros { get; set; }

        public ICollection<MaquinaPlanejada> maquinaPlanejada { get; set; }
        public ICollection<ModeloParametro> modeloParametros { get; set; }
        public ICollection<OrcamentoCustoIndireto> orcamentoCustoIndiretos { get; set; }
        public ICollection<OrcamentoProduto> orcamentoProdutos { get; set; }
        public ICollection<PlanejamentoCompra> planejamentoCompras { get; set; }
        public ICollection<PlanejamentoOperacao> planejamentoOperacao { get; set; }
        public ICollection<ProdutoOrcamento> produtoOrcamento { get; set; }
        public ICollection<ProdutoPlanejado> produtoPlanejado { get; set; }
        public ICollection<Safra> safras { get; set; }
        public ICollection<Talhao> talhaos { get; set; }
        public ICollection<ConfigArea> configArea { get; set; }
        public ICollection<FinanceiroConta> financeiroContas { get; set; }
        public ICollection<AssinaturaConta> assinaturaContas { get; set; }

        public ICollection<Parceiro> parceiros { get; set; }
        public ICollection<PrincipioAtivo> principioAtivos { get; set; }
    }
}