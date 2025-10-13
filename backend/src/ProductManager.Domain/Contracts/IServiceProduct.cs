using ProductManager.Domain.Models;

namespace ProductManager.Domain.Contracts;

public interface IServiceProduct
{
    public Task Add(ProductModel entity);
    public Task Update(ProductModel entity);
    public Task Remove(ProductModel entity);
    public Task FirstOrDefault(string searchFilter);
    public Task<PaginatedListModel<ProductModel>> GetPaginated(int pageIndex, int pageSize, string searchFilter);
}
