using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Contracts;
using ProductManager.Domain.Models;
using ProductManager.Persistence.SqlServer.Contexts;
using System.Linq.Expressions;

namespace ProductManager.Persistence.SqlServer.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly Lazy<DbSet<TEntity>> _dbSet;
    private readonly DataBaseContext _dataBaseContext;

    public Repository(DataBaseContext dataBaseContext)
    {
        _dbSet = new Lazy<DbSet<TEntity>>(() => dataBaseContext.Set<TEntity>());

        _dataBaseContext = dataBaseContext;
    }

    public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        var addedEntity = await this._dbSet.Value.AddAsync(entity, cancellationToken);

        await this._dataBaseContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity;
    }

    public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return await _dbSet.Value.AsQueryable().FirstOrDefaultAsync(predicate);
    }

    public Task<PaginatedListModel<TEntity>> GetPaginated<TField>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? predicate = null)
    {
        var items = _dbSet.Value.AsQueryable().Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        var totalCount = items.CountAsync();

        return Task.FromResult(new PaginatedListModel<TEntity>(items, pageIndex, pageSize, totalCount.Result));
    }

    public Task<TEntity> Remove(TEntity entity)
    {
        return Task.FromResult(_dataBaseContext.Remove(entity).Entity);
    }

    public Task<TEntity> Update(TEntity entity)
    {
        return Task.FromResult(_dataBaseContext.Update(entity).Entity);
    }
}
