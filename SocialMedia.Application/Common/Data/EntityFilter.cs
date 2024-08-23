namespace SocialMedia.Application.Common.Data;

public class EntityFilter
{
    public string OrderColumn { get; set; } = string.Empty;
    public bool OrderDescending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}