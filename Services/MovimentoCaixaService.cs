using ADUSAPI.Context;
using ADUSAPI.Entities;
using ADUSAPICore.Models;
using ADUSClient.MovimentoCaixa;
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
        string? descricao = null)
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
            query = query.Where(m => m.Observacao.Contains(descricao));

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
                nomeparceiro = m.parceiro.RazaoSocial
            }).ToListAsync();
    }

    public async Task<MovimentoCaixaViewModel?> ObterPorIdAsync(int id)
    {
        return await _context.movimentoCaixas
            .Include(m => m.Transacao)
            .Include(m => m.CentroCusto)
            .Include(m => m.ContaCorrente)
            .Include(m => m.Categoria)
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
                DescCategoria = m.Categoria.Descricao
            })
            .FirstOrDefaultAsync();
    }

    public async Task AdicionarAsync(MovimentoCaixaViewModel entidade)
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
            Sinal = entidade.Sinal
        };
        _context.movimentoCaixas.Add(mc);
        await _context.SaveChangesAsync();
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
}