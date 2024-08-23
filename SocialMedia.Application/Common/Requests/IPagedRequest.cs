namespace SocialMedia.Application.Common.Requests;

public interface IPagedRequest
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}