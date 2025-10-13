using ProductManager.Domain.Contracts;
using ProductManager.Domain.Models;

namespace ProductManage.Service;

public class ProductService : IService
{
    private readonly IRepository<ProductModel> _repository;

    public ProductService(IRepository<ProductModel> repository)
    {
        _repository = repository;
    }

    public Task<ProductModel> Add(ProductModel entity)
    {
        return _repository.Add(entity);
    }

    public Task<ProductModel> FirstOrDefault(string searchFilter)
    {
        return _repository.FirstOrDefault(e =>
            e.Name.Contains(searchFilter) ||
            e.Category.Contains(searchFilter) ||
            e.Price.ToString().Contains(searchFilter) ||
            e.Stock.ToString().Contains(searchFilter));
    }

    public Task<PaginatedListModel<ProductModel>> GetPaginated<TField>(int pageIndex, int pageSize, string searchFilter)
    {
        return _repository.GetPaginated<TField>(pageIndex, pageSize, e =>
            e.Name.Contains(searchFilter) ||
            e.Category.Contains(searchFilter) ||
            e.Price.ToString().Contains(searchFilter) ||
            e.Stock.ToString().Contains(searchFilter));
    }

    public Task<ProductModel> Remove(ProductModel entity)
    {
        return _repository.Remove(entity);
    }

    public Task<ProductModel> Update(ProductModel entity)
    {
        return _repository.Update(entity);
    }
}
