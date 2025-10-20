using ProductManager.Domain.Models;
using System.Linq.Expressions;

namespace ProductManager.Domain.Contracts;

public interface IRepository<TEntity>
    where TEntity : class
{
    public Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);
    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    public Task<TEntity> Remove(TEntity entity, CancellationToken cancellationToken = default);
    public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null);
    public Task<PaginatedListModel<TEntity>> GetPaginated<TField>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? predicate = null);
}
