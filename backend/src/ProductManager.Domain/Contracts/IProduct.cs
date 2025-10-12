using ProductManager.Domain.Models;

namespace ProductManager.Domain.Contracts;

public interface IProduct
{
    public Task<PaginatedListModel<ProductModel>> Get(int pageIndex, int pageSize, string? searchTerm = null);
    public Task<ProductModel> Post (ProductModel product);
    public Task<ProductModel> Put(ProductModel product);
    public Task<ProductModel> Delte(int productId);
}
