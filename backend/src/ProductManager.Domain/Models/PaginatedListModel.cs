namespace ProductManager.Domain.Models;

public class PaginatedListModel<TEntity> : List<TEntity>
{
    public IEnumerable<TEntity> Items { get; private set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }

    public PaginatedListModel(IEnumerable<TEntity> items, int pageIndex, int pageSize, int totalCount)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalPages = (totalCount - totalCount % PageSize) / PageSize;
        TotalCount = totalCount;
    }
}
