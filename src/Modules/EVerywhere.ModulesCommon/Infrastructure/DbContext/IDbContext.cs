using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EVerywhere.ModulesCommon.Infrastructure.DbContext;

public interface IDbContext
{
    public Task MigrateDatabase();
    
    public int SaveChanges();
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default);
    
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}