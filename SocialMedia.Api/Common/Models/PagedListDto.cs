using X.PagedList;

namespace SocialMedia.Api.Common.Models;

public class PagedListDto<T>
{
    public IList<T> Items { get; set; }
    public int TotalItemCount { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; } = 10;
    public int PageNumber { get; set; }
    public PagedListDto(IPagedList<T> pagedList)
    {
        Items = pagedList.ToList();
        TotalItemCount = pagedList.TotalItemCount;
        PageCount = pagedList.PageCount;
        PageNumber = pagedList.PageNumber;
        PageSize = pagedList.PageSize;
    }
}