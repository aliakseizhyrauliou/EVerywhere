using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using EVerywhere.ModulesCommon.Domain.Models;
using EVerywhere.ModulesCommon.Infrastructure.DbContext;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EVerywhere.ModulesCommon.Infrastructure.Repositories.Implementations;

public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : IDbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _table;

    public BaseRepository(TContext context)
    {   
        _context = context;
        _table = _context.Set<TEntity>();
    }

    
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _table.AnyAsync(predicate, cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken token = default)
    {
       return await _context.BeginTransactionAsync(isolationLevel, token);
    }

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _table.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _table.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _table
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> InsertAsync([NotNull]TEntity entity, CancellationToken cancellationToken = default)
    {
        await _table.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task InsertManyAsync([NotNull]IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> UpdateAsync([NotNull]TEntity entity, CancellationToken cancellationToken = default)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;    
    }

    public Task UpdateManyAsync([NotNull]IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var model = await _table.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken: cancellationToken);
        if(model is null)
            return;
        
        _table.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }
    

    public async Task DeleteAsync([NotNull]TEntity entity, CancellationToken cancellationToken = default)
    {
        _table.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
    }

    public Task DeleteManyAsync([NotNull]IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}