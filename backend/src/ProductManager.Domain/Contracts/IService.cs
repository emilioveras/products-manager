using ProductManager.Domain.Models;

namespace ProductManager.Domain.Contracts;

public interface IService
{
    public Task<ProductModel> Add(ProductModel entity);
    public Task<ProductModel> Update(ProductModel entity);
    public Task<ProductModel> Remove(ProductModel entity);
    public Task<ProductModel> FirstOrDefault(string searchString);
    public Task<PaginatedListModel<ProductModel>> GetPaginated<TField>(int pageIndex, int pageSize, string searchFilter);
}
