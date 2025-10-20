using ProductManager.Domain.Contracts;
using ProductManager.Domain.Models;
using AutoMapper;

namespace ProductManager.Service;

public class ProductService : IServiceProduct
{
    private readonly IRepository<ProductModel> _repository;

    public ProductService(IRepository<ProductModel> repository)
    {
        _repository = repository;
    }

    public Task Add(ProductModel entity)
    {
        return _repository.Add(entity);
    }

    public Task FirstOrDefault(string searchFilter)
    {
        return _repository.FirstOrDefault(e =>
            e.Name.Contains(searchFilter) ||
            e.Category.Contains(searchFilter) ||
            e.Price.ToString().Contains(searchFilter) ||
            e.Stock.ToString().Contains(searchFilter));
    }

    public async Task<PaginatedListModel<ProductModel>> GetPaginated(int pageIndex, int pageSize, string searchFilter)
    {
        var resutls = await _repository.GetPaginated<ProductModel>(pageIndex, pageSize);

        return new PaginatedListModel<ProductModel>(resutls.Items, resutls.PageIndex, resutls.PageSize, resutls.TotalCount);

        //return string.IsNullOrWhiteSpace(searchFilter) ?
        //    _repository.GetPaginated<ProductModel>(pageIndex, pageSize) :
        //    _repository.GetPaginated<ProductModel>(pageIndex, pageSize, e =>
        //    e.Name.Contains(searchFilter) ||
        //    e.Category.Contains(searchFilter) ||
        //    e.Price.ToString().Contains(searchFilter) ||
        //    e.Stock.ToString().Contains(searchFilter));
    }

    public Task Remove(ProductModel entity)
    {
        return _repository.Remove(entity);
    }

    public Task Update(ProductModel entity)
    {
        return _repository.Update(entity);
    }
}
