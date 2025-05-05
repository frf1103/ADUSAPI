using ADUSAPI.Context;
using ADUSAPI.Entities;

using ADUSAPICore.Models.MovimentoCaixa;

using Microsoft.EntityFrameworkCore;

public class MovimentoCaixaService
{
    private readonly ADUSContext _context;

    public MovimentoCaixaService(ADUSContext context)
    {
        _context = context;
    }

    public async Task<List<MovimentoCaixaViewModel>> ListarAsync(
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        int? idTransacao = null,
        int? idCentroCusto = null,
        string? idParceiro = null,
        string? idContaCorrente = null,
        int? idCategoria = null,
        string? descricao = null,
        string? idmovbanco = null)
    {
        var query = _context.movimentoCaixas
            .Include(m => m.Transacao)
            .Include(m => m.CentroCusto)
            .Include(m => m.ContaCorrente)
            .Include(m => m.Categoria)
            .AsQueryable();

        if (dataInicio.HasValue)
            query = query.Where(m => m.DataMov >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(m => m.DataMov <= dataFim.Value);

        if (idTransacao.HasValue)
            query = query.Where(m => m.IdTransacao == idTransacao.Value);

        if (idCentroCusto.HasValue)
            query = query.Where(m => m.IdCentroCusto == idCentroCusto.Value);

        if (!string.IsNullOrEmpty(idParceiro))
            query = query.Where(m => m.idparceiro == idParceiro);

        if (idContaCorrente != null)
            query = query.Where(m => m.IdContaCorrente == idContaCorrente);

        if (idCategoria.HasValue)
            query = query.Where(m => m.IdCategoria == idCategoria.Value);

        if (!string.IsNullOrWhiteSpace(descricao))
            query = query.Where(m => m.Observacao.Contains(descricao) || m.idmovbanco.Contains(descricao));

        if (!string.IsNullOrWhiteSpace(idmovbanco))
            query = query.Where(m => m.idmovbanco == idmovbanco);

        return await query
            .OrderByDescending(m => m.DataMov)
            .Select(m => new MovimentoCaixaViewModel
            {
                Id = m.Id,
                IdTransacao = m.IdTransacao,
                IdCentroCusto = m.IdCentroCusto,
                IdContaCorrente = m.IdContaCorrente,
                IdCategoria = m.IdCategoria,
                Sinal = m.Sinal,
                Observacao = m.Observacao,
                Valor = m.Valor,
                DataMov = m.DataMov,
                DescTransacao = m.Transacao.Descricao,
                DescCentroCusto = m.CentroCusto.Descricao,
                DescContaCorrente = m.ContaCorrente.Descricao,
                DescCategoria = m.Categoria.Descricao,
                idparceiro = m.idparceiro,
                nomeparceiro = m.parceiro.RazaoSocial,
                idmovbanco = m.idmovbanco
            }).ToListAsync();
    }

    public async Task<MovimentoCaixaViewModel?> ObterPorIdAsync(int id)
    {
        return await _context.movimentoCaixas
            .Include(m => m.Transacao)
            .Include(m => m.CentroCusto)
            .Include(m => m.ContaCorrente)
            .Include(m => m.Categoria)
            .Include(m => m.parceiro)
            .Where(m => m.Id == id)
            .Select(m => new MovimentoCaixaViewModel
            {
                Id = m.Id,
                IdTransacao = m.IdTransacao,
                IdCentroCusto = m.IdCentroCusto,
                IdContaCorrente = m.IdContaCorrente,
                IdCategoria = m.IdCategoria,
                Sinal = m.Sinal,
                Observacao = m.Observacao,
                Valor = m.Valor,
                DataMov = m.DataMov,
                DescTransacao = m.Transacao.Descricao,
                DescCentroCusto = m.CentroCusto.Descricao,
                DescContaCorrente = m.ContaCorrente.Descricao,
                DescCategoria = m.Categoria.Descricao,
                idparceiro = m.idparceiro,
                nomeparceiro = m.parceiro.RazaoSocial,
                idmovbanco = m.idmovbanco
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MovimentoCaixaViewModel> AdicionarAsync(MovimentoCaixaViewModel entidade)
    {
        MovimentoCaixa mc = new MovimentoCaixa
        {
            IdContaCorrente = entidade.IdContaCorrente,
            DataMov = entidade.DataMov,
            IdCategoria = entidade.IdCategoria,
            IdCentroCusto = entidade.IdCentroCusto,
            idparceiro = entidade.idparceiro,
            IdTransacao = entidade.IdTransacao,
            Valor = entidade.Valor,
            Observacao = entidade.Observacao,
            Sinal = entidade.Sinal,
            idmovbanco = entidade.idmovbanco
        };
        _context.movimentoCaixas.Add(mc);
        await _context.SaveChangesAsync();
        return new MovimentoCaixaViewModel
        {
            Id = mc.Id,
            IdContaCorrente = mc.IdContaCorrente,
            DataMov = mc.DataMov,
            IdCategoria = mc.IdCategoria,
            IdCentroCusto = mc.IdCentroCusto,
            idparceiro = mc.idparceiro,
            IdTransacao = mc.IdTransacao,
            Valor = mc.Valor,
            Observacao = mc.Observacao,
            Sinal = mc.Sinal,
            idmovbanco = mc.idmovbanco
        };
    }

    public async Task AtualizarAsync(MovimentoCaixaViewModel entidade)
    {
        var mc = await _context.movimentoCaixas.FindAsync(entidade.Id);
        if (mc != null)
        {
            mc.Sinal = entidade.Sinal;
            mc.IdContaCorrente = entidade.IdContaCorrente;
            mc.IdCentroCusto = entidade.IdCentroCusto;
            mc.DataMov = entidade.DataMov;
            mc.Observacao = entidade.Observacao;
            mc.idparceiro = entidade.idparceiro;
            mc.Valor = entidade.Valor;
            mc.IdCategoria = entidade.IdCategoria;
            mc.IdTransacao = entidade.IdTransacao;
            mc.idmovbanco = entidade.idmovbanco;
            _context.movimentoCaixas.Update(mc);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoverAsync(int id)
    {
        var entidade = await _context.movimentoCaixas.FindAsync(id);
        if (entidade != null)
        {
            _context.movimentoCaixas.Remove(entidade);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ExtratoConta>> ExtratoConta(DateTime ini, DateTime fim, string idconta)
    {
        var hoje = DateTime.Today;
        var saldoAnterior = _context.movimentoCaixas
     .Where(m => m.IdContaCorrente == idconta && m.DataMov < ini)
     .Sum(m => m.Sinal == "C" ? m.Valor : -1 * m.Valor);

        var resultado = new ExtratoConta
        {
            datamov = ini.AddDays(-1),
            saldo = saldoAnterior,
            debito = null,
            credito = null,
            historico = "SALDO ANTERIOR",
            sinal = "C",
            tipo = "0",
            valor = 0
        };
        var query = _context.movimentoCaixas.Where(m => m.IdContaCorrente == idconta && m.DataMov >= ini && m.DataMov <= fim)
            .Include(m => m.Transacao)
            .Select(e => new ExtratoConta
            {
                valor = e.Valor,
                credito = null,
                debito = null,
                sinal = e.Sinal,
                datamov = e.DataMov,
                historico = e.Transacao.Descricao + " " + e.Observacao.Trim(),
                idmovbanco = e.idmovbanco,
                tipo = "1",
                saldo = 0
            }
            ).ToList().OrderBy(e => e.datamov).ThenBy(e => e.idmovbanco);
        var extratoFinal = new List<ExtratoConta> { resultado };
        decimal saldoAtual = saldoAnterior;
        foreach (var item in query)
        {
            if (item.sinal == "C")
            {
                saldoAtual += item.valor;
                item.credito = item.valor;
                item.debito = null;
            }
            else
            {
                saldoAtual -= item.valor;
                item.debito = item.valor;
                item.credito = null;
            }

            item.saldo = saldoAtual;
            extratoFinal.Add(item);
        }
        return extratoFinal;
    }
}