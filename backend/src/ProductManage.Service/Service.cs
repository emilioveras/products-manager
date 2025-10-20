using ProductManager.Domain.Contracts;
using ProductManager.Domain.Models;

namespace ProductManager.Service;

public class Service : IService
{
    private readonly IRepository<ProductModel> _repository;

    public Service(IRepository<ProductModel> repository)
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

    public async Task<PaginatedListModel<ProductModel>> GetPaginated(int pageIndex, int pageSize, string searchFilter)
    {
        return string.IsNullOrWhiteSpace(searchFilter) ?
            await _repository.GetPaginated<ProductModel>(pageIndex, pageSize) :
            await _repository.GetPaginated<ProductModel>(pageIndex, pageSize, e =>
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
