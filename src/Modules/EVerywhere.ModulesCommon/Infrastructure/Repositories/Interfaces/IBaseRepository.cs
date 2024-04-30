using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using EVerywhere.ModulesCommon.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace EVerywhere.ModulesCommon.Infrastructure.Repositories.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default);
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity> InsertAsync([NotNull] TEntity entity,
        CancellationToken cancellationToken = default);

    Task InsertManyAsync([NotNull] IList<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync([NotNull] TEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateManyAsync([NotNull] IList<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(long id, 
        CancellationToken cancellationToken = default);

    Task DeleteAsync([NotNull] TEntity entity, 
        CancellationToken cancellationToken = default);

    Task DeleteManyAsync([NotNull] IList<TEntity> entities,
        CancellationToken cancellationToken = default);
}
